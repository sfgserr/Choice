import * as React from 'react';
import {State} from '../../../enums/AppEnums.ts';
import {AdminStack} from './AdminStack.ts';
import {Panel} from './Screens.tsx';

export default function AdminNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.Admin && (
        <AdminStack.Navigator>
          {Panel()}
        </AdminStack.Navigator>
      )}
    </>
  );
}
