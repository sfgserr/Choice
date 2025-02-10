import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';
import {Chat, ChatMessages, Message} from '../../types/DomainTypes.ts';
import {ObjectStorageService} from '../object/ObjectStorageService.ts';
import {FilePathUtils} from "../../utils/FilePathUtils.ts";

export class ChatService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;
  private readonly objectStorageService: ObjectStorageService;

  constructor(httpService: RefreshTokenHttpServiceDecorator, objectStorageService: ObjectStorageService) {
    this.httpService = httpService;
    this.objectStorageService = objectStorageService;
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

  public async createImage(toUserId: string, content: string) {
    const path = FilePathUtils.getFileName(content);

    if (path != undefined) {
      const response = await this.create(toUserId, path, 'Image');

      if (response.result == 'successful') {
        await this.objectStorageService.upload(content, path);
      }

      return response;
    }
  }

  public async getStatus(userId: string) {
    return await this.httpService.requestWithContent<boolean>(
      `chat/status/${userId}`,
      'GET',
      undefined);
  }

  public async read(userId: string, messageId: string) {
    return await this.httpService.request(
      `chat/${userId}/${messageId}`,
      'PUT',
      undefined);
  }
}
