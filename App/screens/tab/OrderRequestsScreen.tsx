import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {AuthContext, ClientTabProps} from '../../App.tsx';
import {StyledButton} from '../../components/StyledButton.tsx';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';

type Props = BottomTabScreenProps<ClientTabProps, 'OrderRequests'>

export default function OrderRequestsScreen({route, navigation}: Props) {
  const { signOut } = React.useContext(AuthContext);

  return (
    <View>
      <Text
        style={{
          alignSelf: 'center',
          fontSize: 30,
          color: 'black'
        }}>
        OrderRequests
      </Text>
    </View>
  )
}
