import { jwtDecode } from 'jwt-decode';
import AuthService from './services/AuthService.ts';
import UserService from './services/UserService.ts';

export enum Status {
  Successful,
  Unsuccessful
}

type Result = {
  status: Status;
  tokens: string[];
};

async function fetchAccount(
  accessToken: string,
  refreshToken: string,
): Promise<Result> {
  if (accessToken == 'token') {
    return {status: Status.Unsuccessful, tokens: []};
  }

  const token = jwtDecode(accessToken);

  if (token.exp != undefined) {
    if (Date.now() > token.exp) {
      const clientId = process.env.CLIENT_ID;
      const clientSecret = process.env.CLIENT_SECRET;

      if (clientId == undefined || clientSecret == undefined)
        throw new Error();

      let response = await AuthService.refresh(refreshToken, clientId, clientSecret);

      if (response != null) {
        UserService.setUser(response.access_token);

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

export default {
  fetchAccount
}
