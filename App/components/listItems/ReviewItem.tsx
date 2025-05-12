import React from 'react';
import {Text, View} from 'react-native';
import {Icon} from '@rneui/base';
import {Review} from '../../types/DomainTypes.ts';

export default function ReviewItem({review}: {review: Review}) {
  return (
    <View
      style={{
        padding: 10,
        justifyContent: 'center',
        borderColor: 'F5ECE0',
        borderBottomWidth: 1,
      }}>
      <View
        style={{
          flexDirection: 'row',
          alignItems: 'center',
          justifyContent: 'space-between',
        }}>
        <Text
          style={{
            color: 'black',
            fontSize: 16,
            fontWeight: '600',
          }}>
          {review.name}
        </Text>
        <View style={{flexDirection: 'row'}}>
          {Array.from({length: 5}).map((_, i) => (
            <Icon
              key={i}
              type={'material'}
              name={'star'}
              size={15}
              color={i <= review.grade ? '#E4E839' : '#C8C8C8'}/>
          ))}
        </View>
      </View>
    </View>
  );
}
