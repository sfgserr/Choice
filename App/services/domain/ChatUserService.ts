import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';

export class ChatUserService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async addOrUpdateDevice(device: string, token: string) {
    return await this.httpService.request('chatUsers', 'PUT', JSON.stringify({
      device,
      token,
    }));
  }

  public async removeDevice(device: string) {
    return await this.httpService.request(`chatUsers/${device}`, 'DELETE', undefined);
  }
}
