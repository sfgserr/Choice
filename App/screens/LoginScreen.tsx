import * as React from 'react';
import {
  View,
  Dimensions,
  Image,
  Text,
  StyleSheet, KeyboardAvoidingView,
} from 'react-native';
import TextButton from '../components/buttons/TextButton.tsx';
import LoginByEmailScreen from './LoginByEmailScreen.tsx';
import TabBar from '../components/TabBar.tsx';
import {LoginScreenProps} from '../types/NavigationTypes.ts';
import CreateAccountModal from '../components/modals/CreateAccountModal.tsx';
import LoginByPhoneScreen from './LoginByPhoneScreen.tsx';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {SafeAreaView} from 'react-native-safe-area-context';
import KeyboardAvoidingScrollView from '../components/KeyboardAvoidingScrollView.tsx';
import {useIsFocused} from '@react-navigation/native';

const {height} = Dimensions.get('screen');

export default function LoginScreen({route, navigation}: LoginScreenProps) {
  const focused = useIsFocused();

  const [isToggled, setIsToggled] = React.useState(false);
  const [refreshing, setRefreshing] = React.useState(false);

  const refresh = React.useCallback(() => setRefreshing(prev => !prev), []);

  const tabs = [
    {element: <LoginByEmailScreen refresh={refresh}/>, title: 'E-mail'},
    {element: <LoginByPhoneScreen/>, title: 'Телефон'}
  ];

  return (
    <SafeAreaView
      style={styles.container}>
      <KeyboardAvoidingScrollView
        scrollable
        tabs={false}
        focused={focused}>
        <Image
          source={require('../assets/images/logo.png')}
          style={styles.logo}/>
        <Text style={styles.title}>ВЫБОР</Text>
        <Text style={styles.subTitle}>{'Приложение для выбора\nлучших условий'}</Text>
        <View style={styles.horizontalSpread}>
          <TextButton
            text={'Создать аккаунт'}
            onPress={() => setIsToggled(prev => !prev)}/>
          <Text style={styles.weightedText}>Авторизация</Text>
        </View>
        <TabBar tabs={tabs} big/>
      </KeyboardAvoidingScrollView>
      <CreateAccountModal
        isToggled={isToggled}
        handlePress={() => setIsToggled(prev => !prev)}
        navigation={navigation}/>
      <LongRunningOperationIndicator isRefreshing={refreshing}/>
    </SafeAreaView>
  )
}

const styles = StyleSheet.create({
  container: {
    backgroundColor: 'white',
    flex: 1,
    flexDirection: 'column',
  },
  logo: {
    width: height/7.38,
    height: height/7.38,
    alignSelf: 'center',
    marginTop: 60
  },
  title: {
    color: '#313131',
    fontSize: 20,
    fontWeight: '600',
    alignSelf: 'center',
    letterSpacing: 3,
    marginTop: 50
  },
  subTitle: {
    color: '#9C9C9C',
    fontWeight: '400',
    fontSize: 16,
    alignSelf: 'center',
    textAlign: 'center',
    marginTop: 10
  },
  horizontalSpread: {
    flexDirection: 'column',
    paddingTop: 30
  },
  weightedText: {
    color: '#313131',
    fontWeight: '700',
    fontSize: 24,
    alignSelf: 'center',
    paddingBottom: 10
  },
});
