import * as React from 'react';
import {FlatList, RefreshControl, StyleSheet, Text, View} from 'react-native';
import CategoryItem from '../components/listItems/CategoryItem.tsx';
import {Category} from '../types/DomainTypes.ts';
import {useDependency} from '../services/Hooks.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {StyledButton} from '../components/buttons/StyledButton.tsx';

export default function AdminCategoriesScreen({navigation}: {navigation: any}) {
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [categories, setCategories] = React.useState<Category[]>([]);
  const [refreshing, setRefreshing] = React.useState(false);

  const onRefresh = React.useCallback(async () => {
    setRefreshing(true);
    const categories = await categoryService.getCategories();

    if (categories.content != null)
      setCategories(categories.content);
    else
      setCategories([]);

    setRefreshing(false);
  }, []);

  React.useEffect(() => {
    async function getCategories() {
      let c = await categoryService.getCategories();

      if (c.content != null)
        setCategories(c.content);
    }
    getCategories();
  }, []);

  return (
    <View style={styles.container}>
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
                onPress={() => {
                  navigation.navigate('EditCategory', {
                    category: item.item,
                  });
                }}/>
            </View>
          )
        }}/>
      <View style={styles.buttonContainer}>
        <StyledButton
          content={'Добавить категорию'}
          top={0}
          bottom={0}
          isDisabled={false}
          pressed={() => navigation.navigate('CreateCategory')}
          type={'default'}/>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flexDirection: 'column',
    flex: 1,
    backgroundColor: 'white',
  },
  title: {
    fontWeight: '600',
    fontSize: 21,
    color: 'black',
    alignSelf: 'center',
  },
  flatList: {
    paddingTop: 20,
  },
  buttonContainer: {
    position: 'absolute',
    bottom: 10,
    alignSelf: 'center',
    width: '90%',
  },
});
