import {HttpService} from './HttpService.ts';
import {Category} from '../types/DomainTypes.ts';

export class CategoriesService {
  private readonly httpService: HttpService;

  constructor(httpService: HttpService) {
    this.httpService = httpService;
  }

  async getCategories(): Promise<Category[]> {
    let response = await this.httpService.request('api/categories', 'GET', undefined);

    return response.json();
  }
}
