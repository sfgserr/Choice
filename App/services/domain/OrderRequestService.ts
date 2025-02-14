import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {CompanyOrderRequest, OrderRequest, OrderRequestDetails, OrderRequestRadius} from '../../types/DomainTypes.ts';

export class OrderRequestService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  async create(
    categoryId: number,
    description: string,
    toKnowPrice: boolean,
    toKnowDeadline: boolean,
    toKnowEnrollmentDate: boolean,
    photoUris: string[],
    distance: number) {
    return await this.httpService.requestWithContent<OrderRequest>(
      'orderRequests',
      'POST',
      JSON.stringify({
        categoryId,
        description,
        toKnowPrice,
        toKnowDeadline,
        toKnowEnrollmentDate,
        photoUris,
        distance,
      }));
  }

  async edit(
    requestId: string,
    categoryId: number,
    description: string,
    toKnowPrice: boolean,
    toKnowDeadline: boolean,
    toKnowEnrollmentDate: boolean,
    photoUris: string[],
    distance: number) {
    return await this.httpService.request(
      'orderRequests',
      'PUT',
      JSON.stringify({
        requestId,
        categoryId,
        description,
        toKnowPrice,
        toKnowDeadline,
        toKnowEnrollmentDate,
        photoUris,
        distance,
      }));
  }

  async getOrderRequests() {
    return await this.httpService.requestWithContent<OrderRequest[]>(
      'orderRequests',
      'GET',
      undefined
    );
  }

  async getOrderRequest(id: string) {
    return await this.httpService.requestWithContent<OrderRequestDetails>(
      `orderRequests/${id}`,
      'GET',
      undefined);
  }

  async getOrderRequestsRadius() {
    return await this.httpService.requestWithContent<OrderRequestRadius[]>(
      'orderRequests/radius',
      'GET',
      undefined);
  }

  async getOrderRequestAsCompany(id: string) {
    return await this.httpService.requestWithContent<CompanyOrderRequest>(
      `orderRequests/company/${id}`,
      'GET',
      undefined);
  }
}
