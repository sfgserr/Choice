import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator';

export type ReviewText = {
  id: number,
  grade: number,
  text: string,
  created: boolean
}

export class ReviewTextService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async addReviewText(grade: number, text: string) {
    return await this.httpService.request(
      'reviewTexts',
      'POST',
      JSON.stringify({
        grade,
        text,
      }));
  }

  public async getReviewTexts() {
    return await this.httpService.requestWithContent<ReviewText[]>(
      'reviewTexts',
      'GET',
      undefined);
  }

  public async deleteReviewText(id: number) {
    return await this.httpService.request(
      `reviewTexts/${id}`,
      'DELETE',
      undefined);
  }
}
