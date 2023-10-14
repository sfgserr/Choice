import * as React from 'react';
import {
    Text,
    View,
    Image,
    StyleSheet,
    SafeAreaView,
    Border,
    TouchableOpacity,
  } from 'react-native';

const styles = StyleSheet.create({
  logo: {
    width: 110,
    height: 110,
    marginTop: 20
  },
  text: {
    fontSize: 20,
    color: '#313131',
    marginTop: 10
  },
  aboutText: {
    fontSize: 16,
    fontFamily: 'OpenSans',
  },
  container: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center'
  },
});

function LoginScreen() {
    return (
        <SafeAreaView>
          <View style={styles.container}>
            <Image source={require('../resources/images/choice.png')}
                   style={styles.logo}/>
            <Text style={styles.text}>
              В Ы Б О Р
            </Text>
            <Text style={[styles.aboutText, {marginTop: 10}]}>
                Приложение для выбора
            </Text>
            <Text style={styles.aboutText}>
              лучших условий
            </Text> 
          </View>
          <View style={{flexDirection: 'row', paddingTop: 50, paddingLeft: 16}}>
            <Text style={{fontSize: 24, fontWeight: 700, color: '#313131'}}>
                Авторизация
            </Text>
            <TouchableOpacity>
              <Text style={{color: '#2D81E0', fontSize: 16, fontWeight: 400, 
                            marginLeft: 20, marginTop: 7}}>
                Создать аккаунт
              </Text>
            </TouchableOpacity>
          </View>
          <View style={{flexDirection: 'row', paddingLeft: 16, paddingTop: 20}}>
            <TouchableOpacity style={{borderColor: 'black', width: 100, justifyContent: 'center',
                                      paddingLeft: 50}}>
              <Text style={{fontSize: 16, color: '#000000'}}>
                E-mail
              </Text>
            </TouchableOpacity>
            <TouchableOpacity style={{borderColor: 'black', width: 100, justifyContent: 'center',
                                      paddingLeft: 80, width: 'auto'}}>
              <Text style={{fontSize: 16, color: '#000000'}}>
                Телефон
              </Text>
            </TouchableOpacity>
          </View>
        </SafeAreaView>
    );
}

export default LoginScreen;