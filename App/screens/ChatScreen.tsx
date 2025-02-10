import {
  BackHandler,
  DeviceEventEmitter,
  Dimensions,
  FlatList,
  Image,
  StyleSheet,
  Text,
  TextInput, TouchableOpacity,
  View, ViewToken,
} from 'react-native';
import {Category, ChatMessages, Message, OrderRequest} from '../types/DomainTypes.ts';
import React from 'react';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {ChatService} from '../services/domain/ChatService.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {Icon} from '@rneui/base';
import {StyledButton} from '../components/buttons/StyledButton.tsx';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {UserService} from '../services/domain/UserService.ts';
import MessageItem from '../components/listItems/MessageItem.tsx';
import {launchImageLibrary} from "react-native-image-picker";

const d = Dimensions.get('screen');

export default function ChatScreen({id, navigation, onGoBack}: {id: string, navigation: any, onGoBack: () => void}) {
  const chatService = useDependency<ChatService>('ChatService');
  const categoryService = useDependency<CategoryService>('CategoryService');
  const userService = useDependency<UserService>('UserService');

  const [chat, setChat] = React.useState<ChatMessages | null>(null);
  const [status, setStatus] = React.useState<boolean>(false);
  const [categories, setCategories] = React.useState<Category[]>([]);
  const [userId, setUserId] = React.useState<string>('');

  const [count, setCount] = React.useState(0);

  const [message, setMessage] = React.useState('');

  React.useEffect(() => {
    const backPress = () => {
      navigation.goBack();

      onGoBack();

      return true;
    };

    DeviceEventEmitter.addListener('messageSent', (message: Message) => {
      setChat(prev => {
        prev?.messages.push(message);
        return prev;
      });
    });

    DeviceEventEmitter.addListener('read', (id: string) => {
      setChat(prev => {
        if (prev != null) {
          prev.messages[prev.messages.findIndex(m => String(m.id) === String(id))].isRead = true;
        }
        return prev;
      });
    });

    BackHandler.addEventListener('hardwareBackPress', backPress);

    return () => {
      DeviceEventEmitter.removeAllListeners('messageSent');
      DeviceEventEmitter.removeAllListeners('read');
      BackHandler.removeEventListener('hardwareBackPress', backPress);
    };
  }, []);

  React.useEffect(() => {
    const getUser = async () => {
      const user = await userService.getUser();

      if (user.id != undefined)
        setUserId(user.id);
    };
    const getChat = async () => {
      const response = await chatService.getChat(id);

      if (response.content != null) {
        setChat(response.content);
      }
    };
    const getCategories = async () => {
      const response = await categoryService.getCategories();

      if (response.content != null) {
        setCategories(response.content);
      }
    };
    getUser();
    getChat();
    getCategories();
  }, []);

  React.useEffect(() => {
    const getStatus = () => {
      chatService.getStatus(chat.user.id).then(r => {
        if (r.content != null) {
          setStatus(r.content);
        }
      });
    };
    if (chat != null) {
      const timeout = count == 0 ? 0 : 10000;

      getStatus();

      setTimeout(async () => {
        setCount(prev => prev + 1);
      }, timeout);
    }
  }, [count, chat]);

  const createMessage = React.useCallback(async () => {
    if (chat != null) {
      const response = await chatService.create(
        chat.user.id,
        message,
        'Text');

      if (response.content != null) {
        chat.messages.push(response.content);
      }
    }
  }, [message, chat]);

  const sendMessage = React.useCallback(async () => {
    await createMessage();
    setMessage('');
  }, [createMessage]);

  const launchLibrary = React.useCallback(async () => {
    if (chat != null) {
      const imagePickerResponse = await launchImageLibrary({mediaType: 'photo'});

      if (imagePickerResponse.assets && imagePickerResponse.assets[0].uri != undefined) {
        const response = await chatService.createImage(chat.user.id, imagePickerResponse.assets[0].uri);

        if (response && response.content != null) {
          chat.messages.push(response.content);
        }
      }
    }
    setMessage('');
  }, [chat]);

  const onViewAbleItemsChanged = React.useCallback(
    (info: {
      viewableItems: ViewToken<Message>[];
      changed: ViewToken<Message>[];
    }) => {
      for (let i = 0; i < info.changed.length; i++) {
        if (!info.changed[i].item.isRead && info.changed[i].item.fromUserId != userId) {
          chatService.read(
            info.changed[i].item.fromUserId,
            info.changed[i].item.id,
          ).then(r => {
            if (r.result == 'successful') {
              setChat(prev => {
                let index = prev?.messages.findIndex(m => m.id == info.changed[i].item.id);

                if (index != undefined && index != -1 && prev != null) {
                  prev.messages[index].isRead = true;
                }

                return prev;
              });
            }
          });
        }
      }
    },
    [chat],
  );

  const Stub = React.useMemo(() => (
    <View style={styles.stubContainer}>
      <Text style={styles.stubTitle}>Нет сообщений</Text>
      <Text style={styles.stubText}>
        Можете запросить у компании любую интересующую Вас информацию или создать заказ и дождаться ответов от компаний рядом с вами
      </Text>
      <View style={{paddingHorizontal: 40}}>
        <StyledButton
          content={'Создать заказ'}
          top={20}
          bottom={0}
          isDisabled={false}
          pressed={() => navigation.navigate('CreateOrderRequest', {
            categories,
            categoryIndex: 0,
            onGoBack: (o: OrderRequest) => {}})}/>
      </View>
    </View>
  ), [categories]);

  return (
    <View style={styles.container}>
      {chat != null && categories.length > 0 && (
        <View style={styles.chatBackground}>
          <View style={styles.chat}>
            <FlatList
              data={chat.messages}
              showsVerticalScrollIndicator={false}
              style={{flex: 1}}
              renderItem={(item) =>
                <MessageItem
                  item={item}
                  userId={userId}/>
              }
              ListEmptyComponent={Stub}
              viewabilityConfig={{viewAreaCoveragePercentThreshold: 50}}
              onViewableItemsChanged={onViewAbleItemsChanged}/>
          </View>
          <View style={styles.userTab}>
            <View style={[styles.horizontalSpread, {paddingBottom: 5}]}>
              <View style={styles.alignToCenterContainer}>
                <NavigateBackButton navigation={navigation} onGoBack={onGoBack}/>
              </View>
              <View>
                <Text style={styles.userName}>{chat.user.name}</Text>
                <Text style={styles.status}>{status ? 'В сети' : 'Не в сети'}</Text>
              </View>
              <Image
                style={styles.icon}
                source={{uri: `${process.env.MINIO_URL}/app-files/${chat.user.iconUri}`}} />
            </View>
          </View>
          <View style={styles.bottomTab}>
            <View style={[styles.horizontalSpread, {paddingTop: 5}]}>
              <TouchableOpacity
                style={styles.alignToCenterContainer}
                onPress={launchLibrary}>
                <Icon
                  type={'material'}
                  name={'attachment'}
                  color={'#858E99'}
                  size={25}/>
              </TouchableOpacity>
              <View style={styles.textInputBorder}>
                <TextInput
                  value={message}
                  onChangeText={setMessage}
                  multiline={false}
                  maxLength={50}
                  style={styles.textInput}
                  placeholder={'Сообщение'}/>
              </View>
              <TouchableOpacity
                  style={styles.alignToCenterContainer}
                  onPress={sendMessage}
                  disabled={message.length == 0}>
                <Icon
                  type={'material'}
                  name={'send'}
                  color={'#858E99'}
                  size={25}/>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      )}
      <LongRunningOperationIndicator isRefreshing={chat == null || categories.length == 0} />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  chatBackground: {
    flex: 1,
    backgroundColor: '#F4F5FF',
    paddingTop: d.height * 0.108,
    paddingBottom: d.height * 0.108,
  },
  userTab: {
    height: d.height * 0.108,
    width: d.width,
    position: 'absolute',
    top: 0,
    justifyContent: 'flex-end',
    backgroundColor: 'white',
    paddingHorizontal: 10,
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  alignToCenterContainer: {
    alignSelf: 'center',
  },
  bottomTab: {
    height: d.height * 0.108,
    width: d.width,
    position: 'absolute',
    bottom: 0,
    justifyContent: 'flex-start',
    backgroundColor: 'white',
    paddingHorizontal: 10,
  },
  userName: {
    fontWeight: '600',
    fontSize: 17,
    color: 'black',
    alignSelf: 'center',
  },
  icon: {
    width: 35,
    height: 35,
    resizeMode: 'contain',
    alignSelf: 'center',
    borderRadius: 17.5,
  },
  textInputBorder: {
    flex: 1,
    paddingLeft: 5,
    borderRadius: 20,
    flexDirection: 'row',
    borderWidth: 1,
    borderColor: '#D1D1D6',
    alignSelf: 'center',
  },
  textInput: {
    fontSize: 15,
    fontWeight: '500',
    color: 'black',
    alignSelf: 'center',
    flex: 1,
  },
  chat: {
    width: d.width,
    flex: 1,
  },
  stubContainer: {
    flex: 1,
    justifyContent: 'center',
  },
  stubTitle: {
    fontSize: 24,
    fontWeight: '700',
    color: 'black',
    alignSelf: 'center',
  },
  stubText: {
    color: '#818C99',
    fontWeight: '400',
    fontSize: 16,
    paddingTop: 20,
    alignSelf: 'center',
    textAlign: 'center',
  },
  status: {
    color: '#787878',
    fontWeight: '400',
    fontSize: 13,
    alignSelf: 'center',
  },
});
