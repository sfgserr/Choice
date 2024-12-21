import {AuthService} from '../services/AuthService.ts';
import {AccountManager} from '../AccountManager.ts';
import {UserStore} from '../stores/UserStore.ts';
import {UserService} from '../services/UserService.ts';
import {HttpService} from '../services/HttpService.ts';
import {CategoriesService} from '../services/CategoriesService.ts';
import {TokenStorageService} from '../services/TokenStorageService.ts';
import {SingletonHttpService} from './SingletonHttpService.ts';

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
      process.env.CLIENT_SECRET == undefined)
      throw new Error();

    const authService = new AuthService(
      `${process.env.API_URL}/api/auth/token`,
      process.env.CLIENT_ID,
      process.env.CLIENT_SECRET);
    const userStore = new UserStore();
    const userService = new UserService(userStore, authService);
    const categoriesService = new CategoriesService();
    const tokenStorageService = new TokenStorageService();
    const accountManager = new AccountManager(userService, tokenStorageService);

    this.objects["AuthService"] = authService;
    this.objects["AccountManager"] = accountManager;
    this.objects["UserStore"] = userStore;
    this.objects["UserService"] = userService;
    this.objects["CategoriesService"] = categoriesService;
    this.objects["TokenStorageService"] = tokenStorageService;

    this.isInitialized = true;
  }
}
