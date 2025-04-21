import {SignalRWebSocketConnection} from './SignalRWebSocketConnection.ts';

export class SignalRClient {
  private connection: SignalRWebSocketConnection;

  public constructor() {
    this.connection = new SignalRWebSocketConnection('wss://choice.ru:8083/chat');
  }

  public async start(token: string) {
    await this.connection.start(token, (data) => {
      console.log(data);
    });
  }

  public stop() : void {
    this.connection.stop().catch((err) => {});
  }
}
