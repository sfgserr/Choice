import * as React from 'react';
import {State} from '../../../enums/AppEnums.ts';
import {SignOutStack} from './SignOutStack.ts';
import {Login, RegisterClient, RegisterCompany} from './Screens.tsx';

export default function SignOutNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.SignOut && (
        <SignOutStack.Navigator>
          {Login()}
          {RegisterClient()}
          {RegisterCompany()}
        </SignOutStack.Navigator>
      )}
    </>
  );
}
