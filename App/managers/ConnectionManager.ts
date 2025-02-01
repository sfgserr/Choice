import {HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import {DeviceEventEmitter} from 'react-native';

export class ConnectionManager {
  private static connection: HubConnection | null = null;

  private constructor() {
  }

  public static async init(accessToken: string) {
    if (this.connection == null) {
      this.connection = new HubConnectionBuilder().withUrl(
        `${process.env.API_URL}/chat`,
        {accessTokenFactory: () => accessToken}).build();

      this.connection.on('messageSent', (message: any) => {
        DeviceEventEmitter.emit('messageSent', message);
      })
    }
  }
}
