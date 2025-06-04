import {Image} from 'react-native';
import {Pressable} from 'react-native-gesture-handler';
import {useCallback} from 'react';

export default function NavigateBackButton({navigation, onGoBack, override = undefined}: {
  navigation: any,
  onGoBack: (() => void) | undefined
  override: (() => void) | undefined}) {
  const navigateBack = useCallback(() => {
    navigation.goBack();
    if (onGoBack != undefined) {
      onGoBack();
    }
  }, []);

  return (
    <Pressable
      onPress={override || navigateBack}>
      <Image
        style={{
          resizeMode: 'contain',
          alignSelf: 'center',
          width: 20,
          height: 20,
        }}
        source={require('../../assets/images/chevron-left.png')}/>
    </Pressable>
  );
}
