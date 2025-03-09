import AnimatedModal from './AnimatedModal.tsx';
import {StyleSheet, Text, View} from 'react-native';
import React from 'react';
import {StyledButton} from '../buttons/StyledButton.tsx';
import {UnsuccessfulRequestModalProps} from '../../types/ComponentTypes.ts';
import {Icon} from '@rneui/themed';

export default function UnsuccessfulRequestModal({isToggled, handlePress, errorMessage}: UnsuccessfulRequestModalProps) {
  return (
    <AnimatedModal
      isToggled={isToggled}
      handlePress={handlePress}
      withBackdrop={true}>
      <View style={styles.container}>
        <View style={styles.imageContainer}>
          <Icon
            type={'material'}
            name={'error'}
            size={50}
            color={'red'}/>
        </View>
        <View style={styles.titleContainer}>
          <Text style={styles.title}>
            {errorMessage}
          </Text>
        </View>
        <View style={styles.buttonContainer}>
          <StyledButton
            content={'Ok'}
            top={20}
            bottom={5}
            isDisabled={false}
            pressed={async () => handlePress()}/>
        </View>
      </View>
    </AnimatedModal>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  imageContainer: {
    paddingTop: 30
  },
  image: {
    alignSelf: 'center',
    width: 40,
    height: 40,
    resizeMode: 'contain'
  },
  titleContainer: {
    alignItems: 'baseline'
  },
  title: {
    fontWeight: '500',
    fontSize: 20,
    color: 'black',
    paddingTop: 10,
    alignSelf: 'center',
  },
  buttonContainer: {
    paddingHorizontal: 15
  }
})
