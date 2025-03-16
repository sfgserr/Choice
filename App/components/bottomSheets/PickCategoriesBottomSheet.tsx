import {Text, View} from 'react-native';
import React, {ForwardedRef} from 'react';
import CustomBottomSheet from './CustomBottomSheet.tsx';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import {BottomSheetFlatList} from '@gorhom/bottom-sheet';
import {Icon} from '@rneui/themed';
import {TouchableOpacity} from 'react-native-gesture-handler';

const PickCategoriesBottomSheet = React.forwardRef(({categories, close, select}: any, ref: ForwardedRef<BottomSheetMethods>) => {
  return (
    <CustomBottomSheet
      title={'Виды деятельности'}
      ref={ref}
      close={close}>
      <BottomSheetFlatList data={categories} renderItem={item => {
        const category = item.item.category;

        return (
          <View
            style={{
              flexDirection: 'row',
              justifyContent: 'space-between',
              paddingVertical: 8,
            }}>
            <Text
              style={{
                fontWeight: '400',
                fontSize: 17,
                color: 'black',
                alignSelf: 'center'
              }}>
              {category.title}
            </Text>
            <TouchableOpacity
              style={{
                alignSelf: 'center',
                borderRadius: 360,
                backgroundColor: item.item.selected ? '#3F8AE0' : '#E2E2E2',
                padding: 3
              }}
              onPress={() => select(item.index)}>
              <Icon
                type={'material'}
                name={'done'}
                size={14}
                color={'white'}/>
            </TouchableOpacity>
          </View>
        )
      }}/>
    </CustomBottomSheet>
  )
});

export default PickCategoriesBottomSheet;
