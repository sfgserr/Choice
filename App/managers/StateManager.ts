import {State} from '../enums/AppEnums.ts';
import {Status} from '../enums/AccountManagerEnums.ts';
import {UserType} from '../enums/ModelEnums.ts';
import {TokenStorageService} from '../services/object/TokenStorageService.ts';
import {AccountManager} from './AccountManager.ts';
import {TokenService} from '../services/auth/TokenService.ts';
import {ConnectionManager} from './ConnectionManager.ts';

export class StateManager {
  private readonly tokenStorageService: TokenStorageService;
  private readonly accountManager: AccountManager;
  private readonly tokenService: TokenService;
  private readonly userTypeToStateMap: {[id: UserType]: State} = {
    [UserType.User]: State.User,
    [UserType.Client]: State.Client,
    [UserType.Company]: State.Company,
    [UserType.Admin]: State.Admin,
  };

  constructor(
    tokenStorageService: TokenStorageService,
    accountManager: AccountManager,
    tokenService: TokenService,
  ) {
    this.tokenStorageService = tokenStorageService;
    this.accountManager = accountManager;
    this.tokenService = tokenService;
  }

  async getState(): Promise<State> {
    const tokens = await this.tokenStorageService.getTokens();
    if (tokens.length == 0) {
      return State.SignOut;
    }

    let result = await this.accountManager.fetchAccount(tokens[0], tokens[1]);

    if (result.status == Status.Successful) {
      let user = this.tokenService.getUser();

      await this.tokenStorageService.setTokensToStorage(
        result.tokens[0],
        result.tokens[1],
      );

      if (!user.subscribed) {
        return State.Unsubscribe;
      }

      if (user.banned) {
        return State.Banned;
      }

      if (user.userType == UserType.Client || user.userType == UserType.Company)
        await ConnectionManager.init();

      let state = this.userTypeToStateMap[user.userType];

      return state;
    } else {
      return State.SignOut;
    }
  }

  async signIn(accessToken: string, refreshToken: string) {
    await this.tokenStorageService.setTokensToStorage(
      accessToken,
      refreshToken,
    );

    let user = this.tokenService.getUser();

    if (!user.subscribed) {
      return State.Unsubscribe;
    }

    if (user.banned) {
      return State.Banned;
    }

    if (user.userType == UserType.Client || user.userType == UserType.Company)
      await ConnectionManager.init(accessToken);

    return this.userTypeToStateMap[user.userType];
  }

  async signOut() {
    await this.tokenStorageService.setTokensToStorage('token', 'token');
    this.tokenService.signOut();

    await ConnectionManager.disconnect();

    return State.SignOut;
  }
}
