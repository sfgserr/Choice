import {IConnection} from './IConnection.ts';
import {HubConnectionBuilder, LogLevel, HubConnection} from '@microsoft/signalr';
import {setupURLPolyfill} from 'react-native-url-polyfill';

export class HubConnectionAdapter implements IConnection {
  private connection: HubConnection;

  public constructor(accessToken: string) {
    setupURLPolyfill();

    this.connection = new HubConnectionBuilder().withUrl(
      `${process.env.API_URL}/chat`,
      {accessTokenFactory: () => accessToken})
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Debug)
      .build();
  }

  async start(): Promise<void> {
    await this.connection.start();
  }
  close(): void {
    this.connection.stop().catch((err: Error) => {});
  }
  on(method: string, handler: (obj: any) => void): void {
    this.connection.on(method, handler);
  }
}
