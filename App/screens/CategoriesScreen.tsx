import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {AuthContext, StackProps} from '../App.tsx';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {StyledButton} from '../components/StyledButton.tsx';

type Props = NativeStackScreenProps<StackProps, 'Categories'>

export default function CategoriesScreen() {
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
