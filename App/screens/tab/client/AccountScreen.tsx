import * as React from 'react';
import {ActivityIndicator, Image, StyleSheet, Text, View} from 'react-native';
import {AuthContext} from '../../../contexts/authorized/Context.tsx';
import {StyledButton} from '../../../components/buttons/StyledButton.tsx';
import {AccountScreenProps} from '../../../types/NavigationTypes.ts';
import {useDependency} from '../../../services/Hooks.ts';
import {UserService} from '../../../services/domain/UserService.ts';
import {Client} from '../../../types/DomainTypes.ts';
import TextButton from "../../../components/buttons/TextButton.tsx";

export default function AccountScreen({route, navigation}: AccountScreenProps) {
  const userService = useDependency<UserService>('UserService');

  const { signOut } = React.useContext(AuthContext);

  const [user, setUser] = React.useState<Client | null>(null);

  React.useEffect(() => {
    const getUser = async () => {
      const user = await userService.getUserWithoutCache();

      if (user != null)
        setUser(user);
    };

    getUser();
  }, []);

  return (
    <View style={styles.container}>
      {user == null ? (
        <ActivityIndicator size={'large'} color={'#2D81E0'}/>
      ) : (
        <>
          <Text style={styles.title}>Аккаунт</Text>
          <View style={styles.iconContainer}>
            <Image
              style={styles.icon}
              source={{
                uri: `${process.env.MINIO_URL}/app-files/${user?.iconUri}`,
              }}/>
          </View>
          <View style={styles.textButtonContainer}>
            <TextButton text={'Изменить фото'} onPress={() => {}}/>
          </View>
        </>
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
  },
  title: {
    fontWeight: '700',
    fontSize: 22,
    alignSelf: 'center',
    paddingTop: 30,
  },
  iconContainer: {
    alignSelf: 'center',
    justifyContent: 'center',
    paddingTop: 20,
  },
  icon: {
    width: 80,
    height: 80,
    borderRadius: 40,
    resizeMode: 'contain',
    alignSelf: 'center',
  },
  textButtonContainer: {
    justifyContent: 'center',
    paddingTop: 10,
  },
});
