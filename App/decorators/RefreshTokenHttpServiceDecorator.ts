import {StateManager} from '../StateManager.ts';
import {HttpService} from '../services/HttpService.ts';
import {State} from '../enums/AppEnums.ts';

export class RefreshTokenHttpServiceDecorator {
  private readonly stateManager: StateManager;

  constructor(stateManager: StateManager) {
    this.stateManager = stateManager;
  }

  async request(
    endPoint: string,
    method: string,
    body: BodyInit_ | undefined,
    setState: (state: State) => void) {
    const response = await HttpService.getInstance().request(endPoint, method, body);

    if (response.status == 401) {
      const state = await this.stateManager.getState();

      if (state != State.SignOut)
        return await HttpService.getInstance().request(endPoint, method, body);

      setState(State.SignOut);
      return null;
    }

    return response;
  }
}
