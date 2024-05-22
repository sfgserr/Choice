import * as SignalR from '@microsoft/signalr'
import env from '../env';
import 'react-native-url-polyfill/auto';
import {
    DeviceEventEmitter
} from 'react-native';

let builder = new SignalR.HubConnectionBuilder();
let connection = builder.withUrl(`${env.api_url}/chat`).build();

const build = (token) => {
    connection = builder
        .withUrl(`${env.api_url}/chat`, { accessTokenFactory: () => token })
        .build();

    connection.on('orderCreated', message => {
        DeviceEventEmitter.emit('orderCreated', message);
    });
}

const start = async () => {
    await connection.start();
}

const stop = async () => {
    await connection.stop();
}

export default {
    build,
    start,
    stop
}