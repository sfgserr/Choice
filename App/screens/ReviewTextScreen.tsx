import {FlatList, Text, View} from 'react-native';
import {SafeAreaView} from 'react-native-safe-area-context';
import KeyboardAvoidingScrollView from '../components/KeyboardAvoidingScrollView.tsx';
import {useIsFocused} from '@react-navigation/native';
import {Pressable} from 'react-native-gesture-handler';
import {Icon} from '@rneui/themed';
import React, {useCallback, useEffect} from 'react';
import {useDependency} from '../services/Hooks.ts';
import {ReviewText, ReviewTextService} from '../services/domain/ReviewTextService.ts';
import {ArrayUtils, GroupCollection} from '../utils/ArrayUtils.ts';
import GestureBorderedTextInput from '../components/inputs/GestureBordererdTextInput.tsx';

export default function ReviewTextScreen({navigation}: {navigation: any}) {
  const reviewTextService = useDependency<ReviewTextService>('ReviewTextService');

  const isFocused = useIsFocused();

  const grades = React.useMemo(() => [1, 2, 3, 4, 5], []);

  const [grade, setGrade] = React.useState<number>(1);
  const [newText, setText] = React.useState<string>('');
  const [reviewTexts, setReviewTexts] = React.useState<GroupCollection<number, ReviewText>>(null);

  useEffect(() => {
    const getReviewTexts = async () => {
      const response = await reviewTextService.getReviewTexts();
      if (response.content) {
        setReviewTexts(ArrayUtils.groupByKey(response.content, i => i.grade));
      }
    }

    getReviewTexts();
  }, []);

  const add = useCallback(async () => {
    const response = await reviewTextService.addReviewText(grade, newText);

    if (response.result == 'successful') {
      setReviewTexts(prev => {
        prev.add(grade, {grade, id: -1, text: newText, created: true});
        return prev;
      });
      setText('');
    }
  }, [newText, grade]);

  const deleteText = useCallback(async (index: number, id: number) => {
    const response = await reviewTextService.deleteReviewText(id);

    if (response.result == 'successful') {
      setReviewTexts(prev => {
        const copy = new GroupCollection(prev.getAll());
        copy.remove(grade, index);
        return copy;
      });
    }
  }, [grade]);

  return (
    <SafeAreaView
      style={{
        flex: 1,
        backgroundColor: 'white',
      }}>
      <KeyboardAvoidingScrollView
        scrollable
        tabs={false}
        focused={isFocused}>
        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'space-evenly',
            alignItems: 'center',
            paddingTop: 20,
          }}>
          {grades.map((_, index) => (
            <Pressable
              key={index}
              onPress={() => setGrade(_)}>
              <Icon
                name={'star'}
                type={'material'}
                color={grade >= _ ? '#E4E839' : '#CFCFCF'}
                size={50}/>
            </Pressable>
          ))}
        </View>
        <View
          style={{
            flexDirection: 'row',
            paddingTop: 10,
            alignItems: 'center',
            gap: 15,
            paddingHorizontal: 15
          }}>
          <View
            style={{flex: 1}}>
            <GestureBorderedTextInput
              value={newText}
              onChanged={setText}
              placeholder={'Отзыв'}
              isError={false}
              isBig={false}
              keyboard={'default'}
              isReadonly={false}/>
          </View>
          <Pressable
            style={{
              alignSelf: 'center',
              opacity: newText == '' ? 0.5 : 1
            }}
            disabled={newText == ''}
            onPress={add}>
            <Icon
              type={'material'}
              name={'add'}
              size={25}
              color={'#2D81E0'}/>
          </Pressable>
        </View>
        <FlatList
          style={{
            flex: 1,
            paddingTop: 15,
          }}
          data={reviewTexts?.getGroup(grade)}
          renderItem={r => (
            <View
              style={{
                flexDirection: 'row',
                justifyContent: 'space-between',
                paddingHorizontal: 15,
                paddingBottom: 10
              }}
              key={r.index}>
              <Text
                style={{
                  fontSize: 16,
                  fontWeight: '500',
                  color: 'black',
                  alignSelf: 'center'
                }}>
                {r.item.text}
              </Text>
              <Pressable
                style={{
                  alignSelf: 'center',
                  opacity: r.item.created ? 0.5 : 1
                }}
                onPress={async () => await deleteText(r.index, r.item.id)}
                disabled={r.item.created}>
                <Icon
                  type={'material'}
                  name={'delete'}
                  size={25}
                  color={'#6c757d'}/>
              </Pressable>
            </View>
          )}/>
      </KeyboardAvoidingScrollView>
    </SafeAreaView>
  )
}
