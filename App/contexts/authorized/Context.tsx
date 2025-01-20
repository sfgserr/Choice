import * as React from 'react';
import {Auth} from '../../types/AppTypes.ts';
import {State} from '../../enums/AppEnums.ts';
import {StateManager} from '../../managers/StateManager.ts';
import {useDependency} from '../../services/Hooks.ts';

export const AuthContext = React.createContext<Auth>({
  signIn: (accessToken, refreshToken) => {},
  signOut: () => {},
  changeState: (state: State) => {}
});

export const useAuthContext = (setState: (state: State) => void) => {
  const stateManager: StateManager = useDependency<StateManager>('StateManager');

  return React.useMemo(
    () => ({
      signIn: (accessToken: string, refreshToken: string) => {
        async function setTokens() {
          const state = await stateManager.signIn(accessToken, refreshToken);
          setState(state);
        }
        setTokens();
      },
      signOut: () => {
        async function setTokens() {
          const state = await stateManager.signOut();
          setState(state);
        }
        setTokens();
      },
      changeState: (state: State) => {
        setState(state);
      }
    }),
    [stateManager]
  );
}
