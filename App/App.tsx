import * as React from 'react';
import {MMKVLoader, useMMKVStorage} from 'react-native-mmkv-storage';
import UserService from './services/UserService.ts';
import {UserType} from './models/User.ts';
import AccountManager, {Status} from './AccountManager.ts';
import {NavigationContainer} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import Login from './screens/Login.tsx';
import Categories from './screens/Categories.tsx';
import {Text, View} from 'react-native';

const storage = new MMKVLoader().withEncryption().initialize();

type Auth = {
  signIn: (accessToken: string, refreshToken: string) => void;
  signOut: () => void;
}

export const AuthContext = React.createContext<Auth | null>(null);

enum State {
  SignOut,
  Restoring,
  Client,
  Company,
  Admin
}

function App(): React.JSX.Element {
  const [accessToken, setAccessToken] = useMMKVStorage('accessToken', storage, 'token');
  const [refreshToken, setRefreshToken] = useMMKVStorage('refreshToken', storage, 'refresh');

  const [state, setState] = React.useState(State.Restoring);

  const authContext = React.useMemo(
    () => ({
      signIn: (accessToken: string, refreshToken: string) => {
        setAccessToken(accessToken);
        setRefreshToken(refreshToken);

        let user = UserService.getUser();

        setState(user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin)
      },
      signOut: () => {
        setAccessToken('token');
        setRefreshToken('refresh');

        UserService.signOut();

        setState(State.SignOut);
      }
    }),
    [setAccessToken, setRefreshToken]
  );

  React.useEffect(() => {
    const fetchAccount = async () => {
      let result = await AccountManager.fetchAccount(accessToken, refreshToken);

      if (result.status == Status.Successful) {
        let user = UserService.getUser();

        setState(user.userType == UserType.Client ? State.Client : user.userType == UserType.Company ? State.Company : State.Admin)

        setAccessToken(result.tokens[0]);
        setRefreshToken(result.tokens[1]);
      }
      else {
        setState(State.SignOut);
      }
    }

    fetchAccount();
  }, [accessToken, refreshToken, setAccessToken, setRefreshToken]);

  type StackProps = {
    Login: undefined,
    Categories: undefined,
    RegisterClient: undefined,
    RegisterCompany: undefined,
    Admin: undefined,
  }

  const Stack = createNativeStackNavigator<StackProps>();

  return (
    <AuthContext.Provider value={authContext}>
      <NavigationContainer>
        <Stack.Navigator>
          {state == State.SignOut ? (
            <Stack.Screen name={'Login'} component={Login}/>
          ) : state == State.Client ? (
            <Stack.Screen name={'Categories'} component={Categories}/>
          ) : (
            <View
              style={{
                flex: 1,
                backgroundColor: 'white',
              }}>
              <Text
                style={{
                  alignSelf: 'center',
                  fontSize: 20,
                  color: 'black'
                }}>
                Loading
              </Text>
            </View>
          )}
        </Stack.Navigator>
      </NavigationContainer>
    </AuthContext.Provider>
  );
}

export default App;
