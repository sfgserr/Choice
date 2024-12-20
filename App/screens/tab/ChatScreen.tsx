import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {ClientTabProps} from '../../types/NavigationTypes.ts';

type Props = BottomTabScreenProps<ClientTabProps, 'Chat'>

export default function ChatScreen({route, navigation}: Props) {
  return (
    <View>
      <Text
        style={{
          alignSelf: 'center',
          fontSize: 30,
          color: 'black'
        }}>
        Chat
      </Text>
    </View>
  )
}
