import {FlatList, Image, RefreshControl, StyleSheet, Text, View} from 'react-native';
import {CompanyRequestsScreenProps} from '../../../types/NavigationTypes.ts';
import React from 'react';
import {Category, OrderRequestRadius} from '../../../types/DomainTypes.ts';
import OrderRequestRadiusItem from '../../../components/listItems/OrderRequestRadiusItem.tsx';
import {useDependency} from '../../../services/Hooks.ts';
import {OrderRequestService} from '../../../services/domain/OrderRequestService.ts';
import {CategoryService} from '../../../services/domain/CategoryService.ts';
import {GestureHandlerRootView} from 'react-native-gesture-handler';
import {useSafeAreaInsets} from "react-native-safe-area-context";
import {DeviceTokenService} from "../../../services/domain/DeviceTokenService.ts";

export default function CompanyRequestsScreen({route, navigation}: CompanyRequestsScreenProps) {
  const orderRequestService = useDependency<OrderRequestService>('OrderRequestService');
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [refreshing, setRefreshing] = React.useState(false);

  const [orderRequests, setOrderRequests] = React.useState<OrderRequestRadius[]>([]);
  const [categories, setCategories] = React.useState<Category[]>([]);

  const insets = useSafeAreaInsets();

  const onRefresh = React.useCallback(async () => {
    async function getOrderRequests() {
      let response = await orderRequestService.getOrderRequestsRadius();

      if (response.content != null) {
        setOrderRequests(response.content);
      }
    }
    async function getCategories() {
      let response = await categoryService.getCategories();

      if (response.content != null) {
        setCategories(response.content);
      }
    }
    setRefreshing(true);

    await getCategories();
    await getOrderRequests();

    setRefreshing(false);
  }, []);

  React.useEffect(() => {
    async function getOrderRequests() {
      let response = await orderRequestService.getOrderRequestsRadius();

      if (response.content != null) {
        setOrderRequests(response.content);
      }
    }
    async function getCategories() {
      let response = await categoryService.getCategories();

      if (response.content != null) {
        setCategories(response.content);
      }

      await DeviceTokenService.addDevice();
    }
    getCategories();
    getOrderRequests();
  }, []);

  const Stub = () => (
    <View style={styles.stubContainer}>
      <Image
        source={require('../../../assets/images/sad.png')}
        style={styles.image}/>
      <Text style={styles.stubTitle}>
        Рядом нет заказов
      </Text>
      <Text style={styles.text}>
        Дождитесь появления заказов
      </Text>
    </View>
  )

  return (
    <GestureHandlerRootView>
      <View style={[styles.container, {paddingTop: insets.top}]}>
        <View style={styles.contentContainer}>
          <Text style={styles.title}>Заказы</Text>
          <FlatList
            data={orderRequests}
            refreshControl={
              <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
            }
            ListEmptyComponent={Stub}
            contentContainerStyle={{
              flex: orderRequests.length > 0 ? undefined : 1,
            }}
            renderItem={item => (
              <View style={styles.itemContainer}>
                <OrderRequestRadiusItem
                  orderRequest={item.item}
                  categories={categories}
                  navigation={navigation}
                  preview={false}
                />
              </View>
            )}
            style={styles.flatList}
          />
        </View>
      </View>
    </GestureHandlerRootView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white'
  },
  contentContainer: {
    flex: 1
  },
  title: {
    alignSelf: 'center',
    fontWeight: '600',
    fontSize: 21,
    color: 'black',
    paddingTop: 20
  },
  itemContainer: {
    paddingBottom: 10,
    paddingHorizontal: 10,
  },
  flatList: {
    paddingTop: 20
  },
  stubContainer: {
    flex: 1,
    justifyContent: 'center'
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
