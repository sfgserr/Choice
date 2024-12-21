import {State} from '../enums/AppEnums.ts';

export type Auth = {
  signIn: (accessToken: string, refreshToken: string) => void;
  signOut: () => void;
  changeState: (state: State) => void;
}
