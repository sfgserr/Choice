import * as React from 'react';
import {FlatList, RefreshControl, StyleSheet, Text, View} from 'react-native';
import CategoryItem from '../../components/listItems/CategoryItem.tsx';
import {Category} from '../../types/DomainTypes.ts';
import {CategoriesScreenProps} from '../../types/NavigationTypes.ts';
import { AuthContext } from '../../AuthorizedContextProvider.tsx';
import {useDependency} from '../../stores/DependencyInjection.ts';
import {CategoryService} from '../../services/domain/CategoryService.ts';

export default function CategoriesScreen({route, navigation}: CategoriesScreenProps) {
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [categories, setCategories] = React.useState<Category[]>([]);
  const [refreshing, setRefreshing] = React.useState(false);

  const { changeState } = React.useContext(AuthContext);

  const onRefresh = React.useCallback(async () => {
    setRefreshing(true);
    const categories = await categoryService.getCategories(changeState);

    if (categories.content != null)
      setCategories(categories.content);
    else
      setCategories([]);

    setRefreshing(false);
  }, []);

  React.useEffect(() => {
    async function getCategories() {
     let c = await categoryService.getCategories(changeState);

     if (c.content != null)
      setCategories(c.content);
   }
   getCategories();
  }, []);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Услуги</Text>
      <FlatList
        data={categories}
        refreshControl={
          <RefreshControl
            refreshing={refreshing}
            onRefresh={onRefresh}/>
        }
        style={styles.flatList}
        renderItem={(item) => {
          return (
            <View>
              <CategoryItem
                categoryId={item.index}
                categories={categories}
                navigation={navigation}/>
            </View>
          )
        }}/>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'column',
    paddingTop: 20,
    flex: 1,
    backgroundColor: 'white'
  },
  title: {
    fontWeight: '600',
    fontSize: 21,
    color: 'black',
    alignSelf: 'center'
  },
  flatList: {
    paddingTop: 20
  },
});
