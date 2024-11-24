import {Dimensions, StyleSheet} from 'react-native';

const { width, height } = Dimensions.get('screen');

const Styles = StyleSheet.create({
  borderedTextInputView: {
    borderWidth: 0.5,
    backgroundColor: '#f2f3f5',
    height: height*0.054,
    borderRadius: 10,
    justifyContent: 'center',
    paddingLeft: 10,
    flexDirection: 'row'
  },
  borderedTextInput: {
    color: 'black',
    fontSize: 16,
    fontWeight: '400',
    flex: 1
  }
});

export default Styles;
