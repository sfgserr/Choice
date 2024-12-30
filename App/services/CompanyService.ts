import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../enums/AppEnums.ts';
import {CompanyMapMarker} from '../types/DomainTypes.ts';
import {HttpResponse} from '../types/ServiceTypes.ts';

export class CompanyService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async getCompanies(categoryId: number, changeState: (state: State) => void): Promise<HttpResponse<CompanyMapMarker[]>> {
    return await this.httpService.request<CompanyMapMarker[]>(
      `companies/${categoryId}`,
      'GET',
      undefined,
      changeState);
  }
}
