import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../../enums/AppEnums.ts';
import {SubscriptionPayment} from '../../types/DomainTypes.ts';

export class SubscriptionPaymentService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async buy(plan: string, changeState: (state: State) => void) {
    return this.httpService.request(
      `subscriptionPayment/${plan}`,
      'POST',
      undefined,
      changeState);
  }

  async get(changeState: (state: State) => void) {
    return this.httpService.requestWithContent<SubscriptionPayment>(
      `subscriptionPayment`,
      'GET',
      undefined,
      changeState);
  }

  async pay(changeState: (state: State) => void) {
    return this.httpService.request(
      'subscriptionPayment',
      'PUT',
      undefined,
      changeState);
  }
}
