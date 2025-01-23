import {State} from '../../../enums/AppEnums.ts';
import SubscriptionPlansScreen from '../../../screens/SubscriptionPlansScreen.tsx';
import PaySubscriptionScreen from '../../../screens/PaySubscriptionScreen.tsx';
import * as React from 'react';
import {UnsubscribeStack} from './UnsubscribeStack.ts';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';

const SubscriptionPlans = () => (
  <UnsubscribeStack.Screen
    name={'SubscriptionPlans'}
    component={gestureHandlerRootHOC(SubscriptionPlansScreen)}
    options={{headerShown: false}}
  />
);

const PaySubscription = () => (
  <UnsubscribeStack.Screen
    name={'PaySubscription'}
    component={PaySubscriptionScreen}
    options={{headerShown: false}}
  />
)

export default function UnsubscribeNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.Unsubscribe && (
        <UnsubscribeStack.Navigator>
          {SubscriptionPlans()}
          {PaySubscription()}
        </UnsubscribeStack.Navigator>
      )}
    </>
  );
}
