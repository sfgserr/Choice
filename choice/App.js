/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 */

import * as React from 'react';
import {
  SafeAreaView,
  ScrollView,
  StyleSheet,
  View,
} from 'react-native';
import LoginScreen from './screens/LoginScreen';

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    flex: 1,
    justifyContent: 'center',
  }
});

function App() {
  return (
    <SafeAreaView>
      <ScrollView>
        <LoginScreen/>
      </ScrollView>
    </SafeAreaView>
  );
}

export default App;
