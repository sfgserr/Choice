import {State} from '../../../enums/AppEnums.ts';
import * as React from 'react';
import {CompanyStack} from './CompanyStack.ts';
import {Chat, CreateOrderResponse, ImageView, Tab} from './Screens.tsx';

export default function CompanyNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.Company && (
        <CompanyStack.Navigator>
          {Tab()}
          {ImageView()}
          {CreateOrderResponse()}
          {Chat()}
        </CompanyStack.Navigator>
      )}
    </>
  );
}
