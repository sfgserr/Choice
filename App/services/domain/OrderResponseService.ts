import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';

export class OrderResponseService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async createOrderResponse(
    requestId: string,
    price: number,
    deadline: number,
    enrollmentDate: Date,
    prepayment: number) {
    return await this.httpService.request(
      'orderResponses',
      'POST',
      JSON.stringify({
        requestId,
        price,
        deadline,
        enrollmentDate,
        prepayment
      }));
  }
}
