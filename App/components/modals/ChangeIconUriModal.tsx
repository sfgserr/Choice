import React from 'react';
import {
  Dimensions,
  StyleSheet,
  Text,
  View,
} from 'react-native';
import Animated, {useAnimatedStyle, withDelay, withSpring, withTiming} from 'react-native-reanimated';
import {TouchableOpacity} from 'react-native-gesture-handler';
import {ChangeIconUriModalProps} from '../../types/ComponentTypes.ts';
import {launchImageLibrary, launchCamera, ImagePickerResponse} from 'react-native-image-picker';
import {useDependency} from '../../services/Hooks.ts';
import {FileValidationService} from '../../services/object/FileValidationService.ts';
import {ObjectStorageService} from '../../services/object/ObjectStorageService.ts';
import {UserService} from '../../services/domain/UserService.ts';

const d = Dimensions.get('screen');

export default function ChangeIconUriModal({ isToggled, handlePress, setIcon }: ChangeIconUriModalProps) {
  const userService = useDependency<UserService>('UserService');
  const fileValidationService = useDependency<FileValidationService>('FileValidationService');
  const blobService = useDependency<ObjectStorageService>('ObjectStorageService');

  const duration = 1800;

  const animatedStyles = useAnimatedStyle(() => ({
    transform: [{ translateY: withSpring(isToggled ? (-50) : (d.height * 0.4)) }],
  }));

  const animatedStyle = useAnimatedStyle(() => {
    return {
      opacity: withSpring(isToggled ? 1 : 0, {duration}),
      zIndex: isToggled
        ? 1
        : withDelay(duration, withTiming(-1, { duration: 0 })),
    };
  });

  const setFromGallery = React.useCallback(async ()=> {
    const response = await launchImageLibrary({mediaType: 'photo'});
    await set(response);
  }, []);

  const setFromCamera = React.useCallback(async ()=> {
    const response = await launchCamera({mediaType: 'photo'});
    await set(response);
  }, []);

  const set = React.useCallback(async (response: ImagePickerResponse) => {
    if (response.assets != undefined) {
      const result = await fileValidationService.getContentAndValidate(response.assets[0].uri!);

      if (result.object != null) {
        const response = await userService.changeIconUri(result.object.objectName);

        if (response.result == 'successful') {
          const isSet = await blobService.upload(result.object);

          if (isSet) setIcon(result.object.objectName);
        }
      }
    }
  }, []);

  return (
    <>
      <Animated.View style={[styles.backdrop, animatedStyle]}>
        <TouchableOpacity
          style={{flex: 1}}
          onPress={() => handlePress()}
          disabled={!isToggled}
        />
      </Animated.View>
      <Animated.View style={[styles.popup, animatedStyles]}>
        <TouchableOpacity
          style={{
            height: d.height * 0.064,
            borderTopRightRadius: 15,
            borderTopLeftRadius: 15,
            backgroundColor: 'white',
            justifyContent: 'center',
            borderBottomColor: '#e0e0e0',
            borderBottomWidth: 1,
          }}
          onPress={async () => {
            await setFromCamera();
            handlePress();
          }}>
          <Text
            style={{
              color: '#2688EB',
              fontSize: 20,
              fontWeight: '400',
              alignSelf: 'center',
            }}>
            Сделать фото
          </Text>
        </TouchableOpacity>
        <TouchableOpacity
          style={{
            height: d.height * 0.064,
            borderBottomRightRadius: 15,
            borderBottomLeftRadius: 15,
            backgroundColor: 'white',
            justifyContent: 'center',
          }}
          onPress={async () => {
            handlePress();
            await setFromGallery();
          }}>
          <Text
            style={{
              color: '#2688EB',
              fontSize: 20,
              fontWeight: '400',
              alignSelf: 'center',
            }}>
            Из галереи
          </Text>
        </TouchableOpacity>
        <View
          style={{
            paddingTop: 10,
          }}>
          <TouchableOpacity
            style={{
              borderRadius: 15,
              height: d.height * 0.064,
              backgroundColor: 'white',
              justifyContent: 'center',
            }}
            onPress={handlePress}>
            <Text
              style={{
                color: '#2688EB',
                fontSize: 20,
                fontWeight: '400',
                alignSelf: 'center',
              }}>
              Отменить
            </Text>
          </TouchableOpacity>
        </View>
      </Animated.View>
    </>
  );
}

const styles = StyleSheet.create({
  backdrop: {
    ...StyleSheet.absoluteFillObject,
    backgroundColor: 'rgba(0, 0, 0, 0.3)',
  },
  popup: {
    width: d.width * 0.9,
    backgroundColor: 'transparent',
    alignSelf: 'center',
    position: 'absolute',
    zIndex: 2,
    bottom: 0,
    height: 'auto',
  },
  closeButtonContainer: {
    position: 'absolute',
    top: 10,
    right: 10,
  },
  shadow: {
    shadowColor: 'black',
    shadowOffset: {
      width: 10,
      height: 10,
    },
    shadowOpacity: 1,
    elevation: 5,
  },
});
