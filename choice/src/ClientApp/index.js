/**
 * @format
 */

import {AppRegistry} from 'react-native';
import App from './App';
import {name as appName} from './app.json';
import PushNotification from "react-native-push-notification";
import * as KeyChain from 'react-native-keychain';

PushNotification.configure({
    onRegister: async function (token) {
        console.log("TOKEN: ", token);
        await KeyChain.setGenericPassword('device_token', token.token);
    },
    onNotification: function(notification) {
        console.log("NOT: ", notification);
    }
});

AppRegistry.registerComponent(appName, () => App);
