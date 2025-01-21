import {State} from '../../../enums/AppEnums.ts';
import LoadingScreen from '../../../screens/LoadingScreen.tsx';
import * as React from 'react';
import {LoadingStack} from './LoadingStack.ts';

const Loading = () => (
  <LoadingStack.Screen
    name="Loading"
    component={LoadingScreen}
    options={{headerShown: false}}
  />
)

export default function LoadingNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.Restoring && (
        <LoadingStack.Navigator>
          {Loading()}
        </LoadingStack.Navigator>
      )}
    </>
  )
}
