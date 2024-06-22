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
  Image,
  DeviceEventEmitter,
  AppState
} from 'react-native';
import OrderScreen from './Screens/OrderScreen';
import { Icon } from 'react-native-elements';
import ChatsScreen from './Screens/ChatsScreen';
import AccountScreen from './Screens/AccountScreen';
import MapScreen from './Screens/MapScreen';
import categoryStore from './services/categoryStore';
import userStore from './services/userStore';
import OrderRequestCreationScreen from './Screens/OrderRequestCreationScreen';
import OrderRequestScreen from './Screens/OrderRequestScreen';
import ChangePasswordScreen from './Screens/ChangePasswordScreen';
import RegisterScreen from './Screens/RegisterScreen';
import FillCompanyDataScreen from './Screens/FillCompanyDataScreen';
import CompanyRequestsScreen from './Screens/CompanyRequestsScreen';
import CompanyRequestCreationScreen from './Screens/CompanyRequestCreationScreen';
import env from './env';
import connectionService from './services/connectionService';
import ChatScreen from './Screens/ChatScreen';
import CompanyAccountScreen from './Screens/CompanyAccountScreen';
import chatService from './services/chatService';
import AdminScreen from './Screens/AdminScreen';

const Stack = createNativeStackNavigator();
const Tab = createBottomTabNavigator();

let unreadMessagesCount = 0;

export const AuthContext = React.createContext();

const getTabLabel = (routeName) => {
  switch (routeName) {
    case 'Category':
      return 'Услуги';
    case 'Order':
      return 'Заказы';
    case 'Chats':
      return 'Чат';
    case 'Account':
      return 'Аккаунт';
  }
}

function CompanyTab() {
  const [unreadMessagesCount, setUnreadMessagesCount] = React.useState(0);

  React.useEffect(() => {
    async function getUnreadMessages() {
      let chats = await chatService.getChats();

      let count = chats.flatMap(c => c.messages).filter(m => !m.isRead).length;

      setUnreadMessagesCount(count);
    }
    getUnreadMessages();
  }, []);

  const handleMessage = () => {
    setUnreadMessagesCount(prev => prev+1);
    console.log('receive');
  }

  const handleReadMessage = () => {
    setUnreadMessagesCount(prev => prev > 0 ? prev-1 : prev);
    console.log('read');
  }

  React.useEffect(() => {
    DeviceEventEmitter.addListener('tabMessageReceived', handleMessage);
    DeviceEventEmitter.addListener('tabRead', handleReadMessage);

    return () => {
      DeviceEventEmitter.removeAllListeners('tabMessageReceived');
      DeviceEventEmitter.removeAllListeners('tabRead');
    }
  }, [handleReadMessage,handleMessage]);

  return (
    <Tab.Navigator screenOptions={({route}) => ({
      tabBarIcon: ({focused, color, size}) => {
        let iconSrc;

        if (route.name == 'Account') {
          iconSrc = require("./assets/account.png");
        }

        if (route.name == 'Chats') {
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
      tabBarBadge: route.name != 'Chats' ? null : unreadMessagesCount == 0 ? null : unreadMessagesCount,
      tabBarLabel: getTabLabel(route.name)
  })}>
      <Tab.Screen name="Order"
                  component={CompanyRequestsScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Chats"
                  component={ChatsScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Account"
                  component={gestureHandlerRootHOC(CompanyAccountScreen)}
                  options={{headerShown: false}}/>
    </Tab.Navigator>
  )
}

function ClientTab() {
  const [unreadMessagesCount, setUnreadMessagesCount] = React.useState(0);

  React.useEffect(() => {
    async function getUnreadMessages() {
      let chats = await chatService.getChats();

      let count = chats.flatMap(c => c.messages).filter(m => !m.isRead && m.receiverId == userStore.get().guid).length;

      setUnreadMessagesCount(count);
    }
    getUnreadMessages();
  }, []);

  const handleMessage = () => {
    setUnreadMessagesCount(prev => prev+1);
    console.log('receive');
  }

  const handleReadMessage = () => {
    setUnreadMessagesCount(prev => prev > 0 ? prev-1 : prev);
    console.log('read');
  }

  React.useEffect(() => {
    DeviceEventEmitter.addListener('tabMessageReceived', handleMessage);
    DeviceEventEmitter.addListener('tabRead', handleReadMessage);

    return () => {
      DeviceEventEmitter.removeAllListeners('tabMessageReceived');
      DeviceEventEmitter.removeAllListeners('tabRead');
    }
  }, [handleReadMessage,handleMessage]);

  return (
    <Tab.Navigator screenOptions={({route}) => ({
      tabBarIcon: ({focused, color, size}) => {
        let iconSrc;

        if (route.name == 'Category') {
          iconSrc = require('./assets/category.png');
        }

        if (route.name == 'Account') {
          iconSrc = require('./assets/account.png');
        }

        if (route.name == 'Chats') {
          iconSrc = require('./assets/chat.png');
        }

        if (route.name == 'Order') {
          iconSrc = require('./assets/order.png');
        }

        let iconColor = focused ? '#2975CC' : '#99A2AD';

        return <Image 
                   style={{width: 30, height: 30}} 
                   resizeMode='contain' 
                   source={iconSrc} 
                   tintColor={iconColor}/>
      },
      tabBarActiveTintColor: '#2975CC',
      tabBarInactiveTintColor: '#99A2AD',
      tabBarBadge: route.name != 'Chats' ? null : unreadMessagesCount == 0 ? null : unreadMessagesCount,
      tabBarLabel: getTabLabel(route.name)
  })}>
      <Tab.Screen name="Category"
                  component={CategoryScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Order"
                  component={OrderScreen}
                  options={{headerShown: false}}/>
      <Tab.Screen name="Chats"
                  component={ChatsScreen}
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
      setUserType(userType);
      setIsSignedIn(true);

      if (userType != 3) {
        const key = await KeyChain.getGenericPassword();

        connectionService.build(key.password);

        await connectionService.start();
      }
    },
    signOut: async () => {
      userStore.logout();

      setIsSignedIn(false);
      setUserType(0);

      if (userStore.getUserType() != 3) {
        await connectionService.stop();
      }
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
                              component={gestureHandlerRootHOC(FillCompanyDataScreen)}
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
                              component={gestureHandlerRootHOC(MapScreen)}
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
                <Stack.Screen name="Chat"
                              component={gestureHandlerRootHOC(ChatScreen)}
                              options={{headerShown:false}}/>
             </Stack.Navigator>
            </>
          ) : userType == 2 ? (
            <>
              <Stack.Navigator>
                <Stack.Screen name="Tab"
                              component={CompanyTab}
                              options={{headerShown:false}}/>
                <Stack.Screen name="CompanyRequestCreation"
                              component={gestureHandlerRootHOC(CompanyRequestCreationScreen)}
                              options={{headerShown:false}}/>
                <Stack.Screen name="Chat"
                              component={gestureHandlerRootHOC(ChatScreen)}
                              options={{headerShown:false}}/>
                <Stack.Screen name="ChangePassword"
                              component={ChangePasswordScreen}
                              options={{headerShown:false}}/>   
              </Stack.Navigator> 
            </>
          ) : (
            <>
              <Stack.Navigator>
                <Stack.Screen name="Admin"
                              component={AdminScreen}
                              options={{headerShown:false}}/>  
              </Stack.Navigator>
            </>
          )
        }
      </AuthContext.Provider>
    </NavigationContainer>
  );
}

export default App;
