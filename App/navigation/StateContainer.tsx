import {State} from '../enums/AppEnums.ts';
import {NavigationContainer} from '@react-navigation/native';
import * as React from 'react';
import SignOutNavigator from './navigators/singOut/SignOutNavigator.tsx';
import ClientNavigator from './navigators/client/ClientNavigator.tsx';
import CompanyNavigator from './navigators/company/CompanyNavigator.tsx';
import UserNavigator from './navigators/user/UserNavigator.tsx';
import UnsubscribeNavigator from './navigators/unsubscribe/UnsubscribeNavigator.tsx';
import LoadingNavigator from './navigators/loading/LoadingNavigator.tsx';
import AdminNavigator from './navigators/admin/AdminNavigator.tsx';
import BannedNavigator from './navigators/banned/BannedNavigator.tsx';

export default function StateContainer({state}: {state: State}) {
  return (
    <NavigationContainer>
      <SignOutNavigator state={state}/>
      <ClientNavigator state={state}/>
      <CompanyNavigator state={state}/>
      <UserNavigator state={state}/>
      <UnsubscribeNavigator state={state}/>
      <LoadingNavigator state={state}/>
      <AdminNavigator state={state}/>
      <BannedNavigator state={state}/>
    </NavigationContainer>
  );
}
