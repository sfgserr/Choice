import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../enums/AppEnums.ts';
import {CompanyMapMarker} from '../types/DomainTypes.ts';

export class CompanyService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async getCompanies(categoryId: number, changeState: (state: State) => void): Promise<CompanyMapMarker[]> {
    let response = await this.httpService.request(`companies/${categoryId}`, 'GET', undefined, changeState);

    return response?.json();
  }
}
