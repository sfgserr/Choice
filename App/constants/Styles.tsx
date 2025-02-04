import {Dimensions, StyleSheet} from 'react-native';

const { width, height } = Dimensions.get('screen');

const Styles = StyleSheet.create({
  borderedTextInputView: {
    borderRadius: 10,
    paddingLeft: 10,
    flexDirection: 'row',
  },
  borderedTextInputHeight: {
    height: height * 0.054,
  },
  borderedTextInputBigHeight: {
    height: height * 0.116,
  },
  borderedTextInputViewColor: {
    backgroundColor: '#f2f3f5',
  },
  borderedTextInputFocused: {
    borderColor: '#3F8AE0',
    borderWidth: 0.5,
  },
  borderedTextInputUnfocused: {
    borderColor: '#d5d6d8',
    borderWidth: 0.5,
  },
  borderedTextInputError: {
    backgroundColor: '#FAEBEB',
    borderColor: '#E64646',
    borderWidth: 0.5,
  },
  borderedTextInput: {
    color: 'black',
    fontSize: 16,
    fontWeight: '400',
    flex: 1,
    alignSelf: 'center',
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
  },
  title: {
    color: '#6D7885',
    fontSize: 14,
    fontWeight: '400',
  }
});

export default Styles;
