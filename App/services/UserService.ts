import {User, UserType} from '../models/User.ts';
import AuthService from './AuthService.ts';
import UserStore from '../stores/UserStore.ts';
import {jwtDecode} from 'jwt-decode';

const store = new UserStore();

type Claims = {
  sub: string;
  type: string;
};

function getUser(): User {
  return store.state;
}

async function login(login: string, password: string): Promise<void> {
  let result = await AuthService.login(login, password);

  if (result != null) {
    const token = jwtDecode<Claims>(result.access_token);

    if (token.sub != undefined) {
      store.setUser(new User(token.sub, convertStringToUserType(token.type)));
    }
  }
}

function convertStringToUserType(type: string): UserType {
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

export default {
  login,
  getUser
}
