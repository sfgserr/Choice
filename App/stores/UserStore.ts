import IStore from './IStore.ts';
import {User, UserType} from '../models/User.ts';

export class UserStore implements IStore<User> {
  state: User = new User('0', UserType.User);

  setUser(user: User): void {
    this.state = user;
  }
}
