import * as React from 'react';
import {CompanyChangePasswordScreenProps} from '../types/NavigationTypes.ts';
import ChangePasswordScreen from './ChangePasswordScreen.tsx';

export default function CompanyChangePasswordScreen({route, navigation}: CompanyChangePasswordScreenProps) {
  return <ChangePasswordScreen navigation={navigation}/>;
}
