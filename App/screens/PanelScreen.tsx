import {Image, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import React, {useContext, useMemo} from 'react';
import {PanelScreenProps} from '../types/NavigationTypes.ts';
import ClientsScreen from './ClientsScreen.tsx';
import CompaniesScreen from './CompaniesScreen.tsx';
import AdminCategoriesScreen from './AdminCategoriesScreen.tsx';
import TabBar from '../components/TabBar.tsx';
import {AuthContext} from '../contexts/authorized/Context.tsx';

export default function PanelScreen({route, navigation}: PanelScreenProps) {
  const { signOut } = useContext(AuthContext);

  const tabs = useMemo(() => [
    {
      element: <ClientsScreen/>,
      title: 'Клиенты',
    },
    {
      element: <CompaniesScreen/>,
      title: 'Компании',
    },
    {
      element: <AdminCategoriesScreen navigation={navigation}/>,
      title: 'Категории',
    },
  ], []);

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Админ панель</Text>
      <TouchableOpacity
        style={styles.signOutButton}
        onPress={signOut}>
        <Image
          style={styles.icon}
          source={require('../assets/images/signout.png')}/>
      </TouchableOpacity>
      <TabBar tabs={tabs} big={false}/>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  title: {
    fontSize: 21,
    fontWeight: '700',
    color: 'black',
    alignSelf: 'center',
    paddingTop: 30,
  },
  signOutButton: {
    position: 'absolute',
    top: 30,
    right: 20,
  },
  icon: {
    width: 30,
    height: 30,
    resizeMode: 'contain',
  },
});
