import * as React from 'react';
import {
  Text,
  View,
} from 'react-native';

export default function Categories(): React.JSX.Element {
  return (
    <View>
      <Text
        style={{
          alignSelf: 'center',
          fontSize: 30,
          color: 'black'
        }}>
        Categories
      </Text>
    </View>
  )
}
