import React, {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import CustomBottomSheet from './CustomBottomSheet.tsx';
import {Text, TextInput, View} from 'react-native';
import {BottomSheetView} from '@gorhom/bottom-sheet';
import {Icon} from '@rneui/themed';
import {TouchableOpacity} from 'react-native-gesture-handler';
import TextInputTitle from '../TextInputTitle.tsx';
import Styles from '../../constants/Styles.tsx';
import {useDependency} from '../../services/Hooks.ts';
import {OrderResponseService} from '../../services/domain/OrderResponseService.ts';
import {GestureStyledButton} from '../buttons/GestureStyledButton.tsx';

const ReviewBottomSheet = React.forwardRef((
  {responseId, toUserId, toggleModal, close}: {
    responseId: string,
    toUserId: string,
    toggleModal: () => void,
    close: () => void},
  ref: ForwardedRef<BottomSheetMethods>) => {
  const orderResponseService = useDependency<OrderResponseService>('OrderResponseService');

  const gradeNames = React.useMemo(() => ({
    [1]: 'Очень плохо',
    [2]: 'Плохо',
    [3]: 'Нормально',
    [4]: 'Хорошо',
    [5]: 'Отлично',
  }), []);

  const grades = React.useMemo(() => [1, 2, 3, 4, 5], []);

  const [grade, setGrade] = React.useState<number>(1);
  const [text, setText] = React.useState<string>('');

  const review = React.useCallback(async () => {
    const response = await orderResponseService.review(responseId, toUserId, text, grade);

    if (response.result == 'successful') {
      close();
      toggleModal();
    }
  }, [grade, text]);

  return (
    <CustomBottomSheet
      ref={ref}
      title={'Оставить заказ'}
      close={close}>
      <BottomSheetView>
        <Text
          style={{
            alignSelf: 'center',
            color: 'black',
            fontWeight: '600',
            fontSize: 16,
            paddingTop: 20,
            paddingBottom: 10,
          }}>
          {gradeNames[grade]}
        </Text>
        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'space-evenly',
            alignItems: 'center',
          }}>
          {grades.map((_, index) => (
            <TouchableOpacity
              key={index}
              onPress={() => setGrade(_)}>
              <Icon
                name={'star'}
                type={'material'}
                color={grade >= _ ? '#E4E839' : '#CFCFCF'}
                size={50}/>
            </TouchableOpacity>
          ))}
        </View>
        <TextInputTitle s={'Отзыв'} top={20} bottom={5} />
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
            value={text}
            placeholder={
              'Введите текст вашего отзыва'
            }
            onChangeText={setText}
            multiline
          />
        </View>
        <GestureStyledButton
          content={'Оставить отзыв'}
          top={40}
          bottom={5}
          isDisabled={text == ''}
          pressed={review}
          type={'default'}/>
      </BottomSheetView>
    </CustomBottomSheet>
  );
});

export default ReviewBottomSheet;
