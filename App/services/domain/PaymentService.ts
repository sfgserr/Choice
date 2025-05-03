import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';

export class PaymentService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async getWallet() {
    return await this.httpService.requestWithContent<number>('wallets', 'GET', undefined);
  }

  public async createPayment(copecks: number) {
    return await this.httpService.requestWithContent<any>('wallets/payment', 'POST', JSON.stringify({copecks}));
  }

  public async createPayout(bankCardNumber: string, copecks: number) {
    return await this.httpService.request(
      'wallets/payout',
      'POST',
      JSON.stringify({copecks}));
  }
}
