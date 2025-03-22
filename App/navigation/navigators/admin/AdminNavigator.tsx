import * as React from 'react';
import {State} from '../../../enums/AppEnums.ts';
import {AdminStack} from './AdminStack.ts';
import {CreateCategory, EditCategory, EditClient, EditCompany, Panel} from './Screens.tsx';

export default function AdminNavigator({state}: {state: State}) {
  return (
    <>
      {state == State.Admin && (
        <AdminStack.Navigator>
          {Panel()}
          {EditCategory()}
          {CreateCategory()}
          {EditClient()}
          {EditCompany()}
        </AdminStack.Navigator>
      )}
    </>
  );
}
