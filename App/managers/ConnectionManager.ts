import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr';
import {Alert, DeviceEventEmitter} from 'react-native';
import {setupURLPolyfill} from 'react-native-url-polyfill';
import {Message} from '../types/DomainTypes.ts';

export class ConnectionManager {
  private static connection: HubConnection | null = null;

  private constructor() {
  }

  public static async init(accessToken: string) {
    setupURLPolyfill();

    if (this.connection == null) {
      this.connection = new HubConnectionBuilder().withUrl(
        'https://choice-api.ru/chat',
        {accessTokenFactory: () => accessToken})
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Debug)
        .build();

      this.connection.on('messageSent', (message: Message) => {
        DeviceEventEmitter.emit('messageSent', message);
      });

      this.connection.onclose(error => {
        if (error != undefined) {
          Alert.alert('Ошибка', error.message, [{info: 'Ок'}]);
        }
      });

      this.connection.on('read', (data: string) => {
        DeviceEventEmitter.emit('read', data);
      });

      this.connection.on('orderSent', (data: any) => {
        DeviceEventEmitter.emit('orderSent', data);
      });

      await this.connection.start();
    }
  }

  public static async disconnect(): Promise<void> {
    if (this.connection != null) {
      await this.connection.stop();
      this.connection = null;
    }
  }
}
