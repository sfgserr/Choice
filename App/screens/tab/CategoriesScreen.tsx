import * as React from 'react';
import {FlatList, RefreshControl, Text, View} from 'react-native';
import CategoryItem from '../../components/CategoryItem.tsx';
import {Category} from '../../types/DomainTypes.ts';
import {CategoriesScreenProps} from '../../types/NavigationTypes.ts';
import { AuthContext } from '../../App.tsx';

export default function CategoriesScreen({route, navigation}: CategoriesScreenProps) {
  const [categories, setCategories] = React.useState<Category[]>([]);
  const [refreshing, setRefreshing] = React.useState(false);
  const { changeState } = React.useContext(AuthContext);

  const onRefresh = React.useCallback(async () => {
    setRefreshing(true);
    const categories = await route.params.categoryService.getCategories(changeState);

    if (categories != null)
      setCategories(categories);
    else
      setCategories([]);

    setRefreshing(false);
  }, []);

  React.useEffect(() => {
    async function getCategories() {
     let c = await route.params.categoryService.getCategories(changeState);

     if (c != null)
      setCategories(c);
   }
   getCategories();
  }, []);

  return (
    <View
      style={{
        flexDirection: 'column',
        paddingTop: 20,
        flex: 1,
        backgroundColor: 'white'
      }}>
      <Text
        style={{
          fontWeight: '600',
          fontSize: 21,
          color: 'black',
          alignSelf: 'center'
        }}>
        Услуги
      </Text>
      <FlatList
        data={categories}
        refreshControl={
          <RefreshControl
            refreshing={refreshing}
            onRefresh={onRefresh}/>
        }
        style={{
          paddingTop: 20
        }}
        renderItem={(item) => {
          return (
            <View>
              <CategoryItem
                category={item.item}
                navigation={navigation}/>
            </View>
          )
        }}/>
    </View>
  )
}
