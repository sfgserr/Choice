export class HttpService {
  private static instance: HttpService;

  private readonly baseUrl: string;
  private accessToken: string;

  private constructor(baseUrl: string) {
    this.accessToken = '';
    this.baseUrl = baseUrl;
  }

  public static getInstance() {
    if (!this.instance)
      this.instance = new HttpService(`${process.env.API_URL}/api`);

    return this.instance;
  }

  setToken(accessToken: string) {
    this.accessToken = accessToken;
  }

  async request(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined,
    signOut: () => void
  ) {
    const token = `Bearer ${this.accessToken}`;
    const response = await fetch(`${this.baseUrl}/${endPoint}`, {
      method,
      headers: {
        'Authorization': token,
        'Content-Type': 'application/json'
      },
      body});

    if (response.status == 401)
      signOut();

    return response;
  }
}
