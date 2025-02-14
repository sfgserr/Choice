import {
  Dimensions,
  Image,
  ListRenderItemInfo,
  StyleSheet,
  Text, TouchableOpacity,
  View,
} from 'react-native';
import {Icon} from '@rneui/base';
import React from 'react';
import { Message } from '../../types/DomainTypes.ts';

const d = Dimensions.get('screen');

const TextMessage = ({message, isSender}: {message: Message, isSender: boolean}) => (
  <View style={[styles.messageContainer, {alignItems: isSender ? 'flex-end' : 'flex-start'}]}>
    <View style={isSender ? styles.senderMessageBox : styles.receiverMessageBox}>
      <Text
        style={[styles.messageText, {color: isSender ? 'white' : 'black'}]}>
        {message.body}
      </Text>
      <View style={styles.messageInfoContainer}>
        <Text
          style={[
            styles.messageCreationTime,
            {color: isSender ? 'white' : '#8E8E93'},
          ]}>
          {`${new Date(message.creationDate).getHours()}:${new Date(message.creationDate).getMinutes()}`}
        </Text>
        {isSender && (
          <Icon
            name={message.isRead ? 'done-all' : 'check'}
            type={'material'}
            color={'white'}
            size={15}/>
        )}
      </View>
    </View>
  </View>
);

const ImageMessage = ({message, isSender, navigation}: {
  message: Message,
  isSender: boolean,
  navigation: any}) => {
  const uri = `${process.env.MINIO_URL}/app-files/${message.body}`;

  const [imageSize, setImageSize] = React.useState<number>(0);

  return (
    <View style={[styles.messageContainer, {alignItems: isSender ? 'flex-end' : 'flex-start'}]}>
      <View style={isSender ? styles.senderImageMessageBox : styles.receiverImageMessageBox}>
        <TouchableOpacity onPress={() => {navigation.navigate('ImageView', {uri: message.body})}}>
          <Image
            style={styles.image}
            source={{uri}}/>
        </TouchableOpacity>
        <View
          style={{justifyContent: 'center', paddingLeft: 5}}>
          <Text style={[styles.imageName, {color: isSender ? 'white' : 'black'}]}>{message.body?.substring(0, 10)}</Text>
          <Text style={styles.imageSize}>{imageSize}</Text>
        </View>
        <View style={styles.messageImageInfoContainer}>
          <Text
            style={[
              styles.messageCreationTime,
              {color: isSender ? 'white' : '#8E8E93'},
            ]}>
            {`${new Date(message.creationDate).getHours()}:${new Date(message.creationDate).getMinutes()}`}
          </Text>
          {isSender && (
            <Icon
              name={message.isRead ? 'done-all' : 'check'}
              type={'material'}
              color={'white'}
              size={15}/>
          )}
        </View>
      </View>
    </View>
  );
};

export default function MessageItem ({item, userId, navigation}: {
  item: ListRenderItemInfo<Message>,
  userId: string,
  navigation: any}) {
  const isSender = userId === item.item.fromUserId;

  return (
    <>
      {item.item.type == 'Text' ? (
        <TextMessage
          message={item.item}
          isSender={isSender}/>
      ) : item.item.type == 'Image' ? (
        <ImageMessage
          message={item.item}
          isSender={isSender}
          navigation={navigation}/>
      ) : (
        <Text>asdasd</Text>
      )}
    </>
  );
}

const styles = StyleSheet.create({
  messageContainer: {
    paddingTop: 2.5,
    paddingBottom: 2.5,
    paddingHorizontal: 10,
  },
  senderMessageBox: {
    paddingVertical: 2,
    borderTopLeftRadius: 15,
    borderBottomLeftRadius: 15,
    borderTopRightRadius: 20,
    borderBottomRightRadius: 10,
    backgroundColor: '#2D81E0',
    paddingHorizontal: 10,
    flexDirection: 'row',
  },
  receiverMessageBox: {
    paddingVertical: 2,
    borderTopRightRadius: 15,
    borderBottomRightRadius: 15,
    borderTopLeftRadius: 20,
    borderBottomLeftRadius: 10,
    backgroundColor: 'white',
    paddingHorizontal: 10,
    flexDirection: 'row',
    borderWidth: 1,
    borderColor: '#B5CADD',
  },
  senderImageMessageBox: {
    paddingVertical: 10,
    borderRadius: 10,
    backgroundColor: '#2D81E0',
    paddingHorizontal: 10,
    flexDirection: 'row',
  },
  receiverImageMessageBox: {
    paddingVertical: 10,
    borderRadius: 10,
    backgroundColor: 'white',
    paddingHorizontal: 10,
    flexDirection: 'row',
    borderWidth: 1,
    borderColor: '#B5CADD',
  },
  messageText: {
    fontSize: 17,
    fontWeight: '400',
    alignSelf: 'center',
  },
  messageInfoContainer: {
    alignSelf: 'flex-end',
    paddingLeft: 10,
    flexDirection: 'row',
    alignItems: 'center',
  },
  messageImageInfoContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    position: 'absolute',
    bottom: 2,
    right: 2,
  },
  messageCreationTime: {
    fontWeight: '100',
    fontSize: 11,
  },
  image: {
    width: d.height * 0.091,
    height: d.height * 0.091,
    resizeMode: 'cover',
    borderRadius: 10,
  },
  imageName: {
    fontSize: 16,
    fontWeight: '400',
  },
  imageSize: {
    fontSize: 13,
    color: '#ddd',
    fontWeight: '400',
  },
});
