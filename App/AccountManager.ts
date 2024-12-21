import { jwtDecode } from 'jwt-decode';
import {UserService} from './services/UserService.ts';
import {FetchAccountResult} from './types/AccountManagerTypes.ts';
import {Status} from './enums/AccountManagerEnums.ts';

export class AccountManager {
  private readonly userService: UserService;

  constructor(userService: UserService) {
    this.userService = userService;
  }

  async fetchAccount(
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
