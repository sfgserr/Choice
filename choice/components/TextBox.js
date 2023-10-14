import {
    TextInput,
    StyleSheet,
    View,
} from 'react-native';

const styles = StyleSheet.create({
    textBox: {
        borderWidth: 1,
        borderColor: '#d5d5d7',
        backgroundColor: '#f2f3f5',
        borderRadius: 10,
        fontSize: 16,
        color: '#818c99',
        paddingLeft: 10,
    },
});

const TextBox = ({placeholder, width, height}) => {
    return (
        <View>
            <TextInput placeholder={placeholder}
                       width={width}
                       height={height}
                       style={styles.textBox}
                       placeholderTextColor="#818c99"/>
        </View>
    );
}

export default TextBox;