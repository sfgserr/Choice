import {StateManager} from '../StateManager.ts';
import {HttpService} from '../services/HttpService.ts';
import {State} from '../enums/AppEnums.ts';
import {HttpResponse} from '../types/ServiceTypes.ts';

export class RefreshTokenHttpServiceDecorator {
  private readonly stateManager: StateManager;

  constructor(stateManager: StateManager) {
    this.stateManager = stateManager;
  }

  async request<T>(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined,
    setState: (state: State) => void): Promise<HttpResponse<T>> {
    const response = await HttpService.getInstance().request(endPoint, method, body);

    if (response.status == 401) {
      const state = await this.stateManager.getState();

      if (state != State.SignOut) {
        let response = await HttpService.getInstance().request(endPoint, method, body);

        return {
          result: response.status == 200 ? 'successful' : 'bad_request',
          content: response.status == 200 ? await response.json() : null
        };
      }

      setState(State.SignOut);
      return {result: 'unauthorized', content: null};
    }

    return {
      result: response.status == 200 ? 'successful' : 'bad_request',
      content: response.status == 200 ? await response.json() : null
    };
  }
}
