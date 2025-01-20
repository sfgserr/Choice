import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {SubscriptionPayment} from '../../types/DomainTypes.ts';

export class SubscriptionPaymentService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async buy(plan: string) {
    return this.httpService.request(
      `subscriptionPayment/${plan}`,
      'POST',
      undefined);
  }

  async get() {
    return this.httpService.requestWithContent<SubscriptionPayment>(
      `subscriptionPayment`,
      'GET',
      undefined);
  }

  async pay() {
    return this.httpService.request(
      'subscriptionPayment',
      'PUT',
      undefined);
  }
}
