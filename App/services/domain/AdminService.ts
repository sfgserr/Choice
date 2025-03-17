import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {AdminUser} from '../../types/DomainTypes.ts';

export class AdminService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async getClients() {
    return await this.httpService.requestWithContent<AdminUser[]>(
      'admin/users/clients',
      'GET',
      undefined);
  }

  public async getCompanies() {
    return await this.httpService.requestWithContent<AdminUser[]>(
      'admin/users/companies',
      'GET',
      undefined);
  }
}
