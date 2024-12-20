import * as React from 'react';
import {FlatList, Text, View} from 'react-native';

import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {ClientTabProps} from '../../types/NavigationTypes.ts';
import CategoryItem from '../../components/CategoryItem.tsx';
import {Category} from '../../types/DomainTypes.ts';

type Props = BottomTabScreenProps<ClientTabProps, 'Categories'>

export default function CategoriesScreen({route, navigation}: Props) {
  const [categories, setCategories] = React.useState<Category[]>([]);

  React.useEffect(() => {
   async function getCategories() {
     let c = await route.params.categoriesService.getCategories();
     setCategories(c);
   }
   getCategories();
  });

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
        style={{
          paddingTop: 20
        }}
        renderItem={(item) => {
          return (
            <View>
              <CategoryItem category={item.item}/>
            </View>
          )
        }}/>
    </View>
  )
}
