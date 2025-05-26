import {IConnection} from './IConnection.ts';
import {HubConnectionBuilder, LogLevel, HubConnection} from '@microsoft/signalr';
import {setupURLPolyfill} from 'react-native-url-polyfill';
import {TokenStorageService} from '../../services/object/TokenStorageService.ts';
import {AuthService} from '../../services/auth/AuthService.ts';
import {jwtDecode} from 'jwt-decode';
import {Alert} from 'react-native';

export class HubConnectionAdapter implements IConnection {
  private connection: HubConnection;
  private readonly tokenStorageService: TokenStorageService;
  private readonly authService: AuthService;

  public constructor(tokenService: TokenStorageService, authService: AuthService) {
    setupURLPolyfill();

    this.tokenStorageService = tokenService;
    this.authService = authService;

    this.connection = new HubConnectionBuilder().withUrl(
      `${process.env.API_URL}/chat`,
      {accessTokenFactory: async () => await this.getToken()})
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

  private async getToken() {
    const tokens = await this.tokenStorageService.getTokens();

    const accessToken = jwtDecode(tokens[0]);

    if (accessToken.exp && Date.now() / 1000 > accessToken.exp) {
      const refreshedTokens = await this.authService.refresh(tokens[1]);

      if (refreshedTokens) {
        await this.tokenStorageService
          .setTokensToStorage(refreshedTokens.access_token, refreshedTokens.refresh_token);

        return refreshedTokens.access_token;
      } else {
        Alert.alert('Не удалось подключится');
        return '';
      }
    }

    return tokens[0];
  }
}
