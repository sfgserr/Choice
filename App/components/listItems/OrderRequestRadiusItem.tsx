import {Category, OrderRequestRadius} from '../../types/DomainTypes.ts';
import {Dimensions, Image, Text, TouchableOpacity, View} from 'react-native';
import {Icon} from '@rneui/themed';
import React from 'react';
import {StyledButton} from '../buttons/StyledButton.tsx';

const d = Dimensions.get('screen');

export default function OrderRequestRadiusItem({orderRequest, categories, navigation, preview=false}: {
  orderRequest: OrderRequestRadius
  categories: Category[]
  navigation: any
  preview: boolean}) {
  return (
    <View
      style={{
        width: 'auto',
        borderRadius: 10,
        backgroundColor: 'white',
        shadowColor: 'black',
        shadowOffset: {
          width: 10,
          height: 10,
        },
        paddingHorizontal: 10,
        elevation: 2,
      }}>
      <Text
        style={{
          fontWeight: '700',
          fontSize: 14,
          color: 'black',
          paddingTop: 10,
        }}>
        {
          categories[
            categories.findIndex(c => c.categoryId == orderRequest.categoryId)
          ].title
        }
      </Text>
      <Text
        style={{
          fontSize: 15,
          fontWeight: '400',
          color: '#313131',
          paddingTop: 10,
        }}>
        {orderRequest.description}
      </Text>
      {orderRequest.photoUris
        .filter(u => u != '')
        .map((v, i) => (
          <TouchableOpacity
            style={{
              flexDirection: 'row',
              paddingTop: 10,
            }}
            onPress={() => {navigation.navigate('ImageView', {uri: v})}}
            key={i}>
            <Icon
              type={'material'}
              name={'image'}
              size={17}
              color={'#2D81E0'}
              style={{alignSelf: 'center'}}
            />
            <Text
              style={{
                paddingLeft: 5,
                alignSelf: 'center',
                color: '#2D81E0',
                textDecorationLine: 'underline'
              }}>
              {v.length > 15 ? `${v.substring(0, 14)}...` : v}
            </Text>
          </TouchableOpacity>
        ))}
      <View
        style={{paddingTop: 10, paddingBottom: 10}}>
        <View
          style={{
            backgroundColor: '#D7D8D9',
            width: '100%',
            height: .25
          }}/>
        </View>
      <View
        style={{
          flexDirection: 'row'
        }}>
        <Image
          style={{
            width: 40,
            height: 40
          }}
          source={{uri: `${process.env.MINIO_URL}/app-files/${orderRequest.iconUri}`}}/>
        <View style={{paddingLeft: 5, flex: 1}}>
          <View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
            <Text
              style={{
                fontWeight: '500',
                fontSize: 15,
                color: 'black'
              }}>
              {orderRequest.clientName}
            </Text>
            <View style={{flexDirection: 'row'}}>
              <Icon
                name={'star'}
                type={'material'}
                color={'#E4E839'}
                size={20}/>
              <Text
                style={{
                  color: '#575757',
                  fontWeight: '700',
                  fontSize: 12,
                  alignSelf: 'center'
                }}>
                {orderRequest.averageGrade}
              </Text>
            </View>
          </View>
          <Text
            style={{
              fontWeight: '400',
              fontSize: 12,
              color: '#909499'
            }}>
            {`Совершено заказов: ${orderRequest.reviewsCount}`}
          </Text>
        </View>
      </View>
      {!preview ? (
        <>
          <StyledButton
            content={'Ответить'}
            top={10}
            bottom={5}
            isDisabled={false}
            pressed={() => navigation.navigate('CreateOrderResponse', {orderRequest, categories})}/>
        </>) : (<></>)}
    </View>
  );
}
