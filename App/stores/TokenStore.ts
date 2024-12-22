import IStore from './IStore.ts';
import {Token} from '../models/Token.ts';
import {UserType} from '../enums/ModelEnums.ts';

export class TokenStore implements IStore<Token> {
  state: Token = new Token('0', UserType.User);

  setToken(user: Token): void {
    this.state = user;
  }
}
