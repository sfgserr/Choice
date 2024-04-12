import { Dimensions, StyleSheet } from "react-native";

const {width, height} = Dimensions.get('screen');

const styles = StyleSheet.create({
    button: {
      height: height/18,
      borderWidth: 0,
      backgroundColor: '#2D81E0',
      borderRadius: 10,
      justifyContent: 'center'
    },
    buttonText: {
      color: 'white',
      fontSize: 17,
      fontWeight: '500',
      alignSelf: 'center'
    },
    textInput: {
      backgroundColor: '#f2f3f5',
      borderRadius: 10, 
      borderColor: '#d5d5d7', 
      borderWidth: 1, 
      height: height/18,
      paddingHorizontal: 15,
    },
    textInputFont: {
      color: 'black',
      fontWeight: '400',
      fontSize: 16
    }
});

export default styles;