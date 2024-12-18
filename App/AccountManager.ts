import { jwtDecode } from 'jwt-decode';
import {AuthService} from './services/AuthService.ts';
import {UserService} from './services/UserService.ts';

export enum Status {
  Successful,
  Unsuccessful
}

type Result = {
  status: Status;
  tokens: string[];
};

export class AccountManager {
  private readonly authService: AuthService;
  private readonly userService: UserService;

  constructor(authService: AuthService, userService: UserService) {
    this.authService = authService;
    this.userService = userService;
  }

  public async fetchAccount(
    accessToken: string,
    refreshToken: string,
  ): Promise<Result> {
    if (accessToken == 'token') {
      return {status: Status.Unsuccessful, tokens: []};
    }

    const token = jwtDecode(accessToken);

    if (token.exp != undefined) {
      if (Date.now() > token.exp) {
        let response = await this.authService.refresh(refreshToken);

        if (response != null) {
          this.userService.setUser(response.access_token);

          return {status: Status.Successful, tokens: [accessToken, refreshToken]};
        }
        else {
          return {status: Status.Unsuccessful, tokens: []};
        }
      } else {
        return {status: Status.Successful, tokens: [accessToken, refreshToken]};
      }
    }

    throw new Error();
  }
}
