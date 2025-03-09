import React from 'react';
import {DateUtils} from '../utils/DateUtils.ts';
import {OrderResponse} from '../types/DomainTypes.ts';
import {useDependency} from '../services/Hooks.ts';
import {OrderResponseService} from '../services/domain/OrderResponseService.ts';
import {ActivityIndicator, Image, StyleSheet, Text, View} from 'react-native';

type OrderResponseCardProps = {
  responseId: string;
};

export default function OrderResponseCard({responseId}: OrderResponseCardProps) {
  const orderResponseService = useDependency<OrderResponseService>('OrderResponseService');

  const [order, setOrder] = React.useState<OrderResponse>();

  const info = React.useMemo(() => {
    if (order != null) {
      return [
        {
          value: `${order.price} рублей`,
          title: 'Стоимость',
          icon: require('../assets/images/rub.png'),
          color: '#ADCBEB',
          crossOut: false,
          display: order.price > 0,
        },
        {
          value: DateUtils.secondsToDate(order.deadline),
          title: 'Время выполнения работы',
          icon: require('../assets/images/deadline.png'),
          color: '#ADCBEB',
          crossOut: false,
          display: order.deadline > 0,
        },
        {
          value: order.enrollmentDate != null ? DateUtils.formatDate(order.enrollmentDate as Date) : null,
          title:'Дата и время записи',
          icon: require('../assets/images/enrollment.png'),
          color: '#ADCBEB',
          crossOut: false,
          display: order.enrollmentDate != null,
        },
        {
          value: `${order.prepayment} рублей`,
          title: 'Предоплата',
          icon: require('../assets/images/prepayment.png'),
          color: '#ADCBEB',
          crossOut: false,
          display: order.prepayment > 0,
        },
      ];
    }

    return [];
  }, [order]);

  React.useEffect(() => {
    const getOrderResponse = async () => {
      const response = await orderResponseService.get(responseId);

      if (response.result == 'successful') {
        setOrder(response.content);
      }
    };

    getOrderResponse();
  }, []);

  return (
    <View style={styles.order}>
      <View style={{paddingVertical: 10}}>
        {order == null ? (
          <ActivityIndicator size="large" color={'#white'} />
        ) : (
          <View style={{flexDirection: 'row', justifyContent: 'space-between'}}>
            <Text style={styles.orderMessage}>Ответ компании на Ваш запрос</Text>
          </View>
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
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  order: {
    backgroundColor: '#2D81E0',
    borderRadius: 20,
    paddingHorizontal: 10,
  },
  orderMessage: {
    color: 'white',
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
    color: '#ADCBEB',
    paddingLeft: 5,
  },
  infoValue: {
    color: 'white',
    fontWeight: '500',
    fontSize: 14,
  },
});
