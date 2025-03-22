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

  public async getClient(clientId: string) {
    return await this.httpService.requestWithContent<any>(`admin/users/client/${clientId}`, 'GET', undefined);
  }

  public async getCompany(companyId: string) {
    return await this.httpService.requestWithContent<any>(`admin/users/company/${companyId}`, 'GET', undefined);
  }

  public async editClient(
    clientId: string,
    iconUri: string,
    name: string,
    phoneNumber: string,
    email: string,
    city: string,
    street: string) {
    return await this.httpService.request(
      'admin/users/client',
      'PUT',
      JSON.stringify({
        clientId,
        iconUri,
        name,
        phoneNumber,
        email,
        city,
        street,
      }));
  }

  public async editCompany(
    id: string,
    iconUri: string,
    name: string,
    description: string,
    email: string,
    phoneNumber: string,
    city: string,
    street: string,
    socialMedias: string[],
    categoryIds: number[],
    photoUris: string[],
    isPrepaymentAvailable: boolean) {
    return await this.httpService.request(
      'admin/users/company',
      'PUT',
      JSON.stringify({
        id,
        iconUri,
        name,
        phoneNumber,
        email,
        description,
        city,
        street,
        socialMedias,
        categoryIds,
        photoUris,
        isPrepaymentAvailable,
      }));
  }

  public async getCompanies() {
    return await this.httpService.requestWithContent<AdminUser[]>(
      'admin/users/companies',
      'GET',
      undefined);
  }
}
