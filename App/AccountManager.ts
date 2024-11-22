import { jwtDecode } from 'jwt-decode';

enum Result {
  Successful,
  Unsuccessful
}

export default function fetchAccount(accessToken: string, refreshToken: string): Result {
  if (accessToken == 'token') {
    return Result.Unsuccessful;
  }

  const token = jwtDecode(accessToken);

  if (token.exp) {

  }
}
