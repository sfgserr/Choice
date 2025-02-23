import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {OrderResponse} from "../../types/DomainTypes.ts";

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
        prepayment,
      }));
  }

  public async get(id: string) {
    return await this.httpService.requestWithContent<OrderResponse>(
      `orderResponses/${id}`,
      'GET',
      undefined);
  }

  public async changeEnrollmentDate(responseId: string, enrollmentDate: Date) {
    return await this.httpService.request(
      `orderResponses/${responseId}/${enrollmentDate.toJSON()}`,
      'PUT',
      undefined);
  }

  public async confirm(responseId: string) {
    return await this.httpService.request(
      `orderResponses/confirm/${responseId}`,
      'PUT',
      undefined);
  }
}
