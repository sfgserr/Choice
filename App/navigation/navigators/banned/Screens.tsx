import {BannedStack} from './BannedStack.ts';
import BannedScreen from '../../../screens/BannedScreen.tsx';

export const Banned = () => (
  <BannedStack.Screen
    name={'Banned'}
    component={BannedScreen}
    options={{headerShown: false}}/>
);
