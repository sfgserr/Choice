import {ActivityIndicator, Alert, FlatList, StyleSheet, View} from 'react-native';
import React, {useEffect, useMemo} from 'react';
import {useDependency} from '../services/Hooks.ts';
import {AdminService} from '../services/domain/AdminService.ts';
import {AdminUser} from '../types/DomainTypes.ts';
import AdminUserItem from '../components/listItems/AdminUserItem.tsx';

export default function ClientsScreen() {
  const adminService = useDependency<AdminService>('AdminService');

  const [clients, setClients] = React.useState<AdminUser[]>([]);

  useEffect(() => {
    const getClients = async () => {
      const response = await adminService.getClients();

      if (response.result == 'successful') {
        setClients(response.content!);
        return;
      }

      Alert.alert('Ошибка', 'Не удалось получить клиентов', [{info: 'Ок'}]);
    };

    getClients();
  }, []);

  return (
    <View style={styles.container}>
      {clients.length == 0 ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'}/>
      ) : (
        <FlatList
          data={clients}
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
