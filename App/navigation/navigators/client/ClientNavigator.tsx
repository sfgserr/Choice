import * as React from 'react';
import {State} from '../../../enums/AppEnums.ts';
import {ClientStack} from './ClientStack.ts';
import {Tab, Map, CreateOrderRequest, EditOrderRequest, Chat} from './Screens.tsx';

export default function ClientNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.Client && (
        <ClientStack.Navigator>
          {Tab()}
          {Map()}
          {CreateOrderRequest()}
          {EditOrderRequest()}
          {Chat()}
        </ClientStack.Navigator>
      )}
    </>
  );
}
