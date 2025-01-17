import {Dimensions, Image, View} from 'react-native';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {ImageViewScreenProps} from '../types/NavigationTypes.ts';

const d = Dimensions.get('screen');

export default function ImageViewScreen({route, navigation}: ImageViewScreenProps) {
  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'black'
      }}>
      <Image
        style={{
          width: d.width,
          height: d.height,
          resizeMode: 'contain'
        }}
        source={{uri: `${process.env.MINIO_URL}/app-files/${route.params.uri}`}}/>
      <View
        style={{
          backgroundColor: 'white',
          height: d.height*0.0725,
          width: d.width,
          flexDirection: 'row',
          paddingHorizontal: 10,
          justifyContent: 'flex-start',
          position: 'absolute',
          top: 0
        }}>
        <View style={{alignSelf: 'center'}}>
          <NavigateBackButton
            navigation={navigation}/>
        </View>
      </View>
    </View>
  )
}
