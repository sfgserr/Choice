import {Category} from '../types/DomainTypes.ts';
import {HttpService} from './HttpService.ts';

export class CategoriesService {
  async getCategories(signOut: () => void): Promise<Category[]> {
    const httpService = HttpService.getInstance();

    let response = await httpService.request('categories', 'GET', undefined, signOut);

    return response.json();
  }
}
