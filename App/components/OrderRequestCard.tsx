import {Image, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import Styles from '../constants/Styles.tsx';
import {OrderRequestCardProps} from '../types/ComponentTypes.ts';
import {DateUtils} from '../utils/DateUtils.ts';

export default function OrderRequestCard({orderRequest}: OrderRequestCardProps) {
  return (
    <View style={styles.container}>
      <View style={styles.categoryTitleContainer}>
        <View>
          <Text style={styles.id}>{orderRequest.id}</Text>
          <Text style={styles.categoryTitle}>{orderRequest.categoryTitle}</Text>
        </View>
        <View style={[
          styles.statusContainer,
          orderRequest.orderStatus == 'Active' ?
            styles.activeColor : orderRequest.orderStatus == 'Canceled' ?
              styles.canceledColor : styles.finishedColor
        ]}>
          <Text style={styles.status}>
            {orderRequest.orderStatus == 'Active' ?
              'Активен' : orderRequest.orderStatus == 'Canceled' ?
                'Отменен' : 'Завершен'}
          </Text>
        </View>
      </View>
      <Text style={styles.description} numberOfLines={3}>
        {orderRequest.description}
      </Text>
      <View style={styles.dateTimeContainer}>
        <Image
          style={styles.image}
          source={require('../assets/images/calendar.png')}
        />
        <Text style={styles.creationDate}>{DateUtils.formatDate(orderRequest.creationDate)}</Text>
      </View>
      <View style={styles.detailsButtonContainer}>
        <TouchableOpacity style={[Styles.styledButton, styles.detailsButton]}>
          <Text style={styles.detailsButtonContent}>Подробнее</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    paddingHorizontal: 10
  },
  categoryTitleContainer: {
    flexDirection: 'row',
    paddingTop: 10,
    justifyContent: 'space-between'
  },
  id: {
    fontSize: 9,
    color: '#8E8E93',
    fontWeight: '400',
  },
  categoryTitle: {
    color: 'black',
    fontWeight: '600',
    fontSize: 14,
  },
  activeColor: {
    backgroundColor: '#6DC876'
  },
  finishedColor: {
    backgroundColor: '#2D81E0'
  },
  canceledColor: {
    backgroundColor: '#AEAEB2'
  },
  statusContainer: {
    borderRadius: 10,
    paddingHorizontal: 10,
    paddingVertical: 5,
    justifyContent: 'center'
  },
  description: {
    paddingTop: 10,
    paddingBottom: 10,
    fontSize: 15,
    fontWeight: '400',
    color: '#313131',
  },
  dateTimeContainer: {
    flexDirection: 'row',
    paddingBottom: 10,
  },
  image: {
    width: 18,
    height: 18,
    alignSelf: 'center',
  },
  creationDate: {
    color: '#313131',
    fontWeight: '500',
    fontSize: 15,
    alignSelf: 'center',
    paddingLeft: 10,
  },
  status: {
    fontWeight: '500',
    fontSize: 14,
    color: 'white',
    alignSelf: 'center'
  },
  detailsButtonContainer: {
    paddingBottom: 5
  },
  detailsButton: {
    backgroundColor: '#f2f4f5',
    justifyContent: 'center'
  },
  detailsButtonContent: {
    fontSize: 17,
    fontWeight: '500',
    color: '#2688EB',
    alignSelf: 'center'
  }
});
