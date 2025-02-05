import * as React from 'react';
import {
  FlatList,
  StyleSheet,
  Text,
  View,
  ListRenderItemInfo,
  Dimensions,
  TouchableOpacity,
  Image,
} from 'react-native';
import {Chat} from '../types/DomainTypes.ts';
import {useDependency} from '../services/Hooks.ts';
import {ChatService} from '../services/domain/ChatService.ts';
import {Icon} from '@rneui/base';
import {UserService} from '../services/domain/UserService.ts';

const d = Dimensions.get('screen');

export default function ChatsScreen({navigation}: {navigation: any}) {
  const chatService = useDependency<ChatService>('ChatService');
  const userService = useDependency<UserService>('UserService');

  const [chats, setChats] = React.useState<Chat[]>([]);
  const [userId, setUserId] = React.useState('');

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

  const Chat = (item: ListRenderItemInfo<Chat>) => {

    return (
      <TouchableOpacity
        style={styles.chatContainer}
        onPress={() => navigateToChat(item.item.userId)}>
        <Image
          style={styles.icon}
          source={{uri: `${process.env.MINIO_URL}/app-files/${item.item.iconUri}`}}/>
        <View style={{flex: 1, paddingLeft: 10}}>
          <View style={styles.chatInfoContainer}>
            <Text style={styles.chatName}>{item.item.userName}</Text>
            <View style={{flexDirection: 'row', alignItems: 'center'}}>
              <Icon
                name={'done-all'}
                type={'material'}
                size={20}
                color={item.item.lastMessageIsRead ? '#21C004' : '#BBBBBB'}/>
              <Text>{`${new Date(item.item.lastMessageCreationDate + 'Z').getHours()}:${new Date(item.item.lastMessageCreationDate + 'Z').getMinutes()}`}</Text>
            </View>
          </View>
          <Text style={styles.lastMessage}>{item.item.lastMessage}</Text>
        </View>
      </TouchableOpacity>
    );
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Чаты</Text>
      <FlatList
        data={chats}
        contentContainerStyle={{flex: 1}}
        renderItem={Chat}
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
  chatContainer: {
    flexDirection: 'row',
    paddingHorizontal: 15,
  },
  icon: {
    width: d.height * 0.073,
    height: d.height * 0.073,
    borderRadius: d.height * 0.0365,
    resizeMode: 'contain',
    alignSelf: 'center',
  },
  chatInfoContainer: {
    justifyContent: 'space-between',
    flexDirection: 'row',
    alignItems: 'center',
  },
  chatName: {
    color: 'black',
    fontSize: 16,
    fontWeight: '500',
  },
  lastMessage: {
    fontWeight: '400',
    fontSize: 15,
    color: '#8E8E93',
  },
});
