
export class HttpService {
  private readonly baseUrl: string;
  private accessToken: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
    this.accessToken = '';
  }

  setToken(accessToken: string) {
    this.accessToken = accessToken;
  }

  async request(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined,
  ) {
    const token = `Bearer ${this.accessToken}`;
    return await fetch(`${this.baseUrl}/${endPoint}`, {
      method,
      headers: {
        'Authorization': token,
        'Content-Type': 'application/json'
      },
      body});
  }
}
