import {HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import {DeviceEventEmitter} from 'react-native';

export class ConnectionManager {
  private connection: HubConnection | null;

  constructor() {
    this.connection = null;
  }

  public async init(accessToken: string) {
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
