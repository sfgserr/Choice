import {StyleSheet, Text, View} from 'react-native';
import BottomSheet, {BottomSheetBackdrop} from '@gorhom/bottom-sheet';
import React, {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import CloseButton from '../buttons/CloseButton.tsx';

const CustomBottomSheet = React.forwardRef(({title, children, close}: any, ref: ForwardedRef<BottomSheetMethods>) => {
  const renderBackdrop = React.useCallback(
    (props) => (
      <BottomSheetBackdrop
        {...props}
        appearsOnIndex={0}
        disappearsOnIndex={-1}/>
    ),
    []
  );

  const snapPoints = React.useMemo(() => ['25%', '50%', '90%'], []);

  return (
    <BottomSheet
      ref={ref}
      backdropComponent={renderBackdrop}
      snapPoints={snapPoints}
      index={-1}>
      <View style={styles.container}>
        <View style={styles.titleContainer}>
          <CloseButton
            close={() => close()}/>
        </View>
        <Text
          style={styles.title}>
          {title}
        </Text>
        <View style={styles.childrenContainer}>
          {children}
        </View>
      </View>
    </BottomSheet>
  )
})

const styles = StyleSheet.create({
  container: {
    width: '100%',
    display: 'flex',
    paddingHorizontal: 15
  },
  titleContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-end',
  },
  title: {
    fontWeight: '600',
    fontSize: 21,
    position: 'absolute',
    alignSelf: 'center'
  },
  childrenContainer: {
    paddingTop: 20
  }
});

export default CustomBottomSheet;
