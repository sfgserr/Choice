import * as React from 'react';
import {MMKVLoader, useMMKVStorage} from 'react-native-mmkv-storage';
import {UserService} from './services/UserService.ts';
import { AccountManager } from './AccountManager.ts';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import CategoriesScreen from './screens/tab/CategoriesScreen.tsx';
import LoginScreen from './screens/LoginScreen.tsx';
import LoadingScreen from './screens/LoadingScreen.tsx';
import {ObjectGraph} from './di/ObjectGraph.ts';
import {
  createBottomTabNavigator,
} from '@react-navigation/bottom-tabs';
import OrderRequestsScreen from './screens/tab/OrderRequestsScreen.tsx';
import ChatScreen from './screens/tab/ChatScreen.tsx';
import AccountScreen from './screens/tab/AccountScreen.tsx';
import {ClientTabProps, StackProps} from './types/NavigationTypes.ts';
import {
  Image
} from 'react-native';
import {CategoriesService} from './services/CategoriesService.ts';
import {Auth} from './types/AppTypes.ts';
import {State} from './enums/AppEnums.ts';
import {Status} from './enums/AccountManagerEnums.ts';
import {UserType} from './enums/ModelEnums.ts';
import {TokenStorageService} from './services/TokenStorageService.ts';
export const AuthContext = React.createContext<Auth>({
  signIn: (accessToken, refreshToken) => {},
  signOut: () => {},
  restore: () => {}
});

function App(): React.JSX.Element {
  const graph = new ObjectGraph();
  graph.initialize();

  const userService: UserService = graph.resolve<UserService>("UserService");
  const accountManager: AccountManager = graph.resolve<AccountManager>("AccountManager");
  const tokenStorageService: TokenStorageService = graph.resolve<TokenStorageService>("TokenStorageService");

  const [state, setState] = React.useState(State.Restoring);

  const authContext = React.useMemo(
    () => ({
      signIn: (accessToken: string, refreshToken: string) => {
        async function setTokens() {
          await tokenStorageService.setTokensToStorage(accessToken, refreshToken);
        }
        setTokens();
        let user = userService.getUser();

        setState(user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin)
      },
      signOut: () => {
        async function setTokens() {
          await tokenStorageService.setTokensToStorage('token', 'token');
        }
        setTokens();
        userService.signOut();

        setState(State.SignOut);
      },
      restore: () => {
        setState(State.Restoring);
      }
    }),
    [userService, tokenStorageService]
  );

  React.useEffect(() => {
    const getState = async () => {
      const state = await accountManager.getState();
      setState(state);
    }

    getState();
  }, []);

  const Stack = createNativeStackNavigator<StackProps>();
  const ClientTab = createBottomTabNavigator<ClientTabProps>();

  const getTabBarIcon = ({size, focused, color, source} : {
    size: number,
    focused: boolean,
    color: string,
    source: any,

  }) => {
    return <Image
      source={source}
      style={{
        width: 20,
        height: 20,
        tintColor: focused ? '#2975CC' : '#99A2AD',
        resizeMode: 'contain'
      }}/>
  };

  return (
    <AuthContext.Provider value={authContext}>
      <NavigationContainer>
        {state == State.SignOut ? (
          <Stack.Navigator>
            <Stack.Screen
              name="Login"
              component={LoginScreen}
              initialParams={{userService}}
              options={{headerShown: false}}
            />
          </Stack.Navigator>
        ) : state == State.Client ? (
          <>
            <ClientTab.Navigator>
              <ClientTab.Screen
                name={'Categories'}
                component={CategoriesScreen}
                initialParams={{categoriesService: graph.resolve<CategoriesService>("CategoriesService")}}
                options={{
                  headerShown: false,
                  tabBarLabel: 'Услуги',
                  tabBarIcon: ({size, focused, color}) =>
                    getTabBarIcon({size, focused, color, source: require('./assets/images/categories.png')}),
                }}
              />
              <ClientTab.Screen
                name={'OrderRequests'}
                component={OrderRequestsScreen}
                options={{
                  headerShown: false,
                  tabBarLabel: 'Заказы',
                  tabBarIcon: ({size, focused, color}) =>
                    getTabBarIcon({size, focused, color, source: require('./assets/images/orders.png')}),
                }}
              />
              <ClientTab.Screen
                name={'Chat'}
                component={ChatScreen}
                options={{
                  headerShown: false,
                  tabBarLabel: 'Чат',
                  tabBarIcon: ({size, focused, color}) =>
                    getTabBarIcon({size, focused, color, source: require('./assets/images/chat.png')}),
                }}
              />
              <ClientTab.Screen
                name={'Account'}
                component={AccountScreen}
                options={{
                  headerShown: false,
                  tabBarLabel: 'Аккаунт',
                  tabBarIcon: ({size, focused, color}) =>
                    getTabBarIcon({size, focused, color, source: require('./assets/images/account.png')}),
                }}
              />
            </ClientTab.Navigator>
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
