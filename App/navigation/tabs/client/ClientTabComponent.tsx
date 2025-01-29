import * as React from 'react';
import {ClientTabScreenProps} from '../../../types/NavigationTypes.ts';
import {ClientTab} from './ClientTab.ts';
import {Account, Categories, Chats, OrderRequests} from './Screens.tsx';

export default function ClientTabComponent({route, navigation}: ClientTabScreenProps) {
  return (
    <ClientTab.Navigator>
      {Categories()}
      {OrderRequests()}
      {Chats()}
      {Account()}
    </ClientTab.Navigator>
  );
}
