import {CompanyTabScreenProps} from '../../../types/NavigationTypes.ts';
import * as React from 'react';
import {CompanyTab} from './CompanyTab.ts';
import {Account, Chat, OrderRequests} from './Screens.tsx';

export default function CompanyTabComponent({route, navigation}: CompanyTabScreenProps) {
  return (
    <CompanyTab.Navigator>
      <OrderRequests/>
      <Chat/>
      <Account/>
    </CompanyTab.Navigator>
  )
}
