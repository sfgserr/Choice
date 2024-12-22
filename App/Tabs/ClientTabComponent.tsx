import CategoriesScreen from '../screens/tab/CategoriesScreen.tsx';
import {CategoriesService} from '../services/CategoriesService.ts';
import OrderRequestsScreen from '../screens/tab/OrderRequestsScreen.tsx';
import ChatScreen from '../screens/tab/ChatScreen.tsx';
import AccountScreen from '../screens/tab/AccountScreen.tsx';
import * as React from 'react';
import {createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import {ClientTabProps, TabScreenProps} from '../types/NavigationTypes.ts';
import {Image} from 'react-native';

export default function ClientTabComponent({route, navigation}: TabScreenProps) {
  const ClientTab = createBottomTabNavigator<ClientTabProps>();

  const graph = route.params.graph;

  const getTabBarIcon = ({size, focused, color, source} : {
    size: number,
    focused: boolean,
    color: string,
    source: any,

  }) => {
    return <Image
      source={source}
      style={{
        width: 20,
        height: 20,
        tintColor: focused ? '#2975CC' : '#99A2AD',
        resizeMode: 'contain'
      }}/>
  };

  return (
    <ClientTab.Navigator>
      <ClientTab.Screen
        name={'Categories'}
        component={CategoriesScreen}
        initialParams={{categoriesService: graph.resolve<CategoriesService>("CategoriesService")}}
        options={{
          headerShown: false,
          tabBarLabel: 'Услуги',
          tabBarActiveTintColor: '#2975CC',
          tabBarInactiveTintColor: '#99A2AD',
          tabBarLabelStyle: {
            fontSize: 10,
            fontWeight:'500'
          },
          tabBarIcon: ({size, focused, color}) =>
            getTabBarIcon({size, focused, color, source: require('../assets/images/categories.png')}),
        }}
      />
      <ClientTab.Screen
        name={'OrderRequests'}
        component={OrderRequestsScreen}
        options={{
          headerShown: false,
          tabBarLabel: 'Заказы',
          tabBarActiveTintColor: '#2975CC',
          tabBarInactiveTintColor: '#99A2AD',
          tabBarLabelStyle: {
            fontSize: 10,
            fontWeight:'500'
          },
          tabBarIcon: ({size, focused, color}) =>
            getTabBarIcon({size, focused, color, source: require('../assets/images/orders.png')}),
        }}
      />
      <ClientTab.Screen
        name={'Chat'}
        component={ChatScreen}
        options={{
          headerShown: false,
          tabBarLabel: 'Чат',
          tabBarActiveTintColor: '#2975CC',
          tabBarInactiveTintColor: '#99A2AD',
          tabBarLabelStyle: {
            fontSize: 10,
            fontWeight:'500'
          },
          tabBarIcon: ({size, focused, color}) =>
            getTabBarIcon({size, focused, color, source: require('../assets/images/chat.png')}),
        }}
      />
      <ClientTab.Screen
        name={'Account'}
        component={AccountScreen}
        options={{
          headerShown: false,
          tabBarLabel: 'Аккаунт',
          tabBarActiveTintColor: '#2975CC',
          tabBarInactiveTintColor: '#99A2AD',
          tabBarLabelStyle: {
            fontSize: 10,
            fontWeight:'500'
          },
          tabBarIcon: ({size, focused, color}) =>
            getTabBarIcon({size, focused, color, source: require('../assets/images/account.png')}),
        }}
      />
    </ClientTab.Navigator>
  )
}
