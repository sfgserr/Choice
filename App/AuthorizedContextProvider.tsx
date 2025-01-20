import * as React from 'react';
import {Auth} from './types/AppTypes.ts';
import {State} from './enums/AppEnums.ts';
import {useDependency} from './stores/DependencyInjection.ts';
import {StateManager} from './managers/StateManager.ts';

export const AuthContext = React.createContext<Auth>({
  signIn: (accessToken, refreshToken) => {},
  signOut: () => {},
  changeState: (state: State) => {}
});

export const AuthorizedContextProvider = ({children, setState}: {
  children: any
  setState: (state: State) => void}) => {
  const stateManager = useDependency<StateManager>('StateManager');

  const authContext = React.useMemo(
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

  return (
    <AuthContext.Provider value={authContext}>
      {children}
    </AuthContext.Provider>
  )
}
