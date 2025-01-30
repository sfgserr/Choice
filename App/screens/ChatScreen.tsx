import {
  Dimensions,
  FlatList,
  Image,
  StyleSheet,
  Text,
  TextInput,
  View,
} from 'react-native';
import {ChatMessages} from '../types/DomainTypes.ts';
import React from 'react';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {ChatService} from '../services/domain/ChatService.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {Icon} from '@rneui/base';
import {StyledButton} from '../components/buttons/StyledButton.tsx';

const d = Dimensions.get('screen');

export default function ChatScreen({id, navigation}: {id: string, navigation: any}) {
  const chatService = useDependency<ChatService>('ChatService');

  const [chat, setChat] = React.useState<ChatMessages | null>(null);

  React.useEffect(() => {
    const getChat = async () => {
      const response = await chatService.getChat(id);

      if (response.content != null) {
        setChat(response.content);
      }
    }
    getChat();
  }, []);

  return (
    <View style={styles.container}>
      {chat != null && (
        <View style={styles.chatBackground}>
          <View style={styles.chat}>
            <FlatList
              data={chat.messages}
              contentContainerStyle={{flex:1}}
              renderItem={item => (
                <View>
                </View>
              )}
              ListEmptyComponent={(
                <View style={{flex: 1, justifyContent: 'center'}}>
                  <Text
                    style={{
                      fontSize: 24,
                      fontWeight: '700',
                      color: 'black',
                      alignSelf: 'center'
                    }}>
                    Нет сообщений
                  </Text>
                  <Text
                    style={{
                      color: '#818C99',
                      fontWeight: '400',
                      fontSize: 16,
                      paddingTop: 20,
                      alignSelf: 'center',
                      textAlign: 'center'
                    }}>
                    Можете запросить у компании любую интересующую Вас информацию или создать заказ и дождаться ответов от компаний рядом с вами
                  </Text>
                  <View style={{paddingHorizontal: 40}}>
                    <StyledButton
                      content={'Создать заказ'}
                      top={20}
                      bottom={0}
                      isDisabled={false}
                      pressed={() => navigation.go('CreateOrderRequest')}/>
                  </View>
                </View>
              )}/>
          </View>
          <View style={styles.userTab}>
            <View style={[styles.horizontalSpread, {paddingBottom: 5}]}>
              <View style={styles.alignToCenterContainer}>
                <NavigateBackButton navigation={navigation} />
              </View>
              <Text style={styles.userName}>{chat.user.name}</Text>
              <Image
                style={styles.icon}
                source={{uri: `${process.env.MINIO_URL}/app-files/${chat.user.iconUri}`}} />
            </View>
          </View>
          <View style={styles.bottomTab}>
            <View style={[styles.horizontalSpread, {paddingTop: 5}]}>
              <View style={styles.alignToCenterContainer}>
                <Icon
                  type={'material'}
                  name={'attachment'}
                  color={'#858E99'}
                  size={25}/>
              </View>
              <View style={styles.textInputBorder}>
                <TextInput
                  style={styles.textInput}
                  placeholder={'Сообщение'}/>
              </View>
              <View style={styles.alignToCenterContainer}>
                <Icon
                  type={'material'}
                  name={'mic'}
                  color={'#858E99'}
                  size={25}/>
              </View>
            </View>
          </View>
        </View>
      )}
      <LongRunningOperationIndicator isRefreshing={chat == null} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white'
  },
  chatBackground: {
    flex: 1,
    backgroundColor: '#F4F5FF'
  },
  userTab: {
    height: d.height * 0.108,
    width: d.width,
    position: 'absolute',
    top: 0,
    justifyContent: 'flex-end',
    backgroundColor: 'white',
    paddingHorizontal: 10
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  alignToCenterContainer: {
    alignSelf: 'center'
  },
  bottomTab: {
    height: d.height * 0.108,
    width: d.width,
    position: 'absolute',
    bottom: 0,
    justifyContent: 'flex-start',
    backgroundColor: 'white',
    paddingHorizontal: 10
  },
  userName: {
    fontWeight: '600',
    fontSize: 17,
    color: 'black',
    alignSelf: 'center'
  },
  icon: {
    width: 35,
    height: 35,
    resizeMode: 'contain',
    alignSelf: 'center'
  },
  textInputBorder: {
    flex: 1,
    paddingLeft: 5,
    borderRadius: 20,
    flexDirection: 'row',
    borderWidth: 1,
    borderColor: '#D1D1D6',
    alignSelf: 'center'
  },
  textInput: {
    fontSize: 15,
    fontWeight: '500',
    color: 'black',
    alignSelf: 'center'
  },
  chat: {
    width: d.width,
    height: d.height * 0.676,
    position: 'absolute',
    top: d.height*0.108,
  }
});
