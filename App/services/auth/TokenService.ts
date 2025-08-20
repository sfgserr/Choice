import {Token, UserClaims} from '../../types/ServiceTypes.ts';
import {AuthService} from './AuthService.ts';
import {jwtDecode} from 'jwt-decode';
import {UserType} from '../../enums/ModelEnums.ts';

export class TokenService {
  private readonly authService: AuthService;

  private token: Token;

  constructor(authService: AuthService) {
    this.authService = authService;
    this.token = {id: '0', userType: UserType.User, subscribed: undefined, banned: undefined};
  }

  getUser(): Token {
    return this.token;
  }

  async login(email: string, password: string) {
    let result = await this.authService.login(email, password);

    if (result != null) {
      this.setUser(result.access_token);

      return [result.access_token, result.refresh_token];
    }

    return null;
  }

  async refresh(refreshToken: string) {
    let result = await this.authService.refresh(refreshToken);

    if (result != null) {
      this.setUser(result.access_token);

      return [result.access_token, result.refresh_token];
    }

    return null;
  }

  setUser(accessToken: string) {
    const token = jwtDecode<UserClaims>(accessToken);

    if (token.sub != undefined) {
      this.token = {
        id: token.sub,
        userType: this.convertStringToUserType(token.type),
        subscribed: token.subscribed,
        banned: token.banned,
      };
    }
  }

  signOut() {
    this.token = {id: '0', userType: UserType.User, subscribed: undefined};
  }

  private convertStringToUserType(type: string): UserType {
    switch (type) {
      case 'Admin':
        return UserType.Admin;
      case 'User':
        return UserType.User;
      case 'Client':
        return UserType.Client;
      case 'Company':
        return UserType.Company;
      default:
        throw new Error();
    }
  }
}
