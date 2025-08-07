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
import {ReviewText, ReviewTextService} from '../../services/domain/ReviewTextService.ts';
import {ArrayUtils, GroupCollection} from '../../utils/ArrayUtils.ts';
import {Dropdown} from 'react-native-element-dropdown';

const ReviewBottomSheet = React.forwardRef((
  {responseId, toUserId, toggleModal, close}: {
    responseId: string,
    toUserId: string,
    toggleModal: () => void,
    close: () => void},
  ref: ForwardedRef<BottomSheetMethods>) => {
  const orderResponseService = useDependency<OrderResponseService>('OrderResponseService');
  const reviewTextService = useDependency<ReviewTextService>('ReviewTextService');

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
  const [reviewTexts, setTexts] = React.useState<GroupCollection<number, ReviewText>>(null);

  const review = React.useCallback(async () => {
    const response = await orderResponseService.review(responseId, toUserId, text, grade);

    if (response.result == 'successful') {
      close();
      toggleModal();
    }
  }, [grade, text]);

  React.useEffect(() => {
    const getTexts = async () => {
      const response = await reviewTextService.getReviewTexts();

      if (response.content) {
        setTexts(ArrayUtils.groupByKey(response.content, t => t.grade));
      }
    }

    getTexts();
  }, []);

  React.useEffect(() => {
    console.log(`Text is ${text}`);
  }, [text]);

  return (
    <CustomBottomSheet ref={ref} title={'Оставить заказ'} close={close}>
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
            paddingBottom: 20
          }}>
          {grades.map((_, index) => (
            <TouchableOpacity key={index} onPress={() => {
              setText('');
              setGrade(_);
            }}>
              <Icon
                name={'star'}
                type={'material'}
                color={grade >= _ ? '#E4E839' : '#CFCFCF'}
                size={50}
              />
            </TouchableOpacity>
          ))}
        </View>
        {reviewTexts && (
          <Dropdown
            style={[
              Styles.borderedTextInputView,
              Styles.borderedTextInputViewColor,
              Styles.borderedTextInputUnfocused,
              {flexDirection: 'column', paddingVertical: 10, paddingHorizontal: 5}
            ]}
            selectedTextStyle={Styles.borderedTextInput}
            inputSearchStyle={Styles.borderedTextInput}
            placeholder={'Отзыв'}
            valueField={'text'}
            labelField={'text'}
            value={text}
            placeholderStyle={[Styles.borderedTextInput, {color: '#6D7885'}]}
            data={reviewTexts.getGroup(grade)}
            onChange={v => {
              setText(v.text);
            }}/>
        )}
        <GestureStyledButton
          content={'Оставить отзыв'}
          top={40}
          bottom={5}
          isDisabled={text == ''}
          pressed={review}
          type={'default'}
        />
      </BottomSheetView>
    </CustomBottomSheet>
  );
});

export default ReviewBottomSheet;
