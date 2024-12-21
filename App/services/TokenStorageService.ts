import {MMKVInstance, MMKVLoader} from 'react-native-mmkv-storage';
import {HttpService} from './HttpService.ts';

export class TokenStorageService {
  private readonly loader: MMKVInstance;

  constructor() {
    this.loader = new MMKVLoader().withInstanceID('mmkvInstance').withEncryption().initialize();
  }

  async setTokensToStorage(accessToken: string, refreshToken: string) {
    await this.loader.setStringAsync('access_token', accessToken);
    await this.loader.setStringAsync('refresh_token', refreshToken);

    HttpService.getInstance().setToken(accessToken);
  }

  async getTokens(): Promise<string[]> {
    let accessToken = await this.loader.getStringAsync('access_token');
    let refreshToken = await this.loader.getStringAsync('refresh_token');

    if (accessToken != undefined && refreshToken != undefined) {
      return [accessToken, refreshToken];
    }

    return [];
  }
}
