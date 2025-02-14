import CompanyTabComponent from '../../tabs/company/CompanyTabComponent.tsx';
import {CompanyStack} from './CompanyStack.ts';
import * as React from 'react';
import CreateOrderResponseScreen from '../../../screens/CreateOrderResponseScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';
import CompanyChatScreen from '../../../screens/CompanyChatScreen.tsx';
import CompanyImageViewScreen from '../../../screens/CompanyImageViewScreen.tsx';

export const Tab = () => (
  <CompanyStack.Screen
    name={'Tab'}
    component={CompanyTabComponent}
    options={{headerShown: false}}/>
);

export const ImageView = () => (
  <CompanyStack.Screen
    name={'ImageView'}
    component={CompanyImageViewScreen}
    options={{headerShown: false}}/>
);

export const CreateOrderResponse = () => (
  <CompanyStack.Screen
    name={'CreateOrderResponse'}
    component={gestureHandlerRootHOC(CreateOrderResponseScreen)}
    options={{headerShown: false}}/>
);

export const Chat = () => (
  <CompanyStack.Screen
    name={'Chat'}
    component={CompanyChatScreen}
    options={{headerShown: false}}/>
);
