import * as React from 'react';
import {State} from './enums/AppEnums.ts';
import {StateManager} from './managers/StateManager.ts';
import {AuthContextProvider} from './contexts/authorized/Provider.tsx';
import {useDependency} from './services/Hooks.ts';
import StateContainer from './navigation/StateContainer.tsx';

export default function Root(): React.JSX.Element {
  const stateManager: StateManager = useDependency<StateManager>('StateManager');

  const [state, setState] = React.useState(State.Restoring);

  React.useEffect(() => {
    const getState = async () => {
      const state = await stateManager.getState();
      setState(state);
    }

    getState();
  }, []);

  return (
    <AuthContextProvider setState={setState}>
      <StateContainer state={state}/>
    </AuthContextProvider>
  );
}
