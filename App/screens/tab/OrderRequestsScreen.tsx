import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {AuthContext} from '../../App.tsx';
import {OrderRequestsScreenProps} from '../../types/NavigationTypes.ts';

export default function OrderRequestsScreen({route, navigation}: OrderRequestsScreenProps) {
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
