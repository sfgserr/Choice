import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {Chat, ChatMessages, Message} from '../../types/DomainTypes.ts';

export class ChatService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async getChat(userId: string) {
    return await this.httpService.requestWithContent<ChatMessages>(
      `messages/${userId}`,
      'GET',
      undefined);
  }

  public async getChats() {
    return await this.httpService.requestWithContent<Chat[]>(
        'messages',
        'GET',
        undefined);
  }

  public async create(toUserId: string, content: string, type: string) {
    return await this.httpService.requestWithContent<Message>(
      'messages',
      'POST',
      JSON.stringify({
        toUserId,
        content,
        type,
      }));
  }

  public async getStatus(userId: string) {
    return await this.httpService.requestWithContent<boolean>(
      `chat/status/${userId}`,
      'GET',
      undefined);
  }
}
