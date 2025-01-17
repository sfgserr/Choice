import * as React from 'react';
import {
  FlatList,
  Image,
  RefreshControl, ScrollView,
  StyleSheet,
  Text,
  View,
} from 'react-native';
import {AuthContext} from '../../App.tsx';
import {OrderRequestsScreenProps} from '../../types/NavigationTypes.ts';
import {Category, OrderRequest} from '../../types/DomainTypes.ts';
import OrderRequestItem from '../../components/listItems/OrderRequestItem.tsx';
import {StyledButton} from '../../components/buttons/StyledButton.tsx';

export default function OrderRequestsScreen({route, navigation}: OrderRequestsScreenProps) {
  const { changeState } = React.useContext(AuthContext);
  const [orderRequests, setOrderRequests] = React.useState<OrderRequest[]>([]);
  const [categories, setCategories] = React.useState<Category[]>([]);
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
    async function getCategories() {
      let response = await route.params.categoryService.getCategories(changeState);

      if (response.result == 'successful' && response.content != null) {
        setCategories(response.content);
      }
    }
    getOrderRequests();
    getCategories();
  }, []);

  return (
    <View style={styles.container}>
      <View style={styles.titleContainer}>
        <Text style={styles.title}>Заказы</Text>
      </View>
      {orderRequests.length > 0 ? (
        <>
          <FlatList
            data={orderRequests}
            refreshControl={
              <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
            }
            renderItem={item => {
              return (
                <View style={styles.itemContainer}>
                  <OrderRequestItem
                    orderRequest={item.item}
                    navigation={navigation}/>
                </View>
              )
            }}
          />
        </>) : (
          <>
            <ScrollView
              style={styles.stubContainer}
              refreshControl={
                <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
              }>
              <Image
                source={require('../../assets/images/sad.png')}
                style={styles.image}/>
              <Text style={styles.stubTitle}>
                Пока нет заказов
              </Text>
              <Text style={styles.text}>
                Давайте исправим это
              </Text>
              <View style={styles.buttonContainer}>
                <StyledButton
                  content={'Создать заказ'}
                  top={40}
                  bottom={0}
                  isDisabled={false}
                  pressed={() => navigation.navigate('CreateOrderRequest', {
                    categories,
                    categoryIndex: 0,
                    onGoBack: (orderRequest: OrderRequest) => {}
                  })}/>
              </View>
            </ScrollView>
          </>)}
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
  },
  stubContainer: {
    flex: 1
  },
  image: {
    width: 50,
    height: 50,
    resizeMode: 'contain',
    alignSelf: 'center'
  },
  stubTitle: {
    color: 'black',
    fontWeight: '700',
    fontSize: 24,
    alignSelf: 'center',
    paddingTop: 40
  },
  text: {
    alignSelf: 'center',
    fontWeight: '400',
    fontSize: 16,
    color: '#818C99',
    paddingTop: 20
  },
  buttonContainer: {
    paddingHorizontal: 50
  }
});
