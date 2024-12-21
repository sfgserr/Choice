import {State} from './enums/AppEnums.ts';
import {Status} from './enums/AccountManagerEnums.ts';
import {UserType} from './enums/ModelEnums.ts';
import {TokenStorageService} from './services/TokenStorageService.ts';
import {AccountManager} from './AccountManager.ts';
import {UserService} from './services/UserService.ts';

export class StateManager {
  private readonly tokenStorageService: TokenStorageService;
  private readonly accountManager: AccountManager;
  private readonly userService: UserService;

  constructor(tokenStorageService: TokenStorageService, accountManager: AccountManager, userService: UserService) {
    this.tokenStorageService = tokenStorageService;
    this.accountManager = accountManager;
    this.userService = userService;
  }

  async getState(): Promise<State> {
    const tokens = await this.tokenStorageService.getTokens();
    if (tokens.length == 0) {
      return State.SignOut;
    }

    let result = await this.accountManager.fetchAccount(tokens[0], tokens[1]);

    if (result.status == Status.Successful) {
      let user = this.userService.getUser();

      await this.tokenStorageService.setTokensToStorage(result.tokens[0], result.tokens[1]);

      return user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin;
    }
    else {
      return State.SignOut;
    }
  }

  async signIn(accessToken: string, refreshToken: string) {
    await this.tokenStorageService.setTokensToStorage(accessToken, refreshToken);
    let user = this.userService.getUser();

    return user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin;
  }

  async signOut() {
    await this.tokenStorageService.setTokensToStorage('token', 'token');
    this.userService.signOut();

    return State.SignOut;
  }
}
