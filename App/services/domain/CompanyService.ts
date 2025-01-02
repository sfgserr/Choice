import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../../enums/AppEnums.ts';
import {CompanyMapMarker} from '../../types/DomainTypes.ts';
import {HttpResponseWithContent} from '../../types/ServiceTypes.ts';

export class CompanyService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async getCompanies(categoryId: number, changeState: (state: State) => void): Promise<HttpResponseWithContent<CompanyMapMarker[]>> {
    return await this.httpService.requestWithContent<CompanyMapMarker[]>(
      `companies/${categoryId}`,
      'GET',
      undefined,
      changeState);
  }
}
