import {
  BackHandler,
  DeviceEventEmitter,
  Dimensions,
  FlatList,
  Image,
  StyleSheet,
  Text,
  View, ViewToken,
} from 'react-native';
import {Category, ChatUser, Message, OrderRequest} from '../types/DomainTypes.ts';
import React from 'react';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {ChatService} from '../services/domain/ChatService.ts';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {Icon} from '@rneui/base';
import {GestureStyledButton} from '../components/buttons/GestureStyledButton.tsx';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {UserService} from '../services/domain/UserService.ts';
import MessageItem from '../components/listItems/MessageItem.tsx';
import {launchImageLibrary} from 'react-native-image-picker';
import {ArrayUtils} from '../utils/ArrayUtils.ts';
import BottomSheet from '@gorhom/bottom-sheet';
import ReviewBottomSheet from '../components/bottomSheets/ReviewBottomSheet.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import {TextInput, Pressable} from 'react-native-gesture-handler';

const d = Dimensions.get('screen');

export default function ChatScreen({id, navigation, onGoBack}: {id: string, navigation: any, onGoBack: () => void}) {
  const chatService = useDependency<ChatService>('ChatService');
  const categoryService = useDependency<CategoryService>('CategoryService');
  const userService = useDependency<UserService>('UserService');

  const [chatUser, setChatUser] = React.useState<ChatUser | null>(null);
  const [messages, setMessages] = React.useState<Message[]>([]);
  const [status, setStatus] = React.useState<boolean>(false);
  const [categories, setCategories] = React.useState<Category[]>([]);
  const [userId, setUserId] = React.useState<string>('');

  const [count, setCount] = React.useState(0);

  const [message, setMessage] = React.useState('');

  const [responseId, setResponseId] = React.useState<string>('');

  const [isToggled, setIsToggled] = React.useState(false);

  const [isRefreshing, setIsRefreshing] = React.useState(false);

  React.useEffect(() => {
    const backPress = () => {
      navigation.goBack();

      onGoBack();

      return true;
    };

    DeviceEventEmitter.addListener('messageSent', (message: Message) => {
      setMessages(prev => {
        if (prev != undefined) {
          if (message.enrollmentDate != null) {
            const index = ArrayUtils.findLastIndex(
              prev,
              m => m.orderResponseId == message.orderResponseId);

            prev[index].isActive = false;
          }

          prev = [...prev, message];
        }
        return [...prev];
      });
    });

    DeviceEventEmitter.addListener('read', (id: string) => {
      setMessages(prev => {
        if (prev != null) {
          let index = prev.findIndex(m => String(m.id) === String(id));

          if (index != -1) {
            prev[index].isRead = true;
          }
        }
        return [...prev];
      });
    });

    BackHandler.addEventListener('hardwareBackPress', backPress);

    return () => {
      DeviceEventEmitter.removeAllListeners('messageSent');
      DeviceEventEmitter.removeAllListeners('read');
    };
  }, []);

  React.useEffect(() => {
    const getUser = async () => {
      const user = await userService.getUser();

      if (user.id != undefined) {
        setUserId(user.id);
      }
    };
    const getChat = async () => {
      const response = await chatService.getChat(id);

      if (response.content != null) {
        setChatUser(response.content.user);
        setMessages(response.content.messages);
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
      chatService.getStatus(chatUser!.id).then(r => {
        if (r.content != null) {
          setStatus(r.content);
        }
      });
    };
    if (chatUser != null) {
      const timeout = count == 0 ? 0 : 10000;

      getStatus();

      setTimeout(async () => {
        setCount(prev => prev + 1);
      }, timeout);
    }
  }, [count, chatUser]);

  const createMessage = React.useCallback(async () => {
    if (chatUser != null) {
      const response = await chatService.create(
        chatUser.id,
        message,
        'Text');

      if (response.content != null) {
        setMessages(prev => [...prev, response.content!]);
      }
    }
  }, [message, chatUser]);

  const sendMessage = React.useCallback(async () => {
    await createMessage();
    setMessage('');
  }, [createMessage]);

  const launchLibrary = React.useCallback(async () => {
    if (chatUser != null) {
      setIsRefreshing(true);

      const imagePickerResponse = await launchImageLibrary({mediaType: 'photo'});

      if (imagePickerResponse.assets != undefined && imagePickerResponse.assets[0].uri != undefined) {
        const response = await chatService.createImage(chatUser.id, imagePickerResponse.assets[0].uri);

        if (response && response.content != null) {
          setMessages(prev => [...prev, response.content!]);
        }

        setIsRefreshing(false);
      }
    }
  }, [chatUser]);

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
              setMessages(prev => {
                let index = prev.findIndex(m => m.id == info.changed[i].item.id);

                if (index != undefined && index != -1) {
                  prev[index].isRead = true;
                }

                return [...prev];
              });
            }
          });
        }
      }
    },
    [messages],
  );

  const enrollmentDateChanged = React.useCallback((index: number, enrollmentDate: Date) => {
    setMessages(prev => {
      if (prev != null) {
        prev.push({
          ...prev[index],
          id: 'unique-id',
          enrollmentDate,
          fromUserId: userId});

        prev[index].isActive = false;
      }

      return [...prev];
    });
  }, [messages]);

  const Stub = React.useMemo(() => (
    <View style={styles.stubContainer}>
      <Text style={styles.stubTitle}>Нет сообщений</Text>
      <Text style={styles.stubText}>
        Можете запросить у компании любую интересующую Вас информацию или создать заказ и дождаться ответов от компаний рядом с вами
      </Text>
      <View style={{paddingHorizontal: 40}}>
        <GestureStyledButton
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

  const ref = React.useRef<BottomSheet>(null);

  const open = React.useCallback((orderResponseId: string) => {
    setResponseId(orderResponseId);
    ref.current?.expand();
  }, [ref]);

  const close = React.useCallback(() => {
    ref.current?.close();
  }, [ref]);

  const toggleModal = React.useCallback(() => {
    setIsToggled(prev => !prev);
  }, []);

  return (
    <View style={styles.container}>
      {chatUser != null && categories.length > 0 && (
        <View style={styles.chatBackground}>
          <View style={styles.chat}>
            <FlatList
              data={messages}
              showsVerticalScrollIndicator={false}
              contentContainerStyle={{flex: messages.length > 0 ? undefined : 1}}
              renderItem={(item) =>
                <MessageItem
                  item={item}
                  userId={userId}
                  navigation={navigation}
                  onEnrollmentDateChanged={enrollmentDateChanged}
                  open={open}/>
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
                <Text style={styles.userName}>{chatUser.name}</Text>
                <Text style={styles.status}>{status ? 'В сети' : 'Не в сети'}</Text>
              </View>
              <Image
                style={styles.icon}
                source={{uri: `${process.env.MINIO_URL}/app-files/${chatUser.iconUri}`}} />
            </View>
          </View>
          <View style={styles.bottomTab}>
            <View style={[styles.horizontalSpread, {paddingTop: 5}]}>
              <Pressable
                style={styles.alignToCenterContainer}
                onPress={launchLibrary}>
                <Icon
                  type={'material'}
                  name={'attachment'}
                  color={'#858E99'}
                  size={25}/>
              </Pressable>
              <View style={styles.textInputBorder}>
                <TextInput
                  value={message}
                  onChangeText={setMessage}
                  multiline={false}
                  maxLength={50}
                  style={styles.textInput}
                  placeholder={'Сообщение'}/>
              </View>
              <Pressable
                  style={styles.alignToCenterContainer}
                  onPress={sendMessage}
                  disabled={message.length == 0}>
                <Icon
                  type={'material'}
                  name={'send'}
                  color={'#858E99'}
                  size={25}/>
              </Pressable>
            </View>
          </View>
        </View>
      )}
      <LongRunningOperationIndicator isRefreshing={chatUser == null || categories.length == 0 || isRefreshing} />
      <ReviewBottomSheet
        ref={ref}
        toUserId={chatUser == null ? '' : chatUser.id}
        responseId={responseId}
        close={close}
        toggleModal={toggleModal}/>
      <SuccessfulRequestModal
        isToggled={isToggled}
        handlePress={toggleModal}
        title={'Отзыва оставлен'}
        text={''}/>
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
