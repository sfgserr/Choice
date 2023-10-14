import { useState } from 'react';
import {
    TextInput,
    StyleSheet,
    View,
    TouchableOpacity,
    Image,
} from 'react-native';

const styles = StyleSheet.create({
    textBox: {
        borderWidth: 1,
        borderColor: '#d5d5d7',
        backgroundColor: '#f2f3f5',
        borderRadius: 10,
        paddingLeft: 10,
        flexDirection: 'row',
        paddingLeft: 20,
    },
    text: {
        fontSize: 16,
        color: '#818c99',
    },
    child: {
        paddingLeft: 50,
        justifyContent: 'center',
    },
});

const TextBox = ({placeholder, width, height, isPassword, child}) => {
    return (
        <View style={styles.textBox}
              width={width}
              height={height}>
            <TextInput placeholder={placeholder}
                       style={styles.text}
                       secureTextEntry={isPassword}
                       placeholderTextColor="#818c99"/>
            <View style={styles.child}>
                {child}
            </View>
        </View>
    );
}

export default TextBox;