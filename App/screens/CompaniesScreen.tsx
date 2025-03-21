import {ActivityIndicator, Alert, FlatList, StyleSheet, View} from 'react-native';
import React, {useEffect, useMemo} from 'react';
import {useDependency} from '../services/Hooks.ts';
import {AdminService} from '../services/domain/AdminService.ts';
import {AdminUser} from '../types/DomainTypes.ts';
import AdminUserItem from '../components/listItems/AdminUserItem.tsx';

export default function CompaniesScreen() {
  const adminService = useDependency<AdminService>('AdminService');

  const [companies, setCompanies] = React.useState<AdminUser[]>([]);

  useEffect(() => {
    const getCompanies = async () => {
      const response = await adminService.getCompanies();

      if (response.result == 'successful') {
        setCompanies(response.content!);
        return;
      }

      Alert.alert('Ошибка', 'Ну удалось получить компании', [{info: 'Ок'}]);
    };

    getCompanies();
  }, []);

  return (
    <View style={styles.container}>
      {companies.length == 0 ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'}/>
      ) : (
        <FlatList
          data={companies}
          style={{paddingTop: 15}}
          renderItem={item => (
            <AdminUserItem item={item.item}/>
          )}/>
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
});
