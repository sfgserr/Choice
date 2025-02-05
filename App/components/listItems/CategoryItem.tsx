import React from 'react';
import {
  Text, TouchableOpacity,
  View,
  Dimensions, Image,
} from 'react-native';
import {Category} from '../../types/DomainTypes.ts';
import {BottomTabNavigationProp} from '@react-navigation/bottom-tabs';
import {ClientTabProps} from '../../types/NavigationTypes.ts';

export default function CategoryItem({categoryId, categories, navigation}: {categoryId: number, categories: Category[], navigation: BottomTabNavigationProp<ClientTabProps, 'Categories', undefined>}) {
  const { width, height } = Dimensions.get('screen');

  return (
    <View
      style={{
        paddingHorizontal: 20,
        flex: 1
      }}>
      <TouchableOpacity
        style={{
          flexDirection: 'row',
          paddingTop: 10,
          paddingBottom: 10,
          borderBottomWidth: 1,
          borderColor: '#e9e9e9',
          flex: 1
        }}
        onPress={() => {
          navigation.navigate('Map', {
            categoryId,
            categories
          });
        }}>
        <View
          style={{
            width: height*0.054,
            height: height*0.054,
            borderRadius: 10,
            backgroundColor: '#47A4F9',
            justifyContent: 'center'
          }}>
          <Image
            source={{uri: `${process.env.MINIO_URL}/app-files/${categories[categoryId].iconUri}`}}
            style={{
              width: 20,
              height: 20,
              alignSelf: 'center',
              resizeMode: 'contain'
            }}/>
        </View>
        <Text
          style={{
            fontWeight: '400',
            fontSize: 18,
            color: '#181818',
            alignSelf: 'center',
            paddingLeft: 20
          }}>
          {categories[categoryId].title}
        </Text>
        <View
          style={{
            flex: 1,
            flexDirection: 'row',
            justifyContent: 'flex-end'
          }}>
          <Image
            source={require('../../assets/images/chevron-right.png')}
            style={{
              alignSelf: 'center',
              resizeMode: 'contain',
              width: 15,
              height: 15,
              tintColor: '#CDCECF'
            }}/>
        </View>
      </TouchableOpacity>
    </View>
  )
}
