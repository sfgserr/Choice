import {TouchableOpacity, Text, View} from 'react-native';
import Styles from '../../constants/Styles.tsx';
import React from 'react';
import {StyledButtonProps} from '../../types/ComponentTypes.ts';

export function StyledButton({content, top, bottom, isDisabled, pressed, reversed = false}: StyledButtonProps) {
  return (
    <View
      style={{
        paddingTop: top,
        paddingBottom: bottom,
      }}>
      <TouchableOpacity
        style={[
          Styles.styledButton,
          {
            justifyContent: 'center',
            alignItems: 'baseline',
            backgroundColor: reversed ? '#001C3D0D' : '#2D81E0',
            opacity: isDisabled ? 0.4 : 1,
          },
        ]}
        disabled={isDisabled}
        onPress={pressed}>
        <Text
          style={[
            Styles.styledButtonContent,
            {
              alignSelf: 'center',
              color: reversed ? '#2D81E0' : 'white',
            },
          ]}>
          {content}
        </Text>
      </TouchableOpacity>
    </View>
  );
}
