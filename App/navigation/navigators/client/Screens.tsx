import ClientTabComponent from '../../tabs/client/ClientTabComponent.tsx';
import {ClientStack} from './ClientStack.ts';
import * as React from 'react';
import MapScreen from '../../../screens/MapScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';
import CreateOrderRequestScreen from '../../../screens/CreateOrderRequestScreen.tsx';
import EditOrderRequestScreen from '../../../screens/EditOrderRequestScreen.tsx';

export const Tab = () => (
  <ClientStack.Screen
    name={'Tab'}
    component={ClientTabComponent}
    options={{headerShown: false}}
  />
)

export const Map = () => (
  <ClientStack.Screen
    name={'Map'}
    component={MapScreen}
    options={{headerShown: false}}
  />
)

export const CreateOrderRequest = () => (
  <ClientStack.Screen
    name={'CreateOrderRequest'}
    component={gestureHandlerRootHOC(CreateOrderRequestScreen)}
    options={{headerShown: false}}
  />
)

export const EditOrderRequest = () => (
  <ClientStack.Screen
    name={'EditOrderRequest'}
    component={gestureHandlerRootHOC(EditOrderRequestScreen)}
    options={{headerShown: false}}
  />
)
