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
  },
  styledButton: {
    height: height*0.054,
    backgroundColor: '#2D81E0',
    borderRadius: 10
  },
  styledButtonContent: {
    fontWeight: '500',
    fontSize: 17,
    color: 'white'
  }
});

export default Styles;
