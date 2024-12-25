import {Category} from '../types/DomainTypes.ts';
import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../enums/AppEnums.ts';

export class CategoryService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async getCategories(setState: (state: State) => void): Promise<Category[] | null> {
    let response = await this.httpService.request('categories', 'GET', undefined, setState);

    return response?.json();
  }
}
