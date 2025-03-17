import {AdminStack} from './AdminStack.ts';
import PanelScreen from '../../../screens/PanelScreen.tsx';
import * as React from 'react';

export const Panel = () => (
  <AdminStack.Screen
    name={'Panel'}
    component={PanelScreen}
    options={{headerShown: false}}/>
);
