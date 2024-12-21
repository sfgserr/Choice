import { jwtDecode } from 'jwt-decode';
import {UserService} from './services/UserService.ts';
import {FetchAccountResult} from './types/AccountManagerTypes.ts';
import {Status} from './enums/AccountManagerEnums.ts';
import {State} from './enums/AppEnums.ts';
import {UserType} from './enums/ModelEnums.ts';
import {TokenStorageService} from './services/TokenStorageService.ts';

export class AccountManager {
  private readonly userService: UserService;
  private readonly tokenStorageService: TokenStorageService;

  constructor(userService: UserService, tokenStorageService: TokenStorageService) {
    this.userService = userService;
    this.tokenStorageService = tokenStorageService;
  }

  async getState(): Promise<State> {
    const tokens = await this.tokenStorageService.getTokens();
    if (tokens.length == 0) {
      return State.SignOut;
    }

    let result = await this.fetchAccount(tokens[0], tokens[1]);

    if (result.status == Status.Successful) {
      let user = this.userService.getUser();

      await this.tokenStorageService.setTokensToStorage(result.tokens[0], result.tokens[1]);

      return user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin;
    }
    else {
      return State.SignOut;
    }
  }

  private async fetchAccount(
    accessToken: string,
    refreshToken: string,
  ): Promise<FetchAccountResult> {
    if (accessToken == 'token') {
      return {status: Status.Unsuccessful, tokens: []};
    }

    const token = jwtDecode(accessToken);

    if (token.exp != undefined) {
      if (Date.now()/1000 > token.exp) {
        let response = await this.userService.refresh(refreshToken);

        if (response != null) {
          this.userService.setUser(accessToken);
          return {status: Status.Successful, tokens: response};
        }
        else {
          return {status: Status.Unsuccessful, tokens: []};
        }
      } else {
        this.userService.setUser(accessToken);
        return {status: Status.Successful, tokens: [accessToken, refreshToken]};
      }
    }

    throw new Error();
  }
}
