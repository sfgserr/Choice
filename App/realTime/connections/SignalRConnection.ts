import {IConnection} from './IConnection.ts';
import {SignalRClient} from '../signalr/SignalRClient.ts';

export class SignalRConnection implements IConnection {
  private readonly client: SignalRClient;
  private readonly token: string;

  public constructor(client: SignalRClient, token: string) {
    this.client = client;
    this.token = token;
  }

  public async start(): Promise<void> {
    await this.client.start(this.token);
  }
  close(): void {
    this.client.stop();
  }
  on(method: string, handler: (obj: any) => void): void {

  }
}
