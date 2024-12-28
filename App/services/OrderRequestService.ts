import {RefreshTokenHttpServiceDecorator} from '../decorators/RefreshTokenHttpServiceDecorator.ts';

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
    photos: string[],
    radius: number,
    changeState: () => void) {
    var response = await this.httpService.request(
      'orderRequests',
      'POST',
      JSON.stringify({
        categoryId,
        description,
        toKnowPrice,
        toKnowDeadline,
        toKnowEnrollmentDate,
        photos,
        radius
      }),
      changeState);

    return response?.json();
  }
}
