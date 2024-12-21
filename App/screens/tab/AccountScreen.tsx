import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {AuthContext} from '../../App.tsx';
import {StyledButton} from '../../components/StyledButton.tsx';
import {AccountScreenProps} from '../../types/NavigationTypes.ts';

export default function AccountScreen({route, navigation}: AccountScreenProps) {
  const { signOut } = React.useContext(AuthContext);

  return (
    <View>
      <Text
        style={{
          alignSelf: 'center',
          fontSize: 30,
          color: 'black'
        }}>
        <StyledButton
          pressed={async () => signOut()}
          content={'Выйти'}
          top={0}
          bottom={0}
          isDisabled={false}/>
      </Text>
    </View>
  )
}
