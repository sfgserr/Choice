import AnimatedModal from './AnimatedModal.tsx';
import {StyleSheet, View} from 'react-native';
import CloseButton from './CloseButton.tsx';

export default function SuccessfulRequestModal({isToggled, handlePress}) {
  return (
    <AnimatedModal
      isToggled={isToggled}
      handlePress={handlePress}>
      <View style={{flex: 1}}>
        <View style={styles.closeButtonContainer}>
          <CloseButton
            close={() => handlePress()}/>
        </View>
      </View>
    </AnimatedModal>
  )
}

const styles = StyleSheet.create({
  closeButtonContainer: {
    position: 'absolute',
    top: 10,
    right: 10
  }
})
