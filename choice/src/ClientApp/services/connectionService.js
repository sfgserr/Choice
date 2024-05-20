import * as SignalR from '@microsoft/signalr'
import env from '../env';
import 'react-native-url-polyfill/auto';
import {
    DeviceEventEmitter
} from 'react-native';

export default class ConnectionSerivce {
    build(token) {
        this.connection = new SignalR.HubConnectionBuilder()
            .withUrl(`${env.api_url}/chat`, { accessTokenFactory: () => token })
            .build();

        this.connection.on("orderCreated", message => {
            DeviceEventEmitter.emit('orderCreated', message);
        });
    }

    async start() {
        await this.connection.start();
    }

    async stop() {
        await this.connection.stop();
    }
}