import {Dimensions, Text, View} from 'react-native';
import TextInputTitle from '../components/TextInputTitle.tsx';
import React from 'react';
import BorderedTextInput from '../components/inputs/BorderedTextInput.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';

export default function ContactDetailsScreen({next}: {next: () => void}) {
  const d = Dimensions.get('screen');

  const [siteUrl, setSiteUrl] = React.useState('');

  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'white',
        paddingTop: 10,
      }}>
      <Text
        style={{
          fontWeight: '700',
          fontSize: 17,
          color: 'black',
        }}>
        Контактные данные
      </Text>
      <Text
        style={{
          fontWeight: '400',
          fontSize: 16,
          color: '#181818',
          paddingTop: 10,
        }}>
        Укажите информацию, которая будет отображаться в карточке вашей
        компании, ее увидят тысячи наших пользователей
      </Text>
      <TextInputTitle s={'Сайт'} top={20} bottom={5} />
      <BorderedTextInput
        value={siteUrl}
        onChanged={setSiteUrl}
        placeholder={'Введите адрес сайта'}
        isError={false}
        isBig={false}
      />
      <View
        style={{
          bottom: 20,
          position: 'absolute',
          alignSelf: 'center',
          width: d.width * 0.9,
        }}>
        <StyledButton
          content={'Далее'}
          top={0}
          bottom={0}
          isDisabled={siteUrl == ''}
          pressed={next}/>
      </View>
    </View>
  );
}
