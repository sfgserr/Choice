import CategoriesScreen from '../../../screens/tab/CategoriesScreen.tsx';
import {getOptions} from '../Helper.tsx';
import {ClientTab} from './ClientTab.ts';
import * as React from 'react';
import OrderRequestsScreen from '../../../screens/tab/OrderRequestsScreen.tsx';
import ChatScreen from '../../../screens/tab/ChatScreen.tsx';
import AccountScreen from '../../../screens/tab/AccountScreen.tsx';

export const Categories = () => (
  <ClientTab.Screen
    name={'Categories'}
    component={CategoriesScreen}
    options={getOptions({title: 'Услуги', source: require('../../../assets/images/categories.png')})}
  />
)

export const OrderRequests = () => (
  <ClientTab.Screen
    name={'OrderRequests'}
    component={OrderRequestsScreen}
    options={getOptions({title: 'Заказы', source: require('../../../assets/images/orders.png')})}
  />
)

export const Chat = () => (
  <ClientTab.Screen
    name={'Chat'}
    component={ChatScreen}
    options={getOptions({title: 'Чат', source: require('../../../assets/images/chat.png')})}
  />
)

export const Account = () => (
  <ClientTab.Screen
    name={'Account'}
    component={AccountScreen}
    options={getOptions({title: 'Аккаунт', source: require('../../../assets/images/account.png')})}
  />
)
