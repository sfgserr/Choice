import * as React from 'react';
import {State} from './enums/AppEnums';
import {StateManager} from './managers/StateManager';
import {AuthContextProvider} from './contexts/authorized/Provider';
import {useDependency} from './services/Hooks';
import StateContainer from './navigation/StateContainer';
import {ObjectGraph} from './services/ObjectGraph';

export default function Root(): React.JSX.Element {
  const [state, setState] = React.useState(State.Restoring);

  ObjectGraph.initialize(setState);

  const stateManager: StateManager = useDependency<StateManager>('StateManager');

  React.useEffect(() => {
    const getState = async () => {
      const state = await stateManager.getState();
      setState(state);
    };

    getState();
  }, []);

  return (
    <AuthContextProvider setState={setState}>
      <StateContainer state={state}/>
    </AuthContextProvider>
  );
}
