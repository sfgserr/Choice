import React from 'react';
import {
  Text,
  View,
  Dimensions,
  TextInput,
  TouchableOpacity,
  Image,
  ScrollView,
  StyleSheet,
} from 'react-native';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {EditOrderRequestScreenProps} from '../types/NavigationTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import Styles from '../constants/Styles.tsx';
import {Category, OrderRequestDetails, OrderStatus} from '../types/DomainTypes.ts';
import Checkbox from '../components/buttons/Checkbox.tsx';
import ImageBox, {ImageBoxObject, MinioBlob, UploadedBlob} from '../components/ImageBox.tsx';
import {Slider} from '@miblanchard/react-native-slider';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {GestureHandlerRootView} from 'react-native-gesture-handler';
import BottomSheet from '@gorhom/bottom-sheet';
import CategoriesBottomSheet from '../components/bottomSheets/CategoriesBottomSheet.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import UnsuccessfulRequestModal from '../components/modals/UnsuccessfulRequestModal.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {DateUtils} from '../utils/DateUtils.ts';
import {useDependency} from '../services/Hooks.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {ObjectStorageService} from '../services/object/ObjectStorageService.ts';

const d = Dimensions.get('screen');

export default function EditOrderRequestScreen({route, navigation}: EditOrderRequestScreenProps) {
  const orderRequestService = useDependency<OrderRequestService>('OrderRequestService');
  const categoryService = useDependency<CategoryService>('CategoryService');
  const objectStorageService = useDependency<ObjectStorageService>('ObjectStorageService');

  const [categories, setCategories] = React.useState<Category[]>([]);
  const [status, setStatus] = React.useState<OrderStatus>();
  const [description, setDescription] = React.useState('');
  const [toKnowPrice, setToKnowPrice] = React.useState(false);
  const [toKnowDeadline, setToKnowDeadline] = React.useState(false);
  const [toKnowEnrollmentDate, setToKnowEnrollmentDate] = React.useState(false);
  const [photos, setPhotos] = React.useState<ImageBoxObject[]>([MinioBlob.createDefault(), MinioBlob.createDefault(), MinioBlob.createDefault()]);
  const [radius, setRadius] = React.useState<number>(5);
  const [creationDate, setCreationDate] = React.useState<Date>(new Date());
  const [categoryIndex, setCategoryIndex] = React.useState(0);
  const [isChanged, setIsChanged] = React.useState(false);

  const set = (orderRequest: OrderRequestDetails) => {
    setStatus(orderRequest.status);
    setDescription(orderRequest.description);
    setToKnowPrice(orderRequest.toKnowPrice);
    setToKnowDeadline(orderRequest.toKnowDeadline);
    setToKnowEnrollmentDate(orderRequest.toKnowEnrollmentDate);
    setPhotos(orderRequest.photoUris.map(p => new UploadedBlob(p)));
    setRadius(orderRequest.distance);
    setCreationDate(orderRequest.creationDate);
  };

  React.useEffect(() => {
    async function getData() {
      let response = await orderRequestService
        .getOrderRequest(route.params.orderRequestId);

      if (response.result == 'successful' && response.content != null) {
        let categoriesResponse = await categoryService.getCategories();

        if (categoriesResponse.result == 'successful' && categoriesResponse.content != null) {
          setCategories(categoriesResponse.content);

          setCategoryIndex(categoriesResponse.content.findIndex(c =>
            c.categoryId == response.content.categoryId));

          set(response.content);
        }
      }
    }

    getData();
  }, []);

  const data= React.useMemo(() => [
    {
      title: 'Узнать стоимость',
      checked: toKnowPrice,
      pressed: () => {
        setToKnowPrice(p => !p);
        setIsChanged(true);
      },
    },
    {
      title: 'Узнать время выполнения работ',
      checked: toKnowDeadline,
      pressed: () => {
        setToKnowDeadline(p => !p);
        setIsChanged(true);
      },
    },
    {
      title: 'Узнать время записи',
      checked: toKnowEnrollmentDate,
      pressed: () => {
        setToKnowEnrollmentDate(p => !p);
        setIsChanged(true);
      },
    },
  ], [toKnowPrice, toKnowDeadline, toKnowEnrollmentDate]);

  const [isToggled, setIsToggled] = React.useState(false);
  const [isErrorToggled, setIsErrorToggled] = React.useState(false);
  const [isRefreshing, setIsRefreshing] = React.useState(false);
  const [errorMessage, setErrorMessage] = React.useState('');

  const toggleSuccessfulModal = async () => {
    setIsToggled(prev => !prev);
    navigation.goBack();
  };

  const toggleErrorModal = () => {
    setIsErrorToggled(prev => !prev);
  };

  const editOrderRequest = async () => {
    setIsRefreshing(true);

    const response = await orderRequestService.edit(
      route.params.orderRequestId,
      categories[categoryIndex].categoryId,
      description,
      toKnowPrice,
      toKnowDeadline,
      toKnowEnrollmentDate,
      photos.map(b => b.getObjectName()),
      radius
    );

    if (response.result == 'successful') {
      for (let i = 0; i < photos.length; i++) {
        if (!photos[i].isUpload) {
          await objectStorageService.upload(photos[i] as MinioBlob);
        }
      }
    }

    setIsRefreshing(false);

    if (response.result == 'successful') {
      setIsToggled(prev => !prev);
    }
    else {
      setErrorMessage(response.error);
      toggleErrorModal();
    }
  };

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
          <Text
            style={
              styles.title
            }>{`Заказ №${route.params.orderRequestId.substring(0, 8)}`}</Text>
        </View>
        <View style={styles.contentContainer}>
          <TextInputTitle s={'Создан'} top={20} bottom={5} />
          <View
            style={[
              Styles.borderedTextInputView,
              Styles.borderedTextInputHeight,
              Styles.borderedTextInputViewColor,
              Styles.borderedTextInputUnfocused,
              {alignItems: 'center'},
            ]}>
            <TextInput
              style={Styles.borderedTextInput}
              value={DateUtils.formatDate(creationDate)}
              readOnly
            />
            <View style={styles.statusBoxContainer}>
              <View
                style={{
                  ...styles.statusBox,
                  backgroundColor:
                    status == 'Active'
                      ? '#6DC876'
                      : status == 'Finished'
                      ? '#2D81E0'
                      : '#AEAEB2',
                }}>
                <Text style={styles.status}>
                  {status == 'Active'
                    ? 'Активен'
                    : status == 'Finished'
                    ? 'Завершен'
                    : 'Отменен'}
                </Text>
              </View>
            </View>
          </View>
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
              value={
                categories.length == 0
                  ? 'Услуга'
                  : categories[categoryIndex].title
              }
              readOnly
            />
            <TouchableOpacity
              style={styles.chevronDown}
              onPress={() => ref.current?.expand()}>
              <Image
                style={styles.image}
                source={require('../assets/images/chevron-down.png')}
              />
            </TouchableOpacity>
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
              onChangeText={val => {
                setDescription(val);
                setIsChanged(true);
              }}
              multiline
            />
          </View>
          <View style={{paddingTop: 10}}>
            <TouchableOpacity
              style={[
                Styles.borderedTextInputView,
                Styles.borderedTextInputViewColor,
                Styles.borderedTextInputHeight,
                {justifyContent: 'center'},
              ]}
              onPress={() => {}}>
              <View style={styles.voiceButton}>
                <Image
                  source={require('../assets/images/micro.png')}
                  style={styles.voiceButtonImage}
                />
                <Text style={styles.voiceButtonContent}>Записать голосом</Text>
              </View>
            </TouchableOpacity>
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
              index={0}/>
            <ImageBox
              object={photos[2]}
              setPhoto={setPhotos}
              index={0}/>
          </View>
          <View style={[styles.horizontalSpread, {paddingTop: 20}]}>
            <Text style={Styles.title}>Радиус поиска</Text>
            <Text style={styles.radius}>{`${radius} км`}</Text>
          </View>
          <View style={{paddingTop: 10}}>
            <Slider
              minimumValue={5}
              maximumValue={25}
              value={radius}
              onValueChange={value => {
                setRadius(Math.floor(value[0]));
                setIsChanged(true);
              }}
              thumbTintColor={'white'}
              minimumTrackTintColor={'#007AFF'}
              maximumTrackTintColor={'#e4e4e6'}
              thumbStyle={styles.thumbStyle}
            />
          </View>
          <View style={[styles.horizontalSpread, {paddingTop: 10}]}>
            <Text style={Styles.title}>от 5 км</Text>
            <Text style={Styles.title}>до 25 км</Text>
          </View>
        </View>
        {isChanged ? (
          <>
            <View style={styles.buttonContainer}>
              <StyledButton
                content={'Сохранить изменения'}
                top={0}
                bottom={0}
                isDisabled={
                  description == '' ||
                  (!toKnowPrice && !toKnowDeadline && !toKnowEnrollmentDate) ||
                  photos.every(p => p == '')
                }
                pressed={editOrderRequest}
              />
            </View>
          </>
        ) : (
          <></>
        )}
        <CategoriesBottomSheet
          options={{
            categories,
            categoryIndex,
            onIndexChange: (val, index) => {
              if (val) {
                setCategoryIndex(index);
                setIsChanged(true);
              }
            },
          }}
          ref={ref}
          close={() => ref.current?.close()}
        />
        <SuccessfulRequestModal
          isToggled={isToggled}
          handlePress={toggleSuccessfulModal}
          title={'Изменения сохранены'}
        />
        <UnsuccessfulRequestModal
          isToggled={isErrorToggled}
          handlePress={() => setIsErrorToggled(prev => !prev)}
          errorMessage={errorMessage}
        />
        <LongRunningOperationIndicator isRefreshing={isRefreshing} />
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
  statusBoxContainer: {
    paddingRight: 10,
    alignSelf: 'center'
  },
  statusBox: {
    justifyContent: 'center',
    paddingVertical: 3,
    paddingHorizontal: 6,
    borderRadius: 5,
  },
  status: {
    alignSelf: 'center',
    fontSize: 14,
    fontWeight: '500',
    color: 'white'
  },
  chevronDown: {
    alignSelf: 'center',
    paddingRight: 10
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
    paddingBottom: 5
  },
});
