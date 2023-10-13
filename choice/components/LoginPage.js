import * as React from 'react';
import {
    Text,
    View,
    Image,
    StyleSheet,
  } from 'react-native';

const styles = StyleSheet.create({
  logo: {
    width: 100,
    height: 100,
  }
});

function Login() {
    return (
        <View>
           <Image source={require('../resources/images/choice.png')}
                  style={styles.logo}/> 
        </View>
    );
}

export default Login;