import RNFetchBlob from 'rn-fetch-blob';

type TokenResponse = {
  access_token: string;
  refresh_token: string;
}

export class AuthService {
  private readonly clientId: string;
  private readonly clientSecret: string;
  private readonly tokenEndpoint: string;

  constructor(tokenEndpoint: string, clientId: string, clientSecret: string) {
    this.tokenEndpoint = tokenEndpoint;
    this.clientId = clientId;
    this.clientSecret = clientSecret;
  }

  async login(email: string, password: string): Promise<TokenResponse | null> {
    var response = await fetch(this.tokenEndpoint, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: new URLSearchParams({
        username: email,
        password: password,
        grant_type: 'password',
        scope: 'offline_access',
        client_id: this.clientId,
        client_secret: this.clientSecret,
      }).toString(),
    });

    const status = response.status;

    if (status == 401) {
      return null;
    }

    return response.json();
  }

  async refresh(refreshToken: string): Promise<TokenResponse | null> {
    var response = await RNFetchBlob.config({trusty: true}).fetch('POST', this.tokenEndpoint,
      {
        'Content-Type': 'application/x-www-form-urlencoded'
      },
      new URLSearchParams({
        refresh_token: refreshToken,
        grant_type: 'refresh_token',
        scope: 'offline_access',
        client_id: this.clientId,
        clientSecret: this.clientSecret
      }).toString()
    );

    if (response.info().status != 200) {
      return null;
    }

    return response.json();
  }
}
