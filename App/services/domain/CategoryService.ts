import {Category} from '../../types/DomainTypes.ts';
import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {HttpResponseWithContent} from '../../types/ServiceTypes.ts';

export class CategoryService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async getCategories(): Promise<HttpResponseWithContent<Category[]>> {
    return await this.httpService.requestWithContent<Category[]>('categories', 'GET', undefined);
  }

  async createCategory(title: string, iconUri: string) {
    return await this.httpService.request(
      'categories',
      'POST',
      JSON.stringify({
        title,
        iconUri,
      }));
  }
}
