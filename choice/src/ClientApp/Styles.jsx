import { Dimensions, StyleSheet } from "react-native";

const {width, height} = Dimensions.get('screen');

const styles = StyleSheet.create({
    textInput: {
      backgroundColor: '#f2f3f5',
      borderRadius: 10, 
      borderColor: '#d5d5d7', 
      borderWidth: 1, 
      height: height/18
    }
});

export default styles;