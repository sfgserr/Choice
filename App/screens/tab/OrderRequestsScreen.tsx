import * as React from 'react';
import {FlatList, RefreshControl, StyleSheet, Text, View} from 'react-native';
import {AuthContext} from '../../App.tsx';
import {OrderRequestsScreenProps} from '../../types/NavigationTypes.ts';
import {OrderRequest} from '../../types/DomainTypes.ts';
import OrderRequestItem from '../../components/listItems/OrderRequestItem.tsx';

export default function OrderRequestsScreen({route, navigation}: OrderRequestsScreenProps) {
  const { changeState } = React.useContext(AuthContext);
  const [orderRequests, setOrderRequests] = React.useState<OrderRequest[]>([]);
  const [refreshing, setRefreshing] = React.useState(false);

  const onRefresh = React.useCallback(async () => {
    setRefreshing(true);
    const orderRequests = await route.params.orderRequestService.getOrderRequests(changeState);

    if (orderRequests.content != null)
      setOrderRequests(orderRequests.content);
    else
      setOrderRequests([]);

    setRefreshing(false);
  }, []);

  React.useEffect(() => {
    async function getOrderRequests() {
      let response = await route.params.orderRequestService
        .getOrderRequests(changeState);

      if (response.result == 'successful' && response.content != null) {
        setOrderRequests(response.content);
      }
    }
    getOrderRequests();
  }, []);

  return (
    <View style={styles.container}>
      <View style={styles.titleContainer}>
        <Text style={styles.title}>Заказы</Text>
      </View>
      <FlatList
        data={orderRequests}
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }
        renderItem={item => {
          return (
            <View style={styles.itemContainer}>
              <OrderRequestItem orderRequest={item.item}/>
            </View>
          )
        }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  titleContainer: {
    alignItems: 'baseline'
  },
  title: {
    fontWeight: '600',
    fontSize: 21,
    color: 'black',
    paddingTop: 10,
    paddingBottom: 10,
    alignSelf: 'center'
  },
  itemContainer: {
    paddingTop: 5,
    paddingBottom: 5
  }
});
