import {Dimensions, StyleSheet} from 'react-native';

const { width, height } = Dimensions.get('screen');

const Styles = StyleSheet.create({
  borderedTextInputView: {
    borderWidth: 0.5,
    height: height*0.054,
    borderRadius: 10,
    justifyContent: 'center',
    paddingLeft: 10,
    flexDirection: 'row'
  },
  borderedTextInputViewColor: {
    backgroundColor: '#f2f3f5',
  },
  borderedTextInputFocused: {
    borderColor: '#3F8AE0'
  },
  borderedTextInputUnfocused: {
    borderColor: '#d5d6d8'
  },
  borderedTextInputError: {
    backgroundColor: '#FAEBEB',
    borderColor: '#E64646'
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
