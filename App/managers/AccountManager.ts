import { jwtDecode } from 'jwt-decode';
import {TokenService} from '../services/auth/TokenService.ts';
import {FetchAccountResult} from '../types/AccountManagerTypes.ts';
import {Status} from '../enums/AccountManagerEnums.ts';

export class AccountManager {
  private readonly tokenService: TokenService;

  constructor(tokenService: TokenService) {
    this.tokenService = tokenService;
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
        let response = await this.tokenService.refresh(refreshToken);

        if (response != null) {
          this.tokenService.setUser(accessToken);
          return {status: Status.Successful, tokens: response};
        }
        else {
          return {status: Status.Unsuccessful, tokens: []};
        }
      } else {
        this.tokenService.setUser(accessToken);
        return {status: Status.Successful, tokens: [accessToken, refreshToken]};
      }
    }

    throw new Error();
  }
}
