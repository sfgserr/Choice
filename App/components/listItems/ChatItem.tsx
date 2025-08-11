import {
  Dimensions,
  Image,
  ListRenderItemInfo,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from 'react-native';
import {Icon} from '@rneui/base';
import {
  Chat,
} from '../../types/DomainTypes.ts';
import * as React from 'react';

const d = Dimensions.get('screen');

const ChatItem = ({item, userId, unreadMessagesCount, navigateToChat}: {
  item: ListRenderItemInfo<Chat>
  userId: string
  unreadMessagesCount: number
  navigateToChat: (id: string) => void}) => {
  const isSender = item.item.lastMessageUserSenderId == userId;

  return (
    <TouchableOpacity
      style={{...styles.chatContainer}}
      onPress={() => navigateToChat(item.item.userId)}>
      <Image
        style={styles.icon}
        source={{
          uri: `${process.env.MINIO_URL}/app-files/${item.item.iconUri}`,
        }}
      />
      <View style={{flex: 1, paddingLeft: 10}}>
        <View style={styles.chatInfoContainer}>
          <Text style={styles.chatName}>{item.item.userName}</Text>
          <View style={{flexDirection: 'row', alignItems: 'center'}}>
            {isSender && (
              <Icon
                name={'done-all'}
                type={'material'}
                size={20}
                color={item.item.lastMessageIsRead ? '#21C004' : '#BBBBBB'}
              />
            )}
            <Text>{`${new Date(
              item.item.lastMessageCreationDate,
            ).getHours()}:${new Date(
              item.item.lastMessageCreationDate,
            ).getMinutes()}`}</Text>
          </View>
        </View>
        <View
          style={{
            flexDirection: 'row',
            justifyContent: 'space-between'
          }}>
          <Text style={styles.lastMessage}>{item.item.lastMessage ?? 'Заказ'}</Text>
          {unreadMessagesCount && unreadMessagesCount != 0 && (
            <View
              style={{
                borderRadius: 10,
                height: 20,
                width: 20,
                justifyContent: 'center',
                backgroundColor: '#2D81E0',
              }}>
              <Text
                style={{
                  fontSize: 13,
                  color: 'white',
                  alignSelf: 'center',
                }}>
                {unreadMessagesCount}
              </Text>
            </View>
          )}
        </View>
      </View>
    </TouchableOpacity>
  );
};

export default ChatItem;

const styles = StyleSheet.create({
  chatContainer: {
    flexDirection: 'row',
    paddingHorizontal: 15,
    paddingVertical: 5,
    borderBottomWidth: 1,
    borderColor: '#D7D8D9',
  },
  icon: {
    width: d.height * 0.073,
    height: d.height * 0.073,
    borderRadius: d.height * 0.0365,
    resizeMode: 'cover',
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
