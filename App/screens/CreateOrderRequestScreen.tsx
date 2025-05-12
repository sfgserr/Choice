import React from 'react';
import {
  Text,
  View,
  Dimensions,
  Image,
  ScrollView,
  StyleSheet,
} from 'react-native';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {CreateOrderRequestScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import Styles from '../constants/Styles.tsx';
import Checkbox from '../components/buttons/Checkbox.tsx';
import ImageBox, {ImageBoxObject, MinioBlob} from '../components/ImageBox.tsx';
import {Slider} from 'react-native-awesome-slider';
import {GestureHandlerRootView, TextInput} from 'react-native-gesture-handler';
import BottomSheet from '@gorhom/bottom-sheet';
import CategoriesBottomSheet from '../components/bottomSheets/CategoriesBottomSheet.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import UnsuccessfulRequestModal from '../components/modals/UnsuccessfulRequestModal.tsx';
import {OrderRequest} from '../types/DomainTypes.ts';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';
import {ObjectStorageService} from '../services/object/ObjectStorageService.ts';
import Voice from '@react-native-voice/voice';
import {Pressable} from 'react-native-gesture-handler';
import {GestureStyledButton} from '../components/buttons/GestureStyledButton.tsx';
import {useSharedValue} from 'react-native-reanimated';

const d = Dimensions.get('screen');

export default function CreateOrderRequestScreen({route, navigation}: CreateOrderRequestScreenProps) {
  const orderRequestService = useDependency<OrderRequestService>('OrderRequestService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');

  const categories = route.params.categories;

  const [description, setDescription] = React.useState('');
  const [toKnowPrice, setToKnowPrice] = React.useState(false);
  const [toKnowDeadline, setToKnowDeadline] = React.useState(false);
  const [toKnowEnrollmentDate, setToKnowEnrollmentDate] = React.useState(false);
  const [photos, setPhotos] = React.useState<ImageBoxObject[]>([
    MinioBlob.createDefault(),
    MinioBlob.createDefault(),
    MinioBlob.createDefault()]);
  const [radius, setRadius] = React.useState<number>(5);

  const [categoryIndex, setCategoryIndex] = React.useState(route.params.categoryIndex);

  const [orderRequest, setOrderRequest] = React.useState<OrderRequest>();

  const data = React.useMemo(() => ([
    {
      title: 'Узнать стоимость',
      checked: toKnowPrice,
      pressed: () => setToKnowPrice(p => !p),
    },
    {
      title: 'Узнать время выполнения работ',
      checked: toKnowDeadline,
      pressed: () => setToKnowDeadline(p => !p),
    },
    {
      title: 'Узнать время записи',
      checked: toKnowEnrollmentDate,
      pressed: () => setToKnowEnrollmentDate(p => !p),
    },
  ]), [toKnowPrice, toKnowDeadline, toKnowEnrollmentDate]);

  const [isToggled, setIsToggled] = React.useState(false);
  const [isErrorToggled, setIsErrorToggled] = React.useState(false);
  const [isRefreshing, setIsRefreshing] = React.useState(false);
  const [errorMessage, setErrorMessage] = React.useState('');
  const [recording, setRecording] = React.useState(false);

  const toggleSuccessfulModal = async () => {
    setIsToggled(prev => !prev);
    navigation.goBack();
    if (orderRequest != undefined) {
      orderRequest.categoryTitle = categories[categoryIndex].title;
      orderRequest.orderStatus = 'Active';
      await route.params.onGoBack(orderRequest);
    }
  };

  const toggleErrorModal = () => {
    setIsErrorToggled(prev => !prev);
  };

  const createOrderRequest = async () => {
    setIsRefreshing(true);

    const response = await orderRequestService.create(
      categories[categoryIndex].categoryId,
      description,
      toKnowPrice,
      toKnowDeadline,
      toKnowEnrollmentDate,
      photos.map(mb => mb.getObjectName()),
      Math.round(radius));

    if (response.result == 'successful' && response.content != null) {
      for (let i = 0; i < photos.length; i++) {
        await objectStorageService.upload(photos[i] as MinioBlob);
      }

      setIsToggled(prev => !prev);
      setOrderRequest(response.content);
    }
    else {
      setErrorMessage(response.error);
      toggleErrorModal();
    }

    setIsRefreshing(false);
  };

  const progress = useSharedValue(5);
  const min = useSharedValue(5);
  const max = useSharedValue(25);

  const record = async () => {
    if (recording) {
      await Voice.stop();
    } else {
      try {
        await Voice.start('ru-RU');
      } catch (e) {
        console.error(e);
      }
    }

    setRecording(prev => !prev);
  };

  React.useEffect(() => {
    Voice.onSpeechResults = e => {
      if (e.value != undefined) {
        setDescription(e.value[0]);
      }
      setRecording(false);
    };
    Voice.onSpeechError = e => {
      console.error(e.error?.message);
    };

    return () => Voice.removeAllListeners();
  }, []);

  const ref = React.useRef<BottomSheet>(null);

  return (
    <GestureHandlerRootView>
      <ScrollView
        style={styles.container}
        showsVerticalScrollIndicator={false}
        scrollEnabled={!isToggled}>
        <View style={styles.titleView}>
          <View style={{flexDirection: 'row'}}>
            <NavigateBackButton navigation={navigation} onGoBack={undefined}/>
          </View>
          <Text style={styles.title}>Создание заказа</Text>
        </View>
        <View style={styles.contentContainer}>
          <TextInputTitle s={'Категория услуг'} top={20} bottom={5} />
          <View
            style={[
              Styles.borderedTextInputView,
              Styles.borderedTextInputHeight,
              Styles.borderedTextInputViewColor,
              Styles.borderedTextInputUnfocused,
            ]}>
            <TextInput
              style={Styles.borderedTextInput}
              value={categories[categoryIndex].title}
              readOnly
            />
            <Pressable
              style={styles.chevronDown}
              onPress={() => ref.current?.expand()}>
              <Image
                style={styles.image}
                source={require('../assets/images/chevron-down.png')}
              />
            </Pressable>
          </View>
          <TextInputTitle s={'Описание задачи'} top={20} bottom={5} />
          <View
            style={[
              Styles.borderedTextInputView,
              Styles.borderedTextInputBigHeight,
              Styles.borderedTextInputViewColor,
              Styles.borderedTextInputUnfocused,
              {alignItems: 'baseline'},
            ]}>
            <TextInput
              style={Styles.borderedTextInput}
              value={description}
              placeholder={
                'Введите подробности задачи, в чем вам нужна помощь и какой вы ожидаете результат'
              }
              onChangeText={setDescription}
              multiline
            />
          </View>
          <View style={{paddingTop: 10}}>
            <Pressable
              style={[
                Styles.borderedTextInputView,
                Styles.borderedTextInputViewColor,
                Styles.borderedTextInputHeight,
                {justifyContent: 'center', paddingVertical: 10},
              ]}
              onPress={record}>
              <View style={[styles.voiceButton]}>
                {recording ? (
                  <View
                    style={{
                      width: 20,
                      height: 20,
                      borderRadius: 5,
                      backgroundColor: '#2688EB',
                    }}/>
                  ) : (
                  <>
                    <Image
                      source={require('../assets/images/micro.png')}
                      style={styles.voiceButtonImage}
                    />
                    <Text style={styles.voiceButtonContent}>Записать голосом</Text>
                  </>)}
              </View>
            </Pressable>
          </View>
          <TextInputTitle s={'Что узнать у продавца'} top={20} bottom={5} />
          {data.map((item, index) => {
            return (
              <View key={index} style={styles.checkBoxContainer}>
                <Checkbox checked={item.checked} pressed={item.pressed} />
                <Text style={styles.checkBoxTitle}>{item.title}</Text>
              </View>
            );
          })}
          <TextInputTitle
            s={'Приложите файлы или фото к заказу'}
            top={20}
            bottom={5}
          />
          <View style={styles.horizontalSpread}>
            <ImageBox
              object={photos[0]}
              setPhoto={setPhotos}
              index={0}/>
            <ImageBox
              object={photos[1]}
              setPhoto={setPhotos}
              index={1}/>
            <ImageBox
              object={photos[2]}
              setPhoto={setPhotos}
              index={2}/>
          </View>
          <View style={[styles.horizontalSpread, {paddingTop: 20}]}>
            <Text style={Styles.title}>Радиус поиска</Text>
            <Text style={styles.radius}>
              {`${Math.round(radius)} км`}
            </Text>
          </View>
          <View style={{paddingTop: 10}}>
            <Slider
              minimumValue={min}
              maximumValue={max}
              progress={progress}
              onSlidingComplete={n => setRadius(Math.round(n))}
              steps={1}
              renderBubble={() => (<></>)}
              renderThumb={() => (
                <View
                  style={{
                    width: 20,
                    height: 20,
                    borderRadius: 10,
                    backgroundColor: 'white',
                    shadowColor: 'black',
                    elevation: 2,
                  }}/>
              )}
              theme={{
                minimumTrackTintColor: '#007AFF',
                maximumTrackTintColor: '#e4e4e6',
              }}/>
          </View>
          <View style={[styles.horizontalSpread, {paddingTop: 10}]}>
            <Text style={Styles.title}>от 5 км</Text>
            <Text style={Styles.title}>до 25 км</Text>
          </View>
        </View>
        <View style={styles.buttonContainer}>
          <GestureStyledButton
            content={'Создать заказ'}
            top={0}
            bottom={0}
            isDisabled={
              description == '' ||
              (!toKnowPrice && !toKnowDeadline && !toKnowEnrollmentDate)
            }
            pressed={createOrderRequest}
            type={'default'}
          />
        </View>
        <CategoriesBottomSheet
          options={{
            categories,
            categoryIndex,
            onIndexChange: (val, index) => {
              if (val) {setCategoryIndex(index);}
            },
          }}
          ref={ref}
          close={() => ref.current?.close()}
        />
        <SuccessfulRequestModal
          isToggled={isToggled}
          handlePress={toggleSuccessfulModal}
          title={'Заказ создан'}
          text={'Тысячи компаний увидят ваш заказ и ответят вам в самое ближайшее время'}/>
        <UnsuccessfulRequestModal
          isToggled={isErrorToggled}
          handlePress={() => setIsErrorToggled(prev => !prev)}
          errorMessage={errorMessage}/>
        <LongRunningOperationIndicator isRefreshing={isRefreshing}/>
      </ScrollView>
    </GestureHandlerRootView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  titleView: {
    height: d.height * 0.086,
    width: '100%',
    backgroundColor: 'white',
    justifyContent: 'center',
    alignItems: 'baseline',
  },
  title: {
    color: 'black',
    fontSize: 21,
    fontWeight: '600',
    alignSelf: 'center',
    position: 'absolute',
  },
  contentContainer: {
    flex: 1,
    paddingHorizontal: 15,
  },
  chevronDown: {
    alignSelf: 'center',
    paddingRight: 10,
  },
  image: {
    resizeMode: 'contain',
    width: 15,
    height: 15,
  },
  voiceButton: {
    flexDirection: 'row',
    justifyContent: 'center',
  },
  voiceButtonImage: {
    resizeMode: 'contain',
    width: 20,
    height: 20,
    alignSelf: 'center',
  },
  voiceButtonContent: {
    color: '#2688EB',
    fontWeight: '500',
    fontSize: 17,
    alignSelf: 'center',
  },
  checkBoxContainer: {
    flexDirection: 'row',
    paddingTop: 10,
  },
  checkBoxTitle: {
    alignSelf: 'center',
    fontWeight: '400',
    fontSize: 15,
    paddingLeft: 10,
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  radius: {
    fontWeight: '600',
    fontSize: 14,
    color: 'black',
  },
  thumbStyle: {
    shadowColor: 'red',
    elevation: 1,
    shadowRadius: 50,
    shadowOpacity: 0.5,
    shadowOffset: {
      width: 0,
      height: 2,
    },
  },
  buttonContainer: {
    paddingHorizontal: 15,
    paddingTop: 30,
    paddingBottom: 5,
  },
});
