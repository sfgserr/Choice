import {Dimensions, Image, StyleSheet, View} from 'react-native';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import React from 'react';

const d = Dimensions.get('screen');

export default function ImageViewScreen({uri, navigation}: {uri: string, navigation: any}) {
  return (
    <View style={styles.background}>
      <Image
        style={styles.image}
        source={{uri: `${process.env.MINIO_URL}/app-files/${uri}`}}
      />
      <View style={styles.tab}>
        <View style={styles.navigateBackButtonContainer}>
          <NavigateBackButton navigation={navigation} />
        </View>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  background: {
    flex: 1,
    backgroundColor: 'black'
  },
  image: {
    width: d.width,
    height: d.height,
    resizeMode: 'contain'
  },
  tab: {
    backgroundColor: 'white',
    height: d.height*0.0725,
    width: d.width,
    flexDirection: 'row',
    paddingHorizontal: 10,
    justifyContent: 'flex-start',
    position: 'absolute',
    top: 0
  },
  navigateBackButtonContainer: {
    alignSelf: 'center'
  },
});
