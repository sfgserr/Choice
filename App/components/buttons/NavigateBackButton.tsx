import {Image, TouchableOpacity} from 'react-native';

export default function NavigateBackButton({navigation, onGoBack}: {navigation: any, onGoBack: (() => void) | undefined}) {
  return (
    <TouchableOpacity
      onPress={() => {
        navigation.goBack();
        if (onGoBack != undefined) {
          onGoBack();
        }
      }}>
      <Image
        style={{
          resizeMode: 'contain',
          alignSelf: 'center',
          width: 20,
          height: 20,
        }}
        source={require('../../assets/images/chevron-left.png')}/>
    </TouchableOpacity>
  );
}
