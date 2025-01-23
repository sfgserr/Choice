import LoginScreen from '../../../screens/LoginScreen.tsx';
import {SignOutStack} from './SignOutStack.ts';
import * as React from 'react';
import RegisterClientScreen from '../../../screens/RegisterClientScreen.tsx';
import RegisterCompanyScreen from '../../../screens/RegisterCompanyScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';

export const Login = () => (
  <SignOutStack.Screen
    name="Login"
    component={gestureHandlerRootHOC(LoginScreen)}
    options={{headerShown: false}}
  />
)

export const RegisterClient = () => (
  <SignOutStack.Screen
    name={'RegisterClient'}
    component={gestureHandlerRootHOC(RegisterClientScreen)}
    options={{headerShown: false}}
  />
)

export const RegisterCompany = () => (
  <SignOutStack.Screen
    name={'RegisterCompany'}
    component={gestureHandlerRootHOC(RegisterCompanyScreen)}
    options={{headerShown: false}}
  />
)
