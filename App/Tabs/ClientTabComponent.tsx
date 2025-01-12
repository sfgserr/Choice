import CategoriesScreen from '../screens/tab/CategoriesScreen.tsx';
import {CategoryService} from '../services/domain/CategoryService.ts';
import OrderRequestsScreen from '../screens/tab/OrderRequestsScreen.tsx';
import ChatScreen from '../screens/tab/ChatScreen.tsx';
import AccountScreen from '../screens/tab/AccountScreen.tsx';
import * as React from 'react';
import {BottomTabNavigationOptions, createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import {ClientTabProps, TabScreenProps} from '../types/NavigationTypes.ts';
import {Image} from 'react-native';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';

export default function ClientTabComponent({route, navigation}: TabScreenProps) {
  const ClientTab = createBottomTabNavigator<ClientTabProps>();

  const graph = route.params.graph;

  const getTabBarIcon = ({focused, source} : {
    focused: boolean
    source: any}) => {
    return <Image
      source={source}
      style={{
        width: 20,
        height: 20,
        tintColor: focused ? '#2975CC' : '#99A2AD',
        resizeMode: 'contain'
      }}/>
  };

  const getOptions = ({title, source}: {title: string, source: any}): BottomTabNavigationOptions => {
    return {
      headerShown: false,
      tabBarLabel: title,
      tabBarActiveTintColor: '#2975CC',
      tabBarInactiveTintColor: '#99A2AD',
      tabBarLabelStyle: {
        fontSize: 10,
        fontWeight:'500'
      },
      tabBarIcon: ({size, focused, color}) =>
        getTabBarIcon({focused, source}),
    }
  }

  return (
    <ClientTab.Navigator>
      <ClientTab.Screen
        name={'Categories'}
        component={CategoriesScreen}
        initialParams={{categoryService: graph.resolve<CategoryService>("CategoryService")}}
        options={getOptions({title: 'Услуги', source: require('../assets/images/categories.png')})}
      />
      <ClientTab.Screen
        name={'OrderRequests'}
        component={OrderRequestsScreen}
        initialParams={{
          orderRequestService: graph.resolve<OrderRequestService>("OrderRequestService"),
          categoryService: graph.resolve<CategoryService>("CategoryService")
        }}
        options={getOptions({title: 'Заказы', source: require('../assets/images/orders.png')})}
      />
      <ClientTab.Screen
        name={'Chat'}
        component={ChatScreen}
        options={getOptions({title: 'Чат', source: require('../assets/images/chat.png')})}
      />
      <ClientTab.Screen
        name={'Account'}
        component={AccountScreen}
        options={getOptions({title: 'Аккаунт', source: require('../assets/images/account.png')})}
      />
    </ClientTab.Navigator>
  )
}
