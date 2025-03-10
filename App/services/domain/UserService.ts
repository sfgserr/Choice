import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {TokenService} from '../auth/TokenService.ts';
import {UserType} from '../../enums/ModelEnums.ts';

export class UserService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;
  private readonly tokenService: TokenService;

  private user: any;

  constructor(
    httpService: RefreshTokenHttpServiceDecorator,
    tokenStore: TokenService) {
    this.httpService = httpService;
    this.tokenService = tokenStore;

    this.user = null;
  }

  private async fetchUser() {
    const response = await this.httpService.requestWithContent<any>(
      this.tokenService.getUser().userType == UserType.Client ? 'clients' : 'companies',
      'GET',
      undefined);

    if (response.content != null) this.user = response.content;
  }

  async getUser() {
    if (this.user == null)
      await this.fetchUser();

    return this.user;
  }

  async getUserWithoutCache() {
    await this.fetchUser();

    return this.user;
  }
}
