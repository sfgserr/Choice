import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../../enums/AppEnums.ts';

export class SubscriptionPaymentService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async buy(plan: string, changeState: (state: State) => void) {
    return this.httpService.request(
      `subscriptionPayments/${plan}`,
      'POST',
      undefined,
      changeState);
  }
}
