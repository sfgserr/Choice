import CompanyRequestsScreen from '../../../screens/tab/CompanyRequestsScreen.tsx';
import {getOptions} from '../Helper.tsx';
import {CompanyTab} from './CompanyTab.ts';
import * as React from 'react';
import ChatsScreen from '../../../screens/tab/ChatsScreen.tsx';
import CompanyAccountScreen from '../../../screens/tab/CompanyAccountScreen.tsx';

export const OrderRequests = () => (
  <CompanyTab.Screen
    name={'OrderRequests'}
    component={CompanyRequestsScreen}
    options={getOptions({title: 'Заказы', source: require('../../../assets/images/orders.png')})}/>
)

export const Chat = () => (
  <CompanyTab.Screen
    name={'Chats'}
    component={ChatsScreen}
    options={getOptions({title: 'Чат', source: require('../../../assets/images/chat.png')})}/>
)

export const Account = () => (
  <CompanyTab.Screen
    name={'Account'}
    component={CompanyAccountScreen}
    options={getOptions({title: 'Аккаунт', source: require('../../../assets/images/account.png')})}/>
)
