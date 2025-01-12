import {CompanyTabProps, TabScreenProps} from '../types/NavigationTypes.ts';
import {BottomTabNavigationOptions, createBottomTabNavigator} from '@react-navigation/bottom-tabs';
import CompanyRequestsScreen from '../screens/tab/CompanyRequestsScreen.tsx';
import {Image} from 'react-native';
import * as React from 'react';
import ChatScreen from '../screens/tab/ChatScreen.tsx';
import CompanyAccountScreen from '../screens/tab/CompanyAccountScreen.tsx';


export default function CompanyTabComponent({route, navigation}: TabScreenProps) {
  const CompanyTab = createBottomTabNavigator<CompanyTabProps>();

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
    <CompanyTab.Navigator>
      <CompanyTab.Screen
        name={'OrderRequests'}
        component={CompanyRequestsScreen}
        options={getOptions({title: 'Заказы', source: require('../assets/images/orders.png')})}/>
      <CompanyTab.Screen
        name={'Chat'}
        component={ChatScreen}
        options={getOptions({title: 'Чат', source: require('../assets/images/chat.png')})}/>
      <CompanyTab.Screen
        name={'Account'}
        component={CompanyAccountScreen}
        options={getOptions({title: 'Аккаунт', source: require('../assets/images/account.png')})}/>
    </CompanyTab.Navigator>
  )
}
