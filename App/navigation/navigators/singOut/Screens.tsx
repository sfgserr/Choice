import LoginScreen from '../../../screens/LoginScreen.tsx';
import {SignOutStack} from './SignOutStack.ts';
import * as React from 'react';
import RegisterClientScreen from '../../../screens/RegisterClientScreen.tsx';
import RegisterCompanyScreen from '../../../screens/RegisterCompanyScreen.tsx';

export const Login = () => (
  <SignOutStack.Screen
    name="Login"
    component={LoginScreen}
    options={{headerShown: false}}
  />
)

export const RegisterClient = () => (
  <SignOutStack.Screen
    name={'RegisterClient'}
    component={RegisterClientScreen}
    options={{headerShown: false}}
  />
)

export const RegisterCompany = () => (
  <SignOutStack.Screen
    name={'RegisterCompany'}
    component={RegisterCompanyScreen}
    options={{headerShown: false}}
  />
)
