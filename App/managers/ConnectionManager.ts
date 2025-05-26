import {IConnection} from '../realTime/connections/IConnection.ts';
import {HubConnectionAdapter} from '../realTime/connections/HubConnectionAdapter.ts';
import {DeviceEventEmitter} from 'react-native';
import {Message} from '../types/DomainTypes.ts';
import {AuthService} from '../services/auth/AuthService.ts';
import {TokenStorageService} from '../services/object/TokenStorageService.ts';

export class ConnectionManager {
  private static connection: IConnection | null = null;

  private constructor() {
  }

  public static async init(accessToken: string, authService: AuthService, tokenService: TokenStorageService): Promise<void> {
    if (this.connection == null) {
      this.connection = new HubConnectionAdapter(accessToken, tokenService, authService);

      this.connection.on('messageSent', (message: Message) => {
        DeviceEventEmitter.emit('messageSent', message);
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
    this.connection?.close();
  }
}
