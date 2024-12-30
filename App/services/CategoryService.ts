import {Category} from '../types/DomainTypes.ts';
import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../enums/AppEnums.ts';
import {HttpResponse} from '../types/ServiceTypes.ts';

export class CategoryService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async getCategories(setState: (state: State) => void): Promise<HttpResponse<Category[]>> {
    return await this.httpService.request<Category[]>('categories', 'GET', undefined, setState);
  }
}
