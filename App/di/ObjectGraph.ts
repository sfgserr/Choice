import {AuthService} from '../services/auth/AuthService.ts';
import {AccountManager} from '../managers/AccountManager.ts';
import {TokenStore} from '../stores/TokenStore.ts';
import {TokenService} from '../services/auth/TokenService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {TokenStorageService} from '../services/object/TokenStorageService.ts';
import {StateManager} from '../managers/StateManager.ts';
import {RefreshTokenHttpServiceDecorator} from '../services/http/RefreshTokenHttpServiceDecorator.ts';
import YaMap from 'react-native-yamap';
import {UserStore} from '../stores/UserStore.ts';
import {UserService} from '../services/domain/UserService.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';

type Object = {
  [name: string]: object,
};

export class ObjectGraph {
  private isInitialized: boolean;
  private readonly objects: Object = {};

  constructor() {
    this.isInitialized = false;
    this.objects = {};
  }

  resolve<T>(type: string): T {
    return this.objects[type] as T;
  }

  initialize() {
    if (this.isInitialized) return;

    if (
      process.env.API_URL == undefined ||
      process.env.CLIENT_ID == undefined ||
      process.env.CLIENT_SECRET == undefined ||
      process.env.YANDEX_API_KEY == undefined)
      throw new Error();

    YaMap.init(`${process.env.YANDEX_API_KEY}`);

    const authService = new AuthService(
      `${process.env.API_URL}/api/auth/token`,
      process.env.CLIENT_ID,
      process.env.CLIENT_SECRET);
    const tokenStore = new TokenStore();
    const tokenService = new TokenService(tokenStore, authService);
    const tokenStorageService = new TokenStorageService();
    const accountManager = new AccountManager(tokenService);
    const stateManager = new StateManager(tokenStorageService, accountManager, tokenService);
    const httpService = new RefreshTokenHttpServiceDecorator(stateManager);
    const categoryService = new CategoryService(httpService);
    const userStore = new UserStore();
    const userService = new UserService(httpService, userStore, tokenService);
    const companyService = new CompanyService(httpService);
    const orderRequestService = new OrderRequestService(httpService);

    this.objects["AuthService"] = authService;
    this.objects["AccountManager"] = accountManager;
    this.objects["TokenStore"] = tokenStore;
    this.objects["TokenService"] = tokenService;
    this.objects["CategoryService"] = categoryService;
    this.objects["TokenStorageService"] = tokenStorageService;
    this.objects["StateManager"] = stateManager;
    this.objects["HttpService"] = httpService;
    this.objects["UserService"] = userService;
    this.objects["CompanyService"] = companyService;
    this.objects["OrderRequestService"] = orderRequestService;

    this.isInitialized = true;
  }
}
