import {AdminStack} from './AdminStack.ts';
import PanelScreen from '../../../screens/PanelScreen.tsx';
import * as React from 'react';
import EditCategoryScreen from '../../../screens/EditCategoryScreen.tsx';
import CreateCategoryScreen from '../../../screens/CreateCategoryScreen.tsx';
import EditClientScreen from '../../../screens/EditClientScreen.tsx';
import {gestureHandlerRootHOC} from "react-native-gesture-handler";
import EditCompanyScreen from "../../../screens/EditCompanyScreen.tsx";

export const Panel = () => (
  <AdminStack.Screen
    name={'Panel'}
    component={PanelScreen}
    options={{headerShown: false}}/>
);

export const EditCategory = () => (
  <AdminStack.Screen
    name={'EditCategory'}
    component={EditCategoryScreen}
    options={{headerShown: false}}/>
);

export const CreateCategory = () => (
  <AdminStack.Screen
    name={'CreateCategory'}
    component={CreateCategoryScreen}
    options={{headerShown: false}}/>
);

export const EditClient = () => (
  <AdminStack.Screen
    name={'EditClient'}
    component={gestureHandlerRootHOC(EditClientScreen)}
    options={{headerShown: false}}/>
);

export const EditCompany = () => (
  <AdminStack.Screen
    name={'EditCompany'}
    component={gestureHandlerRootHOC(EditCompanyScreen)}
    options={{headerShown: false}}/>
);
