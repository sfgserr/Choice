export class SignalRWebSocketConnection {
  private webSocket: WebSocket | null = null;
  private hubEndPoint: string;

  public constructor(endPoint: string) {
    this.hubEndPoint = endPoint;
  }

  public async start(token: string, onmessage: (message: any) => void): Promise<void> {
    await this.negotiateSignalRConnection(token)
      .then(res => this.connectToWebSocket(res, token, onmessage))
      .catch(error => console.log('Error:', error));
  }

  public async stop(): Promise<void> {
    this.webSocket?.close();
  }

  private async negotiateSignalRConnection(token: string) {
    const negotiateUrl = `${process.env.API_URL}/chat/negotiate?negotiateVersion=1`;
    console.log(`Negotiating at: ${negotiateUrl}`);

    const options = {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
    };

    try {
      const response = await fetch(negotiateUrl, options);
      console.log('Negotiation response status:', response.status);
      const jsonResponse = await response.json();
      console.log('Negotiation response body:', jsonResponse);
      return jsonResponse;
    } catch (error) {
      console.error('Negotiation error:', error);
      throw error;
    }
  }

  private connectToWebSocket(negotiationResponse: any, token: string, onmessage: (data: any) => void): void {
    const connectionToken = encodeURIComponent(
      negotiationResponse.connectionToken,
    );
    const webSocketUrl = `${this.hubEndPoint}?connectionToken=${connectionToken}&transport=WebSockets`;

    console.log(`Connecting to WebSocket at: ${webSocketUrl}`);

    const ws = new WebSocket(webSocketUrl, [], {
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    ws.onmessage = ((message: WebSocketMessageEvent) => {
      console.log(`Received message`);
      onmessage(message.data);
    });
    ws.onerror = ((event) => {
      console.error('Error:', event.message);
    });
    ws.onclose = event => {
      console.log('Connection closed');
      console.log(event.message);
    };

    this.webSocket = ws;
  }
}
