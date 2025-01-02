import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {State} from '../../enums/AppEnums.ts';
import {OrderRequest} from '../../types/DomainTypes.ts';
import {ObjectStorageService} from '../object/ObjectStorageService.ts';

export class OrderRequestService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;
  private readonly objectStorageService: ObjectStorageService;

  constructor(httpService: RefreshTokenHttpServiceDecorator, objectStorageService: ObjectStorageService) {
    this.httpService = httpService;
    this.objectStorageService = objectStorageService;
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
    const response = await this.httpService.requestWithContent<OrderRequest>(
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

    if (response.result == 'successful') {
      await this.objectStorageService.upload(photoUris[0]);
    }

    return response;
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
