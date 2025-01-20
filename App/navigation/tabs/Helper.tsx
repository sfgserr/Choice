import {Image} from 'react-native';
import {BottomTabNavigationOptions} from '@react-navigation/bottom-tabs';
import * as React from 'react';

export const getTabBarIcon = ({focused, source} : {
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

export const getOptions = ({title, source}: {title: string, source: any}): BottomTabNavigationOptions => {
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
