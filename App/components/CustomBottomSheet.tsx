import {Image, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import BottomSheet, {BottomSheetBackdrop} from '@gorhom/bottom-sheet';
import React, {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';

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

  const snapPoints = React.useMemo(() => ['25%', '50%', '70%'], []);

  return (
    <BottomSheet
      ref={ref}
      backdropComponent={renderBackdrop}
      snapPoints={snapPoints}>
      <View style={styles.container}>
        <View style={styles.titleContainer}>
          <TouchableOpacity
            style={styles.closeButton}
            onPress={() => close()}>
            <Image
              style={styles.image}
              source={require('../assets/images/cross.png')}
            />
          </TouchableOpacity>
        </View>
        <Text
          style={styles.title}>
          {title}
        </Text>
        {children}
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
  closeButton: {
    alignSelf: 'center',
    borderRadius: 360,
    backgroundColor: '#f0f1f3',
    overflow: 'hidden',
    width: 25,
    height: 25,
    justifyContent: 'center',
  },
  image: {
    width: '50%',
    height: '50%',
    resizeMode: 'contain',
    alignSelf: 'center',
  },
  title: {
    fontWeight: '600',
    fontSize: 21,
    position: 'absolute',
    alignSelf: 'center'
  },
});

export default CustomBottomSheet;
