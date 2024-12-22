import {AuthService} from '../services/AuthService.ts';
import {AccountManager} from '../AccountManager.ts';
import {TokenStore} from '../stores/TokenStore.ts';
import {TokenService} from '../services/TokenService.ts';
import {CategoriesService} from '../services/CategoriesService.ts';
import {TokenStorageService} from '../services/TokenStorageService.ts';
import {StateManager} from '../StateManager.ts';
import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';
import YaMap from 'react-native-yamap';
import {UserStore} from '../stores/UserStore.ts';
import {UserService} from '../services/UserService.ts';


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
    const categoriesService = new CategoriesService(httpService);
    const userStore = new UserStore();
    const userService = new UserService(httpService, userStore, tokenService);

    this.objects["AuthService"] = authService;
    this.objects["AccountManager"] = accountManager;
    this.objects["TokenStore"] = tokenStore;
    this.objects["TokenService"] = tokenService;
    this.objects["CategoriesService"] = categoriesService;
    this.objects["TokenStorageService"] = tokenStorageService;
    this.objects["StateManager"] = stateManager;
    this.objects["HttpService"] = httpService;
    this.objects["UserService"] = userService;

    this.isInitialized = true;
  }
}
