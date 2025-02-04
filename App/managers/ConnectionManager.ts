import {HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import {Alert, DeviceEventEmitter} from 'react-native';
import {setupURLPolyfill} from 'react-native-url-polyfill';

export class ConnectionManager {
  private static connection: HubConnection | null = null;

  private constructor() {
  }

  public static async init(accessToken: string) {
    setupURLPolyfill();

    if (this.connection == null) {
      this.connection = new HubConnectionBuilder().withUrl(
        'http://127.0.0.1:8080/chat',
        {accessTokenFactory: () => accessToken}).build();

      this.connection.on('messageSent', (message: any) => {
        DeviceEventEmitter.emit('messageSent', message);
      })

      this.connection.onclose(error => Alert.alert('Ошибка', error?.message, [{text: 'Ок'}]))

      await this.connection.start();
    }
  }
}
