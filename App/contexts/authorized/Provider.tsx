import * as React from 'react';
import {State} from '../../enums/AppEnums.ts';
import {AuthContext, useAuthContext} from './Context.tsx';

export const AuthContextProvider = ({children, setState}: {
  children: any
  setState: (state: State) => void}) => {
  const authContext = useAuthContext(setState);

  return (
    <AuthContext.Provider value={authContext}>
      {children}
    </AuthContext.Provider>
  );
};
