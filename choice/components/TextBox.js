import { useState } from 'react';
import {
    TextInput,
    StyleSheet,
    View,
    TouchableOpacity,
    Image,
} from 'react-native';

const TextBox = ({placeholder, isPassword, child}) => {
    const [borderColor, setBorderColor] = useState('#d5d5d7');

    const styles = StyleSheet.create({
        textBox: {
            borderWidth: 1,
            borderColor: borderColor,
            backgroundColor: '#f2f3f5',
            borderRadius: 10,
            flexDirection: 'row',
            paddingLeft: 20,
            alignItems: 'center',
        },
        text: {
            fontSize: 16,
            color: '#000000',
            width: 220,
        },
        child: {
            alignItems: 'flex-end',
            flexDirection: 'row',
        },
    });

    function onFocused() {
        setBorderColor('#3f8ae0');
    }

    function onBlured() {
        setBorderColor('#d5d5d7');
    }

    return (
        <View style={styles.textBox}
              width={270}
              height={45}>
            <TextInput placeholder={placeholder}
                       style={styles.text}
                       secureTextEntry={isPassword}
                       placeholderTextColor="#818c99"
                       onFocus={onFocused}
                       onBlur={onBlured}
                       selectionColor={'#3F8AE0'}/>
            <View style={styles.child}>
                {child}
            </View>
        </View>
    );
}

export default TextBox;