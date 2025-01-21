import {State} from '../../../enums/AppEnums.ts';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';
import FillDataScreen from '../../../screens/FillDataScreen.tsx';
import * as React from 'react';
import {UserStack} from './UserStack.ts';

const FillData = () => (
  <UserStack.Screen
    name={'FillData'}
    component={gestureHandlerRootHOC(FillDataScreen)}
    options={{headerShown: false}}
  />
)

export default function UserNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.User && (
        <UserStack.Navigator>
          {FillData()}
        </UserStack.Navigator>
      )}
    </>
  )
}
