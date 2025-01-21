import CompanyTabComponent from '../../tabs/company/CompanyTabComponent.tsx';
import {CompanyStack} from './CompanyStack.ts';
import * as React from 'react';
import ImageViewScreen from '../../../screens/ImageViewScreen.tsx';
import CreateOrderResponseScreen from '../../../screens/CreateOrderResponseScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';

export const Tab = () => (
  <CompanyStack.Screen
    name={'Tab'}
    component={CompanyTabComponent}
    options={{headerShown: false}}/>
)

export const ImageView = () => (
  <CompanyStack.Screen
    name={'ImageView'}
    component={ImageViewScreen}
    options={{headerShown: false}}/>
)

export const CreateOrderResponse = () => (
  <CompanyStack.Screen
    name={'CreateOrderResponse'}
    component={gestureHandlerRootHOC(CreateOrderResponseScreen)}
    options={{headerShown: false}}/>
)
