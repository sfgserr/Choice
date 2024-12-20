import * as React from 'react';
import {MMKVLoader, useMMKVStorage} from 'react-native-mmkv-storage';
import {UserService} from './services/UserService.ts';
import {UserType} from './models/User.ts';
import { AccountManager, Status } from './AccountManager.ts';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import CategoriesScreen from './screens/tab/CategoriesScreen.tsx';
import LoginScreen from './screens/LoginScreen.tsx';
import LoadingScreen from './screens/LoadingScreen.tsx';
import {ObjectGraph} from './di/ObjectGraph.ts';
import {
  BottomTabNavigationOptions,
  createBottomTabNavigator,
} from '@react-navigation/bottom-tabs';
import OrderRequestsScreen from './screens/tab/OrderRequestsScreen.tsx';
import ChatScreen from './screens/tab/ChatScreen.tsx';
import AccountScreen from './screens/tab/AccountScreen.tsx';
import {ClientTabProps, StackProps} from './types/NavigationTypes.ts';
import {
  Image
} from 'react-native';
import {HttpService} from './services/HttpService.ts';
import {CategoriesService} from './services/CategoriesService.ts';

const storage = new MMKVLoader().withEncryption().initialize();

type Auth = {
  signIn: (accessToken: string, refreshToken: string) => void;
  signOut: () => void;
  restore: () => void;
}

export const AuthContext = React.createContext<Auth>({
  signIn: (accessToken, refreshToken) => {},
  signOut: () => {},
  restore: () => {}
});

enum State {
  SignOut,
  Restoring,
  Client,
  Company,
  Admin
}

function start(): ObjectGraph {
  process.env.NODE_TLS_REJECT_UNAUTHORIZED='0';

  const graph = new ObjectGraph();
  graph.initialize();

  return graph;
}

function App(): React.JSX.Element {
  const graph = start();

  const userService: UserService = graph.resolve<UserService>("UserService");
  const accountManager: AccountManager = graph.resolve<AccountManager>("AccountManager");

  const [accessToken, setAccessToken] = useMMKVStorage('accessToken', storage, 'token');
  const [refreshToken, setRefreshToken] = useMMKVStorage('refreshToken', storage, 'refresh');
  const [state, setState] = React.useState(State.Restoring);

  const authContext = React.useMemo(
    () => ({
      signIn: (accessToken: string, refreshToken: string) => {
        setAccessToken(accessToken);
        setRefreshToken(refreshToken);
        graph.resolve<HttpService>("HttpService").setToken(accessToken);
        let user = userService.getUser();

        setState(user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin)
      },
      signOut: () => {
        setAccessToken('token');
        setRefreshToken('refresh');

        userService.signOut();

        setState(State.SignOut);
      },
      restore: () => {
        setState(State.Restoring);
      }
    }),
    [setAccessToken, setRefreshToken, userService]
  );

  React.useEffect(() => {
    const fetchAccount = async () => {
      let result = await accountManager.fetchAccount(accessToken, refreshToken);

      if (result.status == Status.Successful) {
        let user = userService.getUser();

        setState(user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin)

        setAccessToken(result.tokens[0]);
        setRefreshToken(result.tokens[1]);
        graph.resolve<HttpService>("HttpService").setToken(result.tokens[0]);
      }
      else {
        setState(State.SignOut);
      }
    }

    fetchAccount();
  }, [accessToken, refreshToken, setAccessToken, setRefreshToken, state, setState, accountManager, userService]);

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
