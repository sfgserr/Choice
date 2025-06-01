import * as React from 'react';
import {
  FlatList,
  StyleSheet,
  Text,
  View,
  DeviceEventEmitter,
} from 'react-native';
import {Chat, Message} from '../types/DomainTypes.ts';
import {useDependency} from '../services/Hooks.ts';
import {ChatService} from '../services/domain/ChatService.ts';
import {UserService} from '../services/domain/UserService.ts';
import ChatItem from '../components/listItems/ChatItem.tsx';
import {useSafeAreaInsets} from 'react-native-safe-area-context';

export default function ChatsScreen({navigation}: {navigation: any}) {
  const chatService = useDependency<ChatService>('ChatService');
  const userService = useDependency<UserService>('UserService');

  const [chats, setChats] = React.useState<Chat[]>([]);
  const [userId, setUserId] = React.useState('');

  //not good solution but react native navigation renders screen only once
  const [count, setCount] = React.useState(0);
  const [refreshing, setRefreshing] = React.useState(false);

  const insets = useSafeAreaInsets();

  React.useEffect(() => {
    DeviceEventEmitter.addListener('messageSent', (message: Message) => {
      setChats(prev => {
        let chatIndex = prev.findIndex(c => String(message.fromUserId) === String(c.userId));

        if (chatIndex != -1) {
          return prev.map((chat, index) =>
            index === chatIndex
              ? { ...chat,
                lastMessage: message.body,
                lastMessageIsRead: false,
                lastMessageId: message.id,
                lastMessageUserSenderId: message.fromUserId,
                lastMessageCreationDate: message.creationDate,
              }
              : chat
          );
        }

        return prev;
      });
    });

    DeviceEventEmitter.addListener('read', (messageId: string) => {
      setChats(prev => {
        let chatIndex = prev.findIndex(c => String(messageId) === String(c.lastMessageId));

        if (chatIndex != -1) {
          return prev.map((chat, index) =>
            index === chatIndex
              ? { ...chat,
                lastMessageIsRead: true,
              }
              : chat
          );
        }

        return prev;
      });
    });
  }, [count]);

  React.useEffect(() => {
    onRefresh();
  }, [count]);

  const onRefresh = React.useCallback(async () => {
    const getUserId = async () => {
      const user = await userService.getUser();

      if (user.id != undefined) {
        setUserId(user.id);
      }
    };
    const getChats = async () => {
      const response = await chatService.getChats();

      if (response.content != null) {
        setChats(response.content);
      }
    };

    setRefreshing(true);

    await getUserId();
    await getChats();

    setRefreshing(false);
  }, []);

  const onGoBack = React.useCallback(() => {
    setCount(prev => prev + 1);
  }, []);

  const navigateToChat = React.useCallback((id: string) => {
    const removeListeners = () => {
      DeviceEventEmitter.removeAllListeners('messageSent');
      DeviceEventEmitter.removeAllListeners('read');
    };

    navigation.navigate('Chat', {id, onGoBack});
    removeListeners();
  }, []);

  return (
    <View style={[styles.container, { paddingTop: insets.top }]}>
      <Text style={styles.title}>Чаты</Text>
      <FlatList
        data={chats}
        contentContainerStyle={{flex: 1}}
        onRefresh={onRefresh}
        refreshing={refreshing}
        renderItem={item => (
          <ChatItem
            userId={userId}
            navigateToChat={navigateToChat}
            item={item}/>
        )}
        style={{paddingTop: 10}}/>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  title: {
    fontWeight: '600',
    fontSize: 21,
    color: 'black',
    alignSelf: 'center',
    paddingTop: 20,
  },
});
