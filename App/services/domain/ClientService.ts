import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';

export class ClientService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async create(
    name: string,
    password: string,
    email: string,
    phoneNumber: string,
    city: string,
    street: string) {
    return await this.httpService.request(
      'clients',
      'POST',
      JSON.stringify({
        name,
        password,
        email,
        phoneNumber,
        city,
        street
      }));
  }

  async changeIconUri(
    iconUri: string) {
    return await this.httpService.request(
      `clients/${iconUri}`,
      'PUT',
      undefined);
  }
}
