/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */

import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { gestureHandlerRootHOC } from 'react-native-gesture-handler';
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
  Image
} from 'react-native';
import OrderScreen from './Screens/OrderScreen';
import { Icon } from 'react-native-elements';
import ChatScreen from './Screens/ChatScreen';
import AccountScreen from './Screens/AccountScreen';
import MapScreen from './Screens/MapScreen';
import categoryStore from './services/categoryStore';
import userStore from './services/userStore';
import OrderRequestCreationScreen from './Screens/OrderRequestCreationScreen';
import OrderRequestScreen from './Screens/OrderRequestScreen';
import ChangePasswordScreen from './Screens/ChangePasswordScreen';
import RegisterScreen from './Screens/RegisterScreen';
import FillCompanyDataScreen from './Screens/FillCompanyDataScreen';

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

export const AuthContext = React.createContext();

const getTabLabel = (routeName) => {
  switch (routeName) {
    case 'Category':
      return 'Услуги';
    case 'Order':
      return 'Заказы';
    case 'Chat':
      return 'Чат';
    case 'Account':
      return 'Аккаунт';
  }
}

function ClientTab() {
  return (
    <Tab.Navigator screenOptions={({route}) => ({
      tabBarIcon: ({focused, color, size}) => {
        let iconSrc;

        if (route.name == 'Category') {
          iconSrc = 'category';
        }

        if (route.name == 'Account') {
          iconSrc = 'account-circle';
        }

        if (route.name == 'Chat') {
          iconSrc = 'chat';
        }

        if (route.name == 'Order') {
          iconSrc = 'shopping-basket';
        }

        let iconColor = focused ? '#2975CC' : '#99A2AD';

        return <Icon size={25} name={iconSrc} type='material' color={iconColor}/>
      },
      tabBarActiveTintColor: '#2975CC',
      tabBarInactiveTintColor: '#99A2AD',
      tabBarLabel: getTabLabel(route.name)
  })}>
      <Tab.Screen name="Category"
                  component={CategoryScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Order"
                  component={OrderScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Chat"
                  component={ChatScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Account"
                  component={AccountScreen}
                  options={{headerShown: false}}/>
    </Tab.Navigator>
  );
}

function App() {
  const authContext = React.useMemo(() => ({
    signIn: async (userType) => {
      await categoryStore.retrieveData();
      await userStore.retrieveData(userType);

      setUserType(userType);
      setIsSignedIn(true);
    },
    signOut: () => {
      userStore.logout();

      setIsSignedIn(false);
      setUserType(0);
    }
  }));

  const [isSignedIn, setIsSignedIn] = React.useState(false);
  const [userType, setUserType] = React.useState(0);

  return (
    <NavigationContainer>
      <AuthContext.Provider value={authContext}>
        {
          !isSignedIn ? (
            <>
              <Stack.Navigator>
                <Stack.Screen name="Login"
                              component={gestureHandlerRootHOC(LoginScreen)}
                              options={{headerShown: false}}/>
                <Stack.Screen name="Register"
                              component={RegisterScreen}
                              options={{headerShown: false}}/>
                <Stack.Screen name="FillCompanyData"
                              component={FillCompanyDataScreen}
                              options={{headerShown: false}}/>
              </Stack.Navigator>
            </>
          ) : userType == 1 ? (
            <>
             <Stack.Navigator>
                <Stack.Screen name="Tab"
                              component={ClientTab}
                              options={{headerShown: false}}/>
                <Stack.Screen name="Map"
                              component={MapScreen}
                              options={{headerShown:false}}/>
                <Stack.Screen name="OrderRequestCreation"
                              component={gestureHandlerRootHOC(OrderRequestCreationScreen)}
                              options={{headerShown:false}}/>
                <Stack.Screen name="OrderRequest"
                              component={gestureHandlerRootHOC(OrderRequestScreen)}
                              options={{headerShown:false}}/>
                <Stack.Screen name="ChangePassword"
                              component={ChangePasswordScreen}
                              options={{headerShown:false}}/>
             </Stack.Navigator>
            </>
          ) : (
            <>
             <Tab.Navigator screenOptions={({route}) => ({
              tabBarIcon: ({focused, color, size}) => {
                let iconSrc;

                if (route.name == 'Account') {
                  iconSrc = require("./assets/account.png");
                }

                if (route.name == 'Chat') {
                  iconSrc = require("./assets/chat.png");
                }

                if (route.name == 'Order') {
                  iconSrc = require("./assets/category.png");
                }

                let iconColor = focused ? '#2975CC' : '#99A2AD';

                return <Image style={{height: 25, width: 25}} source={iconSrc} tintColor={iconColor}/>
              },
              tabBarActiveTintColor: '#2975CC',
              tabBarInactiveTintColor: '#99A2AD',
              tabBarLabel: getTabLabel(route.name)
          })}>
              <Tab.Screen name="Order"
                          component={OrderScreen}
                          options={{headerShown: false}}/>
              <Tab.Screen name="Chat"
                          component={ChatScreen}
                          options={{headerShown: false}}/>
              <Tab.Screen name="Account"
                          component={AccountScreen}
                          options={{headerShown: false}}/>
            </Tab.Navigator>
            </>
          )
        }
      </AuthContext.Provider>
    </NavigationContainer>
  );
}

export default App;
