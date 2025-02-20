import {
  ActivityIndicator,
  Dimensions,
  Image,
  ListRenderItemInfo,
  StyleSheet,
  Text, TouchableOpacity,
  View,
} from 'react-native';
import {Icon} from '@rneui/base';
import React from 'react';
import {Message, OrderResponse} from '../../types/DomainTypes.ts';
import {Order} from 'aws-sdk/clients/glue';
import {useDependency} from '../../services/Hooks.ts';
import {OrderResponseService} from '../../services/domain/OrderResponseService.ts';
import {DateUtils} from '../../utils/DateUtils.ts';
import {StyledButton} from '../buttons/StyledButton.tsx';
import Styles from '../../constants/Styles.tsx';
import RNDateTimePicker from '@react-native-community/datetimepicker';

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
        <TouchableOpacity onPress={() => {navigation.navigate('ImageView', {uri: message.body});}}>
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

const OrderMessage = ({message, isSender, userId, index, onEnrollmentDateChanged}: {
  message: Message,
  isSender: boolean,
  userId: string,
  index: number,
  onEnrollmentDateChanged: (index: number) => void}) => {
  const orderResponseService = useDependency<OrderResponseService>('OrderResponseService');

  const [order, setOrder] = React.useState<OrderResponse | null>(null);
  const [isClient, setIsClient] = React.useState(false);

  const [showDateTimePicker, setShowDateTimePicker] = React.useState(false);

  const info = React.useMemo(() => {
    if (order != null) {
      return [
        {
          value: `${order.price} рублей`,
          title: 'Стоимость',
          icon: require('../../assets/images/rub.png'),
          color: 'black',
          crossOut: false,
          display: order.price > 0,
        },
        {
          value: DateUtils.secondsToDate(order.deadline),
          title: 'Время выполнения работы',
          icon: require('../../assets/images/deadline.png'),
          color: 'black',
          crossOut: false,
          display: order.deadline > 0,
        },
        {
          value: message.enrollmentDate != null ? DateUtils.formatDate(message.enrollmentDate as Date) : null,
          title: 'Дата и время записи',
          icon: require('../../assets/images/enrollment.png'),
          color: 'black',
          crossOut: true,
          display: message.enrollmentDate != null,
        },
        {
          value: order.enrollmentDate != null ? DateUtils.formatDate(order.enrollmentDate as Date) : null,
          title: message.enrollmentDate != null ? 'Новая время записи' : 'Дата и время записи',
          icon: require('../../assets/images/enrollment.png'),
          color: message.enrollmentDate != null ? '#FF4545' : 'black',
          crossOut: false,
          display: order.enrollmentDate != null,
        },
        {
          value: `${order.prepayment} рублей`,
          title: 'Предоплата',
          icon: require('../../assets/images/prepayment.png'),
          color: 'black',
          crossOut: false,
          display: order.prepayment > 0,
        },
      ];
    }

    return [];
  }, [order]);

  React.useEffect(() => {
    const getOrderResponse = async () => {
      const response = await orderResponseService.get(message.orderResponseId!);

      if (response.result == 'successful') {
        setOrder(response.content);
        setIsClient(userId == response.content!.clientId);
      }
    };

    getOrderResponse();
  }, []);

  const changeEnrollmentDate = React.useCallback(async (date: Date) => {
    if (order != null) {
      const response = await orderResponseService.changeEnrollmentDate(order.id, date);

      if (response.result == 'successful') {
        onEnrollmentDateChanged(index);
      }
    }
  }, [order]);

  const displayChangeEnrollmentDate = React.useMemo(() => {
    return order != null && order.status == 'Active' && (userId != order.userChangedEnrollmentDate || !message.isActive || !order.isActive);
  }, [order]);

  const displayEnroll = React.useMemo(() => {
    return order != null && order.status == 'Active' && isClient && order.isEnrollmentDateConfirmed && order.isPaid && message.isActive && order.isActive;
  }, [order]);

  const displayWaitForConfirm = React.useMemo(() => {
    return order != null && order.status == 'Active' && isClient && !order.isEnrollmentDateConfirmed && message.isActive && order.isActive;
  }, [order]);

  const displayConfirm = React.useMemo(() => {
    return order != null && order.status == 'Active' && !isClient && !order.isEnrollmentDateConfirmed && message.isActive && order.isActive;
  }, [order]);

  return (
    <View style={styles.messageContainer}>
      <View style={styles.order}>
        <View style={{paddingVertical: 10}}>
          {order == null ? (
            <ActivityIndicator size="large" color={'#2D81E0'} />
          ) : (
            <Text style={styles.orderMessage}>
              {message.enrollmentDate == null
                ? isSender
                  ? 'Ваш ответ на заказ клиента'
                  : 'Ответ компании на Ваш запрос'
                : !isSender
                ? 'Вы предложили изменить время записи'
                : isClient
                ? 'Компания предлагает изменить время записи'
                : 'Клиент предлагает изменить время записи'}
            </Text>
          )}
          {info.map((i, key) => (
            <>
              {i.display && (
                <View style={{paddingTop: 10}} key={key}>
                  <View style={styles.orderInfoContainer}>
                    <View style={{flexDirection: 'row', alignItems: 'center'}}>
                      <Image
                        source={i.icon}
                        style={styles.infoIcon}
                        tintColor={i.color}
                      />
                      <Text style={[styles.infoTitle, {color: i.color}]}>
                        {i.title}
                      </Text>
                    </View>
                    <Text
                      style={[
                        styles.infoValue,
                        {color: i.color},
                      ]}>{`${i.value}`}</Text>
                    {i.crossOut && (
                      <View
                        style={{
                          position: 'absolute',
                          width: '100%',
                          height: 1,
                          backgroundColor: 'black',
                        }}
                      />
                    )}
                  </View>
                </View>
              )}
            </>
          ))}
          {displayChangeEnrollmentDate && (
            <StyledButton
              content={'Изменить время и дату записи'}
              top={10}
              bottom={0}
              isDisabled={!order?.isActive || !message.isActive}
              pressed={() => setShowDateTimePicker(true)}
              reversed={true}
            />
          )}
          {displayWaitForConfirm && (
            <StyledButton
              content={'Дождитесь ответа компании'}
              top={10}
              bottom={0}
              isDisabled={!order?.isActive || !message.isActive}
              pressed={() => {}}
              reversed={true}
            />
          )}
          {displayEnroll && (
            <StyledButton
              content={'Записаться и внести предоплату'}
              top={10}
              bottom={0}
              isDisabled={!order?.isActive || !message.isActive}
              pressed={() => {}}
            />
          )}
          {displayConfirm && (
            <StyledButton
              content={'Подтвердить запись'}
              top={10}
              bottom={0}
              isDisabled={!order?.isActive || !message.isActive}
              pressed={() => {}}
              reversed={true}
            />
          )}
        </View>
      </View>
      {showDateTimePicker && (
        <RNDateTimePicker
          mode={'datetime'}
          display={'spinner'}
          value={new Date()}
          minimumDate={new Date()}
          onChange={async (e, d) => {
            if (e.type == 'set' && d != undefined) {
              await changeEnrollmentDate(d);
            }

            setShowDateTimePicker(false);
          }}
        />
      )}
    </View>
  );
};

export default function MessageItem ({item, userId, navigation, onEnrollmentDateChanged}: {
  item: ListRenderItemInfo<Message>,
  userId: string,
  navigation: any,
  onEnrollmentDateChanged: (index: number) => void,}) {
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
        <OrderMessage
          message={item.item}
          isSender={isSender}
          userId={userId}
          index={item.index}
          onEnrollmentDateChanged={onEnrollmentDateChanged}/>
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
    maxWidth: d.width / 1.8,
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
  order: {
    backgroundColor: 'white',
    borderWidth: 1,
    borderColor: '#B5CADD',
    borderRadius: 20,
    paddingHorizontal: 10,
  },
  orderMessage: {
    color: 'black',
    fontWeight: '700',
    fontSize: 14,
  },
  orderInfoContainer: {
    flexDirection: 'row',
    paddingLeft: 5,
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  infoIcon: {
    width: 15,
    height: 15,
    resizeMode: 'contain',
  },
  infoTitle: {
    fontSize: 14,
    fontWeight: '400',
    color: '#2E2424',
    paddingLeft: 5,
  },
  infoValue: {
    color: 'black',
    fontWeight: '500',
    fontSize: 14,
  },
});
