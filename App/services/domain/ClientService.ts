import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../../enums/AppEnums.ts';

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
    street: string,
    changeState: (state: State) => void) {
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
      }),
      changeState);
  }
}
