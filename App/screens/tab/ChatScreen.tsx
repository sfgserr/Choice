import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';
import {ChatScreenProps} from '../../types/NavigationTypes.ts';

export default function ChatScreen({route, navigation}: ChatScreenProps) {
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
