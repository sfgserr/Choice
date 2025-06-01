import React from 'react';
import {Dimensions, Text, TextInput, View} from 'react-native';
import {GestureHandlerRootView, Pressable} from 'react-native-gesture-handler';
import {EditReviewScreenProps} from '../types/NavigationTypes.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {Icon} from '@rneui/themed';
import TextInputTitle from '../components/TextInputTitle.tsx';
import Styles from '../constants/Styles.tsx';
import {GestureStyledButton} from '../components/buttons/GestureStyledButton.tsx';
import {Review} from '../types/DomainTypes.ts';
import UnsuccessfulRequestModal from '../components/modals/UnsuccessfulRequestModal.tsx';
import {useDependency} from '../services/Hooks.ts';
import {AdminService} from '../services/domain/AdminService.ts';
import {SafeAreaView} from "react-native-safe-area-context";

const d = Dimensions.get('screen');

export default function EditReviewScreen({navigation, route}: EditReviewScreenProps) {
  const adminService = useDependency<AdminService>('AdminService');

  const gradeNames = React.useMemo(() => ({
    [1]: 'Очень плохо',
    [2]: 'Плохо',
    [3]: 'Нормально',
    [4]: 'Хорошо',
    [5]: 'Отлично',
  }), []);

  const grades = React.useMemo(() => [1, 2, 3, 4, 5], []);

  const [review, setReview] = React.useState<Review>(route.params.review);

  const [isChanged, setIsChanged] = React.useState(false);
  const [isToggled, setIsToggled] = React.useState(false);
  const [errorMessage, setErrorMessage] = React.useState('');

  const set = React.useCallback((update: (r: Review) => Review) => {
    setReview(update);
    setIsChanged(true);
  }, []);

  const edit = React.useCallback(async () => {
    const response = await adminService.editReview(review.id, review.grade, review.text);

    if (response.result != 'successful') {
      const error = response.error.length > 30 ? response.error.substring(0, 30) : response.error;
      setErrorMessage(error);
      setIsToggled(prev => !prev);
    } else {
      navigation.goBack();
    }
  }, [review]);

  return (
    <GestureHandlerRootView>
      <SafeAreaView style={{flex: 1}}>
        <View style={{flex: 1, backgroundColor: 'white'}}>
          <View
            style={{
              alignItems: 'baseline',
              justifyContent: 'center',
              height: d.height * 0.086,
            }}>
            <View style={{flexDirection: 'row', paddingHorizontal: 15}}>
              <NavigateBackButton navigation={navigation} onGoBack={undefined} />
            </View>
            <Text
              style={{
                fontSize: 21,
                fontWeight: '600',
                color: 'black',
                alignSelf: 'center',
                position: 'absolute',
              }}>
              Отзыв
            </Text>
          </View>
          <View style={{paddingHorizontal: 15}}>
            <Text
              style={{
                alignSelf: 'center',
                color: 'black',
                fontWeight: '600',
                fontSize: 16,
                paddingTop: 20,
                paddingBottom: 10,
              }}>
              {gradeNames[review.grade]}
            </Text>
            <View
              style={{
                flexDirection: 'row',
                justifyContent: 'space-evenly',
                alignItems: 'center',
              }}>
              {grades.map((_, index) => (
                <Pressable key={index} onPress={() => set(r => ({...r, grade: _}))}>
                  <Icon
                    name={'star'}
                    type={'material'}
                    color={review.grade >= _ ? '#E4E839' : '#CFCFCF'}
                    size={50}
                  />
                </Pressable>
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
                value={review.text}
                placeholder={'Введите текст вашего отзыва'}
                onChangeText={text => set(r => ({...r, text}))}
                multiline
              />
            </View>
            <GestureStyledButton
              content={'Сохранить'}
              top={40}
              bottom={5}
              isDisabled={review.text == '' || !isChanged}
              pressed={edit}
              type={'default'}
            />
          </View>
        </View>
        <UnsuccessfulRequestModal
          isToggled={isToggled}
          handlePress={() => setIsToggled(prev => !prev)}
          errorMessage={errorMessage}/>
      </SafeAreaView>
    </GestureHandlerRootView>
  );
}
