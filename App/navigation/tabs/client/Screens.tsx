import CategoriesScreen from '../../../screens/tab/client/CategoriesScreen.tsx';
import {getOptions} from '../Helper.tsx';
import {ClientTab} from './ClientTab.ts';
import * as React from 'react';
import OrderRequestsScreen from '../../../screens/tab/client/OrderRequestsScreen.tsx';
import ChatsScreen from '../../../screens/ChatsScreen.tsx';
import AccountScreen from '../../../screens/tab/client/AccountScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';
import {useEffect, useState} from 'react';
import {DeviceEventEmitter} from 'react-native';
import {useDependency} from "../../../services/Hooks.ts";
import {ChatService} from "../../../services/domain/ChatService.ts";
import {UserService} from "../../../services/domain/UserService.ts";

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

export const Chats = () => {
  const chatService = useDependency<ChatService>('ChatService');
  const userService = useDependency<UserService>('UserService');

  const [options, setOptions] = useState(getOptions({title: 'Чат', source: require('../../../assets/images/chat.png')}));

  useEffect(() => {
    DeviceEventEmitter.addListener('messageSentNotification', () => {
      setOptions(prev => {
        if (!prev.tabBarBadge) {
          prev.tabBarBadge = 1;
        } else {
          prev.tabBarBadge = +prev.tabBarBadge + 1;
        }

        return prev;
      });
    });

    DeviceEventEmitter.addListener('userReadMessage', () => {
      setOptions(prev => {
        if (prev.tabBarBadge) {
          prev.tabBarBadge = +prev.tabBarBadge - 1;
          prev.tabBarBadge = prev.tabBarBadge == 0 ? undefined : prev.tabBarBadge;
        }
        return prev;
      });
    })



    //getUnreadMessages();
  }, []);



  return (
    <ClientTab.Screen
      name={'Chats'}
      component={ChatsScreen}
      options={options}
    />
  )
}

export const Account = () => (
  <ClientTab.Screen
    name={'Account'}
    component={gestureHandlerRootHOC(AccountScreen)}
    options={getOptions({title: 'Аккаунт', source: require('../../../assets/images/account.png')})}
  />
)
