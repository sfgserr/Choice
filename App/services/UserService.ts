import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';
import {UserStore} from '../stores/UserStore.ts';
import {State} from '../enums/AppEnums.ts';
import {TokenService} from './TokenService.ts';
import {UserType} from '../enums/ModelEnums.ts';

export class UserService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;
  private readonly userStore: UserStore;
  private readonly tokenService: TokenService;

  constructor(
    httpService: RefreshTokenHttpServiceDecorator,
    userStore: UserStore,
    tokenStore: TokenService,
  ) {
    this.httpService = httpService;
    this.userStore = userStore;
    this.tokenService = tokenStore;
  }

  async fetchUser(changeState: (state: State) => void) {
    const response = await this.httpService.requestWithContent(
      this.tokenService.getUser().userType == UserType.Client ? 'clients' : 'companies',
      'GET',
      undefined,
      changeState,
    );

    if (response != null) this.userStore.setUser(response.json());
  }
}
