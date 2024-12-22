import IStore from './IStore.ts';

export class UserStore implements IStore<any> {
  state: any;

  setUser(state: any) {
    this.state = state;
  }
}
