/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */

import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import LoginScreen from './Screens/LoginScreen';
import CategoryScreen from './Screens/CategoryScreen';
import * as KeyChain from 'react-native-keychain';
import {
  SafeAreaView,
  ScrollView,
  StatusBar,
  StyleSheet,
  Text,
  useColorScheme,
  View,
} from 'react-native';

const Stack = createNativeStackNavigator();
export const AuthContext = React.createContext();

function App() {
  const authContext = React.useMemo(() => ({
    signIn: () => {
      setIsSignedIn(true);
    }
  }));

  const [isSignedIn, setIsSignedIn] = React.useState(false);

  return (
    <NavigationContainer>
      <AuthContext.Provider value={authContext}>
        <Stack.Navigator>
          {
            !isSignedIn ? 
            <Stack.Screen name="Login"
                          component={LoginScreen}
                          options={{headerShown: false}}/>
            :
            <Stack.Screen name="Category"
                          component={CategoryScreen}
                          options={{headerShown: false}}/>
          }
        </Stack.Navigator>
      </AuthContext.Provider>
    </NavigationContainer>
  );
}

export default App;
