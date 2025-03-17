import * as React from 'react';
import {ClientChangePasswordScreenProps} from '../types/NavigationTypes.ts';
import ChangePasswordScreen from './ChangePasswordScreen.tsx';

export default function ClientChangePasswordScreen({route, navigation}: ClientChangePasswordScreenProps) {
  return <ChangePasswordScreen navigation={navigation}/>;
}
