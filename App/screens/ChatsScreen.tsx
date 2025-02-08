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

export default function ChatsScreen({navigation}: {navigation: any}) {
  const chatService = useDependency<ChatService>('ChatService');
  const userService = useDependency<UserService>('UserService');

  const [chats, setChats] = React.useState<Chat[]>([]);
  const [userId, setUserId] = React.useState('');

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
        let chatIndex = chats.findIndex(c => String(c.lastMessageId) === String(messageId));

        if (chatIndex != -1) {
          prev[chatIndex].lastMessageIsRead = true;
        }

        return prev;
      });
    });

    return () => {
      DeviceEventEmitter.removeAllListeners('messageSent');
      DeviceEventEmitter.removeAllListeners('read');
    };
  }, []);

  React.useEffect(() => {
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
    getUserId();
    getChats();
  }, []);

  const navigateToChat = React.useCallback((id: string) => {
    navigation.navigate('Chat', {id});
  }, []);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Чаты</Text>
      <FlatList
        data={chats}
        contentContainerStyle={{flex: 1}}
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
    fontWeight: '700',
    fontSize: 21,
    color: 'black',
    alignSelf: 'center',
    paddingTop: 20,
  },
});
