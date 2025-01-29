import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {ChatsScreenProps} from '../../types/NavigationTypes.ts';

export default function ChatsScreen({route, navigation}: ChatsScreenProps) {
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
