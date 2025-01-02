import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../../enums/AppEnums.ts';
import {OrderRequest} from '../../types/DomainTypes.ts';

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
    distance: number,
    changeState: (state: State) => void) {
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
        distance
      }),
      changeState);
  }

  async getOrderRequests(changeState: (state: State) => void) {
    return await this.httpService.requestWithContent<OrderRequest[]>(
      'orderRequests',
      'GET',
      undefined,
      changeState
    );
  }
}
