import {RefreshTokenHttpServiceDecorator} from '../http/RefreshTokenHttpServiceDecorator.ts';

export class IdentityService {
  private readonly httpService: RefreshTokenHttpServiceDecorator;

  constructor(httpService: RefreshTokenHttpServiceDecorator) {
    this.httpService = httpService;
  }

  public async changePassword(oldPassword: string, newPassword: string) {
    return await this.httpService.request(
      'identity',
      'PUT',
      JSON.stringify({
        oldPassword,
        newPassword,
      }));
  }
}
