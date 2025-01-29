import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {ChatMessages} from '../../types/DomainTypes.ts';

export class ChatService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async getChat(userId: string) {
    return await this.httpService.requestWithContent<ChatMessages>(
      `api/messages/${userId}`,
      'GET',
      undefined);
  }
}
