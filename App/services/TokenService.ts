import {Token} from '../models/Token.ts';
import {AuthService} from './AuthService.ts';
import {TokenStore} from '../stores/TokenStore.ts';
import {jwtDecode} from 'jwt-decode';
import {UserClaims} from '../types/ServiceTypes.ts';
import {UserType} from '../enums/ModelEnums.ts';

export class TokenService {
  private readonly store: TokenStore;
  private readonly authService: AuthService;

  constructor(store: TokenStore, authService: AuthService) {
    this.store = store;
    this.authService = authService;
  }

  getUser(): Token {
    return this.store.state;
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
      this.store.setToken(new Token(token.sub, this.convertStringToUserType(token.type)));
    }
  }

  signOut() {
    this.store.setToken(new Token('0', UserType.User));
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
