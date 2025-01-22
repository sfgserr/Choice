import {
  Image,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from 'react-native';
import {CreateOrderResponseScreenProps} from '../types/NavigationTypes.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import OrderRequestRadiusItem from '../components/listItems/OrderRequestRadiusItem.tsx';
import React from 'react';
import {CompanyOrderRequest} from '../types/DomainTypes.ts';
import TextInputTitle from '../components/TextInputTitle.tsx';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import Styles from '../constants/Styles.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {useDependency} from '../services/Hooks.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';
import RNDateTimePicker from '@react-native-community/datetimepicker';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import CustomBottomSheet from '../components/bottomSheets/CustomBottomSheet.tsx';
import BottomSheet, {BottomSheetView} from '@gorhom/bottom-sheet';
import Animated from 'react-native-reanimated';
import AnimatedText from '../components/AnimatedText.tsx';
import {OrderResponseService} from '../services/domain/OrderResponseService.ts';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';

type Form = {
  price: string
  deadlinesIndex: number
  time: Date
  date: Date
}

const ToggleBorder = ({title, value, onPress}: {title: string, value: string, onPress: () => void}) => (
  <>
    <TextInputTitle
      s={title}
      top={20}
      bottom={5}
    />
    <View style={styles.borderedTextInput}>
      <TextInput
        value={value}
        placeholder={'Выберите время выполнения работ'}
        style={Styles.borderedTextInput}
        readOnly
      />
      <TouchableOpacity
        style={styles.chevronDownButton}
        onPress={onPress}>
        <Image
          style={styles.image}
          source={require('../assets/images/chevron-down.png')}
        />
      </TouchableOpacity>
    </View>
  </>
)

export default function CreateOrderResponseScreen({route, navigation}: CreateOrderResponseScreenProps) {
  const orderRequestService = useDependency<OrderRequestService>('OrderRequestService');
  const orderResponseService = useDependency<OrderResponseService>('OrderResponseService');

  const [isRefreshing, setIsRefreshing] = React.useState(false);
  const [isToggled, setIsToggled] = React.useState(false);

  const [showDatePicker, setShowDatePicker] = React.useState(false);
  const [showTimePicker, setShowTimePicker] = React.useState(false);

  const [orderRequest, setOrderRequest] = React.useState<CompanyOrderRequest | null>(null);

  const [form, setForm] = React.useState<Form>({
    price: '',
    deadlinesIndex: -1,
    time: new Date(),
    date: new Date(),
  });

  const validate = () => {
    if (form.price == '' && orderRequest?.toKnowPrice)
      return false;

    if (form.deadlinesIndex == -1 && orderRequest?.toKnowDeadline)
      return false;

    return true;
  }

  const secondsInDay = 24 * 3600;
  const deadlines = [
    {
      title: 'День',
      seconds: secondsInDay
    },
    {
      title: 'Неделя',
      seconds: 7 * secondsInDay
    },
    {
      title: 'Месяц',
      seconds: 30 * secondsInDay
    },
    {
      title: '3 месяца',
      seconds: 90 * secondsInDay
    }
  ]

  React.useEffect(() => {
    const getOrderRequest = async () => {
      const response = await orderRequestService.getOrderRequestAsCompany(
        route.params.orderRequest.id);

      if (response.content != null) {
        setOrderRequest(response.content);
      }
      else {
        navigation.goBack();
      }
    }

    setIsRefreshing(true);
    getOrderRequest();
    setIsRefreshing(false);
  }, []);

  const createOrderResponse = React.useCallback(async () => {
    form.date.setHours(form.time.getHours(), form.time.getMinutes());

    setIsRefreshing(true);

    const result = await orderResponseService.createOrderResponse(
      route.params.orderRequest.id,
      +form.price,
      deadlines[form.deadlinesIndex].seconds,
      form.date,
      0);

    setIsRefreshing(false);

    if (result.result == 'successful')
      setIsToggled(prev => !prev);
  }, [form]);

  const ref = React.useRef<BottomSheet>(null);

  return (
    <>
      <View style={styles.container}>
        <View style={styles.navigateBackButtonContainer}>
          <NavigateBackButton navigation={navigation} />
        </View>
        <Text style={styles.title}>Ответить на заказ</Text>
        <View style={styles.orderRequestItemContainer}>
          <OrderRequestRadiusItem
            orderRequest={route.params.orderRequest}
            categories={route.params.categories}
            navigation={navigation}
            preview
          />
        </View>
        <Text style={styles.subTitle}>Клиент хочет узнать:</Text>
        {orderRequest != null && (
          <>
            {orderRequest.toKnowPrice && (
              <>
                <TextInputTitle s={'Стоимость'} top={20} bottom={5} />
                <BorderedTextInput
                  value={form.price}
                  onChanged={v => setForm(prev => ({...prev, price: v}))}
                  placeholder={'Введите стоимость'}
                  isError={false}
                  isBig={false}
                  keyboard={'number-pad'}
                />
              </>
            )}
            {orderRequest.toKnowDeadline && (
              <ToggleBorder
                title={'Время выполнения работ'}
                value={
                  form.deadlinesIndex == -1
                    ? ''
                    : deadlines[form.deadlinesIndex].title
                }
                onPress={() => ref.current?.expand()}
              />
            )}
            {orderRequest.toKnowEnrollmentDate && (
              <>
                <View style={styles.horizontalSpread}>
                  <View style={styles.dateInputContainer}>
                    <ToggleBorder
                      title={'Дата записи'}
                      value={`${form.date.getDate()}.${
                        form.date.getMonth() + 1
                      }`}
                      onPress={() => setShowDatePicker(prev => !prev)}
                    />
                  </View>
                  <View style={styles.timeInputContainer}>
                    <ToggleBorder
                      title={'Время записи'}
                      value={`${form.time.getHours()}:${
                        form.time.getMinutes() == 0
                          ? '00'
                          : form.time.getMinutes()
                      }`}
                      onPress={() => setShowTimePicker(prev => !prev)}
                    />
                  </View>
                </View>
              </>
            )}
          </>
        )}
        <StyledButton
          content={'Ответить'}
          top={20}
          bottom={5}
          isDisabled={!validate()}
          pressed={async () => createOrderResponse()}
        />
        {showDatePicker && (
          <RNDateTimePicker
            mode={'date'}
            display={'spinner'}
            value={form.time}
            minimumDate={new Date()}
            onChange={(e, d) => {
              if (d != null) setForm({...form, date: d});

              setShowDatePicker(prev => !prev);
            }}
          />
        )}
        {showTimePicker && (
          <RNDateTimePicker
            mode={'time'}
            display={'spinner'}
            value={form.time}
            onChange={(e, d) => {
              if (d != null) setForm({...form, time: d});

              setShowTimePicker(prev => !prev);
            }}
          />
        )}
        <LongRunningOperationIndicator isRefreshing={isRefreshing} />
      </View>
      <CustomBottomSheet
        ref={ref}
        title={'Время выполнения работ'}
        close={() => ref.current?.close()}>
        <BottomSheetView>
          <Animated.View>
            {deadlines.map((v, i) => {
              return (
                <AnimatedText
                  item={v}
                  index={i}
                  key={i}
                  selectedIndex={form.deadlinesIndex}
                  setSelectedIndex={i =>
                    setForm(prev => ({...prev, deadlinesIndex: i}))
                  }
                />
              );
            })}
          </Animated.View>
        </BottomSheetView>
      </CustomBottomSheet>
      <SuccessfulRequestModal
        isToggled={isToggled}
        handlePress={() => {
          setIsToggled(prev => !prev);
          navigation.goBack();
        }}
        title={'Ответ отправлен клиенту'}
        text={'Ожидайте ответа от клиента и старайтесь отвечать оперативно'}
      />
    </>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    paddingHorizontal: 10
  },
  navigateBackButtonContainer: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    paddingTop: 20,
    alignItems: 'center',
  },
  title: {
    position: 'absolute',
    alignSelf: 'center',
    top: 15,
    fontSize: 21,
    fontWeight: '700',
  },
  orderRequestItemContainer: {
    paddingTop: 20
  },
  subTitle: {
    fontSize: 17,
    fontWeight: '700',
    color: 'black',
    paddingTop: 20,
  },
  borderedTextInput: {
    ...Styles.borderedTextInputView,
    ...Styles.borderedTextInputHeight,
    ...Styles.borderedTextInputViewColor,
    ...Styles.borderedTextInputUnfocused
  },
  chevronDownButton: {
    alignSelf: 'center',
    paddingRight: 10
  },
  image: {
    width: 15,
    height: 15,
    resizeMode: 'contain'
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  dateInputContainer: {
    flex: 1,
    paddingRight: 3
  },
  timeInputContainer: {
    flex: 1,
    paddingLeft: 3
  }
});
