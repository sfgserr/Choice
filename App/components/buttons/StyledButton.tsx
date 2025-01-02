import {TouchableOpacity, Text, View} from 'react-native';
import Styles from '../../constants/Styles.tsx';
import React from 'react';
import {StyledButtonProps} from '../../types/ComponentTypes.ts';

export function StyledButton({content, top, bottom, isDisabled, pressed}: StyledButtonProps) {
  return (
    <View
      style={{
        top,
        bottom,
      }}>
      <TouchableOpacity
        style={[
          Styles.styledButton,
          {
            justifyContent: 'center',
            alignItems: 'baseline',
            backgroundColor: isDisabled ? '#abcdf3' : '#2D81E0',
          },
        ]}
        disabled={isDisabled}
        onPress={pressed}>
        <Text
          style={[
            Styles.styledButtonContent,
            {
              alignSelf: 'center',
            },
          ]}>
          {content}
        </Text>
      </TouchableOpacity>
    </View>
  );
}
