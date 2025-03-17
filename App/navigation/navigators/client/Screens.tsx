import ClientTabComponent from '../../tabs/client/ClientTabComponent.tsx';
import {ClientStack} from './ClientStack.ts';
import * as React from 'react';
import MapScreen from '../../../screens/MapScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';
import CreateOrderRequestScreen from '../../../screens/CreateOrderRequestScreen.tsx';
import EditOrderRequestScreen from '../../../screens/EditOrderRequestScreen.tsx';
import ClientChatScreen from '../../../screens/ClientChatScreen.tsx';
import ClientImageViewScreen from '../../../screens/ClientImageViewScreen.tsx';
import ClientChangePasswordScreen from '../../../screens/ClientChangePasswordScreen.tsx';

export const Tab = () => (
  <ClientStack.Screen
    name={'Tab'}
    component={ClientTabComponent}
    options={{headerShown: false}}
  />
);

export const Map = () => (
  <ClientStack.Screen
    name={'Map'}
    component={gestureHandlerRootHOC(MapScreen)}
    options={{headerShown: false}}
  />
);

export const CreateOrderRequest = () => (
  <ClientStack.Screen
    name={'CreateOrderRequest'}
    component={gestureHandlerRootHOC(CreateOrderRequestScreen)}
    options={{headerShown: false}}
  />
);

export const EditOrderRequest = () => (
  <ClientStack.Screen
    name={'EditOrderRequest'}
    component={gestureHandlerRootHOC(EditOrderRequestScreen)}
    options={{headerShown: false}}
  />
);

export const Chat = () => (
  <ClientStack.Screen
    name={'Chat'}
    component={gestureHandlerRootHOC(ClientChatScreen)}
    options={{headerShown: false}}
  />
);

export const ImageView = () => (
  <ClientStack.Screen
    name={'ImageView'}
    component={ClientImageViewScreen}
    options={{headerShown: false}}
  />
);

export const ChangePassword = () => (
  <ClientStack.Screen
    name={'ChangePassword'}
    component={gestureHandlerRootHOC(ClientChangePasswordScreen)}
    options={{headerShown: false}}
  />
);
