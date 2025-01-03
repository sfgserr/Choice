import {Dimensions, StyleSheet, View} from 'react-native';
import OrderRequestCard from '../OrderRequestCard.tsx';
import {OrderRequestCardProps} from '../../types/ComponentTypes.ts';

const d = Dimensions.get('screen');

export default function OrderRequestItem({orderRequest, navigation}: OrderRequestCardProps) {
  return (
    <View style={styles.container}>
      <OrderRequestCard
        orderRequest={orderRequest}
        navigation={navigation}/>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    width: d.width * 0.9,
    height: 'auto',
    backgroundColor: 'white',
    borderRadius: 18,
    alignSelf: 'center',
    shadowColor: 'black',
    shadowOffset: {
      width: 10,
      height: 10
    },
    shadowOpacity: 1,
    elevation: 5
  }
});
