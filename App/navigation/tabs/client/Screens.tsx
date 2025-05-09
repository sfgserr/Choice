import CategoriesScreen from '../../../screens/tab/client/CategoriesScreen.tsx';
import {getOptions} from '../Helper.tsx';
import {ClientTab} from './ClientTab.ts';
import * as React from 'react';
import OrderRequestsScreen from '../../../screens/tab/client/OrderRequestsScreen.tsx';
import ChatsScreen from '../../../screens/ChatsScreen.tsx';
import AccountScreen from '../../../screens/tab/client/AccountScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';

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
    component={gestureHandlerRootHOC(OrderRequestsScreen)}
    options={getOptions({title: 'Заказы', source: require('../../../assets/images/orders.png')})}
  />
)

export const Chats = () => (
  <ClientTab.Screen
    name={'Chats'}
    component={ChatsScreen}
    options={getOptions({title: 'Чат', source: require('../../../assets/images/chat.png')})}
  />
)

export const Account = () => (
  <ClientTab.Screen
    name={'Account'}
    component={gestureHandlerRootHOC(AccountScreen)}
    options={getOptions({title: 'Аккаунт', source: require('../../../assets/images/account.png')})}
  />
)
