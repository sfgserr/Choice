import {State} from '../../../enums/AppEnums.ts';
import {BannedStack} from './BannedStack.ts';
import {Banned} from './Screens.tsx';

export default function BannedNavigator({state}: {state: State}) {
  return <>
      {state == State.Banned && (
          <BannedStack.Navigator>
              {Banned()}
          </BannedStack.Navigator>
      )}
  </>;
}
