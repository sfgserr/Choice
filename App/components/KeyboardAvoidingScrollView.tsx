import {Keyboard, ScrollView, View} from 'react-native';
import React from 'react';
import {useSafeAreaInsets} from 'react-native-safe-area-context';

export default function KeyboardAvoidingScrollView({ children, scrollable, tabs, focused }: {
  children: any,
  scrollable: boolean,
  tabs: boolean,
  focused: boolean}) {
  const insects = useSafeAreaInsets();

  const [paddingBottom, setPaddingBottom] = React.useState<number>(0);

  React.useEffect(() => {
    if (focused) {
      Keyboard.addListener('keyboardDidShow', keyboardDidShow);
      Keyboard.addListener('keyboardDidHide', keyboardDidHide);
    } else {
      Keyboard.removeAllListeners('keyboardDidShow');
      Keyboard.removeAllListeners('keyboardDidHide');
    }
  }, [focused]);

  const keyboardDidShow = React.useCallback((e) => {
    let newKeyboardHeight = e.endCoordinates.height + paddingBottom;

    if (tabs) {
      newKeyboardHeight -= insects.top;
    }

    setPaddingBottom(newKeyboardHeight);
  }, [paddingBottom]);

  const keyboardDidHide = React.useCallback((e) => {
    setPaddingBottom(0);
  }, []);

  return (
    <View
      style={{
        flex: 1,
        backgroundColor: 'white',
        paddingBottom,
      }}>
      {scrollable ? (
        <ScrollView
          style={{flex: 1}}
          showsVerticalScrollIndicator={false}>
          {children}
        </ScrollView>
      ) : (
        <View
          style={{flex: 1}}>
          {children}
        </View>
      )}
    </View>
  );
}
