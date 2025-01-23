import * as React from 'react';
import {
  FlatList,
  Image,
  RefreshControl,
  StyleSheet,
  Text,
  View,
} from 'react-native';
import {OrderRequestsScreenProps} from '../../types/NavigationTypes.ts';
import {Category, OrderRequest} from '../../types/DomainTypes.ts';
import OrderRequestItem from '../../components/listItems/OrderRequestItem.tsx';
import {StyledButton} from '../../components/buttons/StyledButton.tsx';
import {useDependency} from '../../services/Hooks.ts';
import {OrderRequestService} from '../../services/domain/OrderRequestService.ts';
import {CategoryService} from '../../services/domain/CategoryService.ts';

export default function OrderRequestsScreen({route, navigation}: OrderRequestsScreenProps) {
  const orderRequestService = useDependency<OrderRequestService>('OrderRequestService');
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [orderRequests, setOrderRequests] = React.useState<OrderRequest[]>([]);
  const [categories, setCategories] = React.useState<Category[]>([]);

  const [refreshing, setRefreshing] = React.useState(false);

  const getOrderRequests = React.useCallback(async () => {
    const orderRequests = await orderRequestService.getOrderRequests();

    if (orderRequests.content != null)
      setOrderRequests(orderRequests.content);
    else
      setOrderRequests([]);
  }, []);

  const onRefresh = React.useCallback(async () => {
    setRefreshing(true);

    await getOrderRequests();

    setRefreshing(false);
  }, []);

  React.useEffect(() => {
    const getCategories = async ()=> {
      let response = await categoryService.getCategories();

      if (response.result == 'successful' && response.content != null) {
        setCategories(response.content);
      }
    };
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
            <View
              style={styles.stubContainer}>
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
            </View>
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
    justifyContent: 'center',
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
