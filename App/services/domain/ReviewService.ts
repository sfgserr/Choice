import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {Review} from '../../types/DomainTypes.ts';

export class ReviewService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async getReviews(userId: string) {
    return await this.httpService.requestWithContent<Review[]>(
      `orderResponses/reviews/${userId}`,
      'GET',
      undefined);
  }
}
