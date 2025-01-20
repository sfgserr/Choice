import * as React from 'react';
import {TokenService} from './services/auth/TokenService.ts';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import LoginScreen from './screens/LoginScreen.tsx';
import LoadingScreen from './screens/LoadingScreen.tsx';
import {ObjectGraph} from './services/ObjectGraph.ts';
import {StackProps} from './types/NavigationTypes.ts';
import {Auth} from './types/AppTypes.ts';
import {State} from './enums/AppEnums.ts';
import {StateManager} from './managers/StateManager.ts';
import MapScreen from './screens/MapScreen.tsx';
import ClientTabComponent from './Tabs/ClientTabComponent.tsx';
import {CompanyService} from './services/domain/CompanyService.ts';
import CreateOrderRequestScreen from './screens/CreateOrderRequestScreen.tsx';
import {gestureHandlerRootHOC} from 'react-native-gesture-handler';
import {OrderRequestService} from './services/domain/OrderRequestService.ts';
import EditOrderRequestScreen from './screens/EditOrderRequestScreen.tsx';
import {CategoryService} from './services/domain/CategoryService.ts';
import RegisterClientScreen from './screens/RegisterClientScreen.tsx';
import {ClientService} from './services/domain/ClientService.ts';
import RegisterCompanyScreen from './screens/RegisterCompanyScreen.tsx';
import FillDataScreen from './screens/FillDataScreen.tsx';
import CompanyTabComponent from './Tabs/CompanyTabComponent.tsx';
import SubscriptionPlansScreen from './screens/SubscriptionPlansScreen.tsx';
import {SubscriptionPaymentService} from './services/domain/SubscriptionPaymentService.ts';
import PaySubscriptionScreen from './screens/PaySubscriptionScreen.tsx';
import ImageViewScreen from './screens/ImageViewScreen.tsx';
import CreateOrderResponseScreen from './screens/CreateOrderResponseScreen.tsx';
import {AuthorizedContextProvider} from './AuthorizedContextProvider.tsx';
import {useDependency} from './stores/DependencyInjection.ts';
function App(): React.JSX.Element {
  const stateManager: StateManager = useDependency<StateManager>('StateManager');

  const [state, setState] = React.useState(State.Restoring);
  React.useEffect(() => {
    const getState = async () => {
      const state = await stateManager.getState();
      setState(state);
    }

    getState();
  }, []);

  const Stack = createNativeStackNavigator<StackProps>();

  return (
    <AuthorizedContextProvider setState={setState}>
      <NavigationContainer>
        {state == State.SignOut ? (
          <Stack.Navigator>
            <Stack.Screen
              name="Login"
              component={LoginScreen}
              options={{headerShown: false}}
            />
            <Stack.Screen
              name={'RegisterClient'}
              component={RegisterClientScreen}
              options={{headerShown: false}}
            />
            <Stack.Screen
              name={'RegisterCompany'}
              component={RegisterCompanyScreen}
              options={{headerShown: false}}
            />
          </Stack.Navigator>
        ) : state == State.Client ? (
          <>
            <Stack.Navigator>
              <Stack.Screen
                name={'Tab'}
                component={ClientTabComponent}
                options={{headerShown: false}}
              />
              <Stack.Screen
                name={'Map'}
                component={MapScreen}
                options={{headerShown: false}}
              />
              <Stack.Screen
                name={'CreateOrderRequest'}
                component={gestureHandlerRootHOC(CreateOrderRequestScreen)}
                options={{headerShown: false}}
              />
              <Stack.Screen
                name={'EditOrderRequest'}
                component={gestureHandlerRootHOC(EditOrderRequestScreen)}
                options={{headerShown: false}}
              />
            </Stack.Navigator>
          </>
        ) : state == State.Company ? (
          <>
            <Stack.Navigator>
              <Stack.Screen
                name={'Tab'}
                component={CompanyTabComponent}
                options={{headerShown: false}}/>
              <Stack.Screen
                name={'ImageView'}
                component={ImageViewScreen}
                options={{headerShown: false}}/>
              <Stack.Screen
                name={'CreateOrderResponse'}
                component={CreateOrderResponseScreen}
                options={{headerShown: false}}/>
            </Stack.Navigator>
          </>) : state == State.User ? (
          <>
            <Stack.Navigator>
              <Stack.Screen
                name={'FillData'}
                component={gestureHandlerRootHOC(FillDataScreen)}
                options={{headerShown: false}}
              />
            </Stack.Navigator>
          </>
        ) : state == State.Unsubscribe ? (
          <>
            <Stack.Navigator>
              <Stack.Screen
                name={'SubscriptionPlans'}
                component={SubscriptionPlansScreen}
                options={{headerShown: false}}
              />
              <Stack.Screen
                name={'PaySubscription'}
                component={PaySubscriptionScreen}
                options={{headerShown: false}}
              />
            </Stack.Navigator>
          </>) : (
          <>
            <Stack.Navigator>
              <Stack.Screen
                name="Loading"
                component={LoadingScreen}
                options={{headerShown: false}}
              />
            </Stack.Navigator>
          </>
        )}
      </NavigationContainer>
    </AuthorizedContextProvider>
  );
}

export default App;
