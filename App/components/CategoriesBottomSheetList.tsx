import {StyleSheet, Switch, Text, View} from 'react-native';
import {BottomSheetFlatList} from '@gorhom/bottom-sheet';
import React from 'react';
import {CategoriesBottomSheetListProps} from '../types/ComponentTypes.ts';

export default function CategoriesBottomSheetList({categories, categoryIndex, onIndexChange}: CategoriesBottomSheetListProps) {
  return (
    <BottomSheetFlatList
      data={categories}
      renderItem={item => {
        return <View style={styles.container}>
          <Text style={styles.text}>
            {item.item.title}
          </Text>
          <Switch
            onValueChange={(val) => onIndexChange(val, item.index)}
            disabled={categoryIndex == item.index}
            value={categoryIndex == item.index}
            thumbColor={'white'}
            trackColor={{true: '#2688EB', false: '#001C3D14'}}/>
        </View>
      }}/>
  )
}

const styles = StyleSheet.create({
  container: {
    paddingBottom: 10,
    flexDirection: 'row',
    justifyContent: 'space-between'
  },
  text: {
    color: 'black',
    alignSelf: 'center',
    fontWeight: '400',
    fontSize: 17
  }
});
