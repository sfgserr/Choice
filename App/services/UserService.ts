import {User, UserType} from '../models/User.ts';
import {AuthService} from './AuthService.ts';
import {UserStore} from '../stores/UserStore.ts';
import {jwtDecode} from 'jwt-decode';

type Claims = {
  sub: string;
  type: string;
};

export class UserService {
  private readonly store: UserStore;
  private readonly authService: AuthService;

  constructor(store: UserStore, authService: AuthService) {
    this.store = store;
    this.authService = authService;
  }

  getUser(): User {
    return this.store.state;
  }

  async login(email: string, password: string): Promise<string[]> {
    let result = await this.authService.login(email, password);

    if (result != null) {
      this.setUser(result.access_token);

      return [result.access_token, result.refresh_token];
    }

    throw new Error();
  }
  setUser(accessToken: string) {
    const token = jwtDecode<Claims>(accessToken);

    if (token.sub != undefined) {
      this.store.setUser(new User(token.sub, this.convertStringToUserType(token.type)));
    }
  }

  signOut() {
    this.store.setUser(new User('0', UserType.User));
  }

  private convertStringToUserType(type: string): UserType {
    switch (type) {
      case 'admin':
        return UserType.Admin;
      case 'user':
        return UserType.User;
      case 'client':
        return UserType.Client;
      case 'company':
        return UserType.Company;
      default:
        throw new Error();
    }
  }
}
