import {FlatList, Image, ScrollView, StyleSheet, Text, View} from 'react-native';
import {CompanyRequestsScreenProps} from '../../types/NavigationTypes.ts';
import React from 'react';
import {Category, OrderRequestRadius} from '../../types/DomainTypes.ts';
import {StyledButton} from '../../components/buttons/StyledButton.tsx';
import {AuthContext} from '../../App.tsx';
import OrderRequestRadiusItem from '../../components/listItems/OrderRequestRadiusItem.tsx';

export default function CompanyRequestsScreen({route, navigation}: CompanyRequestsScreenProps) {
  const { changeState } = React.useContext(AuthContext);

  const [refreshing, setRefreshing] = React.useState(false);

  const [orderRequests, setOrderRequests] = React.useState<OrderRequestRadius[]>([]);
  const [categories, setCategories] = React.useState<Category[]>([]);

  React.useEffect(() => {
    async function getOrderRequests() {
      let response = await route.params.orderRequestService.getOrderRequestsRadius(
        changeState);

      if (response.content != null) {
        setOrderRequests(response.content);
      }
    }
    async function getCategories() {
      let response = await route.params.categoryService.getCategories(changeState);

      if (response.content != null) {
        setCategories(response.content);
      }
    }
    getOrderRequests();
    getCategories();
  }, []);

  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'white'
      }}>
      {orderRequests.length > 0 ? (
        <>
          <View
            style={{flex: 1}}>
            <Text
              style={{
                alignSelf: 'center',
                fontWeight: '600',
                fontSize: 21,
                color: 'black',
                paddingTop: 20
              }}>
              Заказы
            </Text>
            <FlatList
              data={orderRequests}
              renderItem={(item) => (
                <View
                  style={{
                    paddingBottom: 10,
                    paddingHorizontal: 10
                  }}>
                  <OrderRequestRadiusItem
                    orderRequest={item.item}
                    categories={categories}
                    navigation={navigation}/>
                </View>
              )}
              style={{paddingTop: 20}}/>
          </View>
        </>) : (
        <>
          <View style={styles.stubContainer}>
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
                pressed={() => {}}/>
            </View>
          </View>
        </>)}
    </View>
  )
}

const styles = StyleSheet.create({
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
