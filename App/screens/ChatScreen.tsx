import {
  Dimensions,
  FlatList,
  Image,
  ListRenderItemInfo,
  StyleSheet,
  Text,
  TextInput, TouchableOpacity,
  View,
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

const d = Dimensions.get('screen');

export default function ChatScreen({id, navigation}: {id: string, navigation: any}) {
  const chatService = useDependency<ChatService>('ChatService');
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [chat, setChat] = React.useState<ChatMessages | null>(null);
  const [status, setStatus] = React.useState<boolean>(false);
  const [categories, setCategories] = React.useState<Category[]>([]);

  const [count, setCount] = React.useState(0);

  const [message, setMessage] = React.useState('');

  React.useEffect(() => {
    const getChat = async () => {
      const response = await chatService.getChat(id);

      if (response.content != null) {
        setChat(response.content);
      }
    }
    const getCategories = async () => {
      const response = await categoryService.getCategories();

      if (response.content != null) {
        setCategories(response.content);
      }
    }
    getChat();
    getCategories();
  }, []);

  React.useEffect(() => {
    const getStatus = async () => {
      if (chat != null) {
        const response = await chatService.getStatus(chat.user.id);

        if (response.content != null) {
          setStatus(response.content);
        }
      }
    };
    const timeout = count == 1000 ? 0 : 17000;

    const timer = setInterval(() => setCount(prev => prev+1), timeout);

    getStatus();

    return () => clearInterval(timer);
  }, [count]);

  const sendMessage = React.useCallback(async () => {
    if (chat != null) {
      const response = await chatService.create(
        chat.user.id,
        message,
        'Text');

      if (response.result == 'successful') {
      }
    }
  }, []);

  const Stub = () => (
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
  );

  const Message = (item: ListRenderItemInfo<Message>) => (
    <View>

    </View>
  )

  return (
    <View style={styles.container}>
      {chat != null && categories.length > 0 && (
        <View style={styles.chatBackground}>
          <View style={styles.chat}>
            <FlatList
              data={chat.messages}
              contentContainerStyle={{flex:1}}
              renderItem={Message}
              ListEmptyComponent={Stub}/>
          </View>
          <View style={styles.userTab}>
            <View style={[styles.horizontalSpread, {paddingBottom: 5}]}>
              <View style={styles.alignToCenterContainer}>
                <NavigateBackButton navigation={navigation} />
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
              <TouchableOpacity style={styles.alignToCenterContainer}>
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
                  style={styles.textInput}
                  placeholder={'Сообщение'}/>
              </View>
              <TouchableOpacity style={styles.alignToCenterContainer}>
                <Icon
                  type={'material'}
                  name={'mic'}
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
  },
  stubContainer: {
    flex: 1,
    justifyContent: 'center'
  },
  stubTitle: {
    fontSize: 24,
    fontWeight: '700',
    color: 'black',
    alignSelf: 'center'
  },
  stubText: {
    color: '#818C99',
    fontWeight: '400',
    fontSize: 16,
    paddingTop: 20,
    alignSelf: 'center',
    textAlign: 'center'
  },
  messageContainer: {
    paddingTop: 10
  },
  status: {
    color: '#787878',
    fontWeight: '400',
    fontSize: 13,
    alignSelf: 'center'
  }
});
