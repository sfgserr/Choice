import * as React from 'react';
import {TokenService} from './services/TokenService.ts';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import LoginScreen from './screens/LoginScreen.tsx';
import LoadingScreen from './screens/LoadingScreen.tsx';
import {ObjectGraph} from './di/ObjectGraph.ts';
import {StackProps} from './types/NavigationTypes.ts';
import {Auth} from './types/AppTypes.ts';
import {State} from './enums/AppEnums.ts';
import {StateManager} from './StateManager.ts';
import MapScreen from './screens/MapScreen.tsx';
import ClientTabComponent from './Tabs/ClientTabComponent.tsx';
import {CompanyService} from './services/CompanyService.ts';

export const AuthContext = React.createContext<Auth>({
  signIn: (accessToken, refreshToken) => {},
  signOut: () => {},
  changeState: (state: State) => {}
});

function App(): React.JSX.Element {
  const graph = new ObjectGraph();
  graph.initialize();

  const stateManager: StateManager = graph.resolve<StateManager>("StateManager");

  const [state, setState] = React.useState(State.Restoring);

  const authContext = React.useMemo(
    () => ({
      signIn: (accessToken: string, refreshToken: string) => {
        async function setTokens() {
          const state = await stateManager.signIn(accessToken, refreshToken);
          setState(state);
        }
        setTokens();
      },
      signOut: () => {
        async function setTokens() {
          const state = await stateManager.signOut();
          setState(state);
        }
        setTokens();
      },
      changeState: (state: State) => {
        setState(state);
      }
    }),
    [stateManager]
  );

  React.useEffect(() => {
    const getState = async () => {
      const state = await stateManager.getState();
      setState(state);
    }

    getState();
  }, []);

  const Stack = createNativeStackNavigator<StackProps>();

  return (
    <AuthContext.Provider value={authContext}>
      <NavigationContainer>
        {state == State.SignOut ? (
          <Stack.Navigator>
            <Stack.Screen
              name="Login"
              component={LoginScreen}
              initialParams={{tokenService: graph.resolve<TokenService>("TokenService")}}
              options={{headerShown: false}}
            />
          </Stack.Navigator>
        ) : state == State.Client ? (
          <>
            <Stack.Navigator>
              <Stack.Screen
                name={'Tab'}
                component={ClientTabComponent}
                initialParams={{graph}}
                options={{headerShown: false}}/>
              <Stack.Screen
                name={'Map'}
                component={MapScreen}
                options={{headerShown: false}}
                initialParams={{companyService: graph.resolve<CompanyService>("CompanyService")}}/>
            </Stack.Navigator>
          </>
        ) : (
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
    </AuthContext.Provider>
  );
}

export default App;
