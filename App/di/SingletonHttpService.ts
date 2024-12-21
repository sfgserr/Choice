import {HttpService} from '../services/HttpService.ts';

export class SingletonHttpService {
  private static instance: HttpService;

  public static initialize(httpService: HttpService) {
    this.instance = httpService;
  }

  public static getInstance() {
    return this.instance;
  }
}
