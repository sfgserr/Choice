import {AuthService} from './auth/AuthService.ts';
import {AccountManager} from '../managers/AccountManager.ts';
import {TokenService} from './auth/TokenService.ts';
import {CategoryService} from './domain/CategoryService.ts';
import {TokenStorageService} from './object/TokenStorageService.ts';
import {StateManager} from '../managers/StateManager.ts';
import {RefreshTokenHttpServiceDecorator} from './http/RefreshTokenHttpServiceDecorator.ts';
import YaMap from 'react-native-yamap';
import {UserService} from './domain/UserService.ts';
import {CompanyService} from './domain/CompanyService.ts';
import {OrderRequestService} from './domain/OrderRequestService.ts';
import {ObjectStorageService} from './object/ObjectStorageService.ts';
import {ClientService} from './domain/ClientService.ts';
import {SubscriptionPaymentService} from './domain/SubscriptionPaymentService.ts';

type Object = {
  [name: string]: object,
};

export class ObjectGraph {
  private static isInitialized: boolean;
  private static readonly objects: Object = {};

  private constructor() {
  }

  static resolve<T>(type: string): T {
    if (!this.isInitialized)
      this.initialize();

    return this.objects[type] as T;
  }

  private static initialize() {
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
    const tokenService = new TokenService(authService);
    const tokenStorageService = new TokenStorageService();
    const accountManager = new AccountManager(tokenService);
    const stateManager = new StateManager(tokenStorageService, accountManager, tokenService);
    const httpService = new RefreshTokenHttpServiceDecorator(stateManager);
    const categoryService = new CategoryService(httpService);
    const userService = new UserService(httpService, tokenService);
    const objectStorageService = new ObjectStorageService(
      `${process.env.MINIO_URL}`,
      `${process.env.MINIO_ACCESS_KEY}`,
      `${process.env.MINIO_SECRET_KEY}`);
    const companyService = new CompanyService(httpService, objectStorageService);
    const orderRequestService = new OrderRequestService(httpService, objectStorageService);
    const clientService = new ClientService(httpService);
    const subscriptionPaymentService = new SubscriptionPaymentService(httpService);

    this.objects["AuthService"] = authService;
    this.objects["AccountManager"] = accountManager;
    this.objects["TokenService"] = tokenService;
    this.objects["CategoryService"] = categoryService;
    this.objects["TokenStorageService"] = tokenStorageService;
    this.objects["StateManager"] = stateManager;
    this.objects["HttpService"] = httpService;
    this.objects["UserService"] = userService;
    this.objects["CompanyService"] = companyService;
    this.objects["OrderRequestService"] = orderRequestService;
    this.objects["ClientService"] = clientService;
    this.objects["SubscriptionPaymentService"] = subscriptionPaymentService;

    this.isInitialized = true;
  }
}
