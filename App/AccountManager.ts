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
  private readonly userService: UserService;

  constructor(userService: UserService) {
    this.userService = userService;
  }

  async fetchAccount(
    accessToken: string,
    refreshToken: string,
  ): Promise<Result> {
    if (accessToken == 'token') {
      return {status: Status.Unsuccessful, tokens: []};
    }

    const token = jwtDecode(accessToken);

    if (token.exp != undefined) {
      if (Date.now()/1000 > token.exp) {
        let response = await this.userService.refresh(refreshToken);

        if (response != null) {
          return {status: Status.Successful, tokens: [accessToken, refreshToken]};
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
