import {StateManager} from '../../managers/StateManager.ts';
import {HttpService} from './HttpService.ts';
import {State} from '../../enums/AppEnums.ts';
import {HttpResponse, HttpResponseWithContent} from '../../types/ServiceTypes.ts';

export class RefreshTokenHttpServiceDecorator {
  private readonly stateManager: StateManager;

  constructor(stateManager: StateManager) {
    this.stateManager = stateManager;
  }

  public async requestWithContent<T>(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined,
    setState: (state: State) => void): Promise<HttpResponseWithContent<T>> {
    const response = await this.internalRequestWithContent<T>(endPoint, method, body);

    if (response.result == 'unauthorized') {
      const state = await this.stateManager.getState();

      if (state != State.SignOut) {
        return await this.internalRequestWithContent<T>(endPoint, method, body);
      }

      setState(State.SignOut);
      return {result: 'unauthorized', content: null, error: ''};
    }

    return response;
  }

  private async internalRequest(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined): Promise<HttpResponse> {
    const response = await HttpService.getInstance().request(endPoint, method, body);

    if (response.status == 200) {
      return {
        result: 'successful',
        error: ''
      };
    }
    else if (response.status == 401) {
      return {
        result: 'unauthorized',
        error: ''
      }
    }
    else {
      return {
        result: 'bad_request',
        error: this.getError(await response.json())
      }
    }
  }

  private async internalRequestWithContent<T>(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined): Promise<HttpResponseWithContent<T>> {
    const response = await HttpService.getInstance().request(endPoint, method, body);

    if (response.status == 200) {
      return {
        result: 'successful',
        error: '',
        content: await response.json()
      };
    }
    else if (response.status == 401) {
      return {
        result: 'unauthorized',
        error: '',
        content: null
      }
    }
    else {
      return {
        result: 'bad_request',
        error: this.getError(await response.json()),
        content: null
      }
    }
  }

  public async request(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined,
    setState: (state: State) => void): Promise<HttpResponse> {
    const response = await this.internalRequest(endPoint, method, body);

    if (response.result == 'unauthorized') {
      const state = await this.stateManager.getState();

      if (state != State.SignOut) {
        return await this.internalRequest(endPoint, method, body);
      }

      setState(State.SignOut);
      return {result: 'unauthorized', error: ''};
    }

    return response;
  }

  private getError(content: any) {
    return content.errors != undefined ? content.errors[0] : content.detail != undefined ? content.detail : 'Неизвестная ошибка';
  }
}
