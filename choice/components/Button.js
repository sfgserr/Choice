import { 
    StyleSheet,
    TouchableOpacity,
    Text, 
} from 'react-native';

const Button = ({text}) => {
    const styles = StyleSheet.create({
        button: {
            width: 270,
            height: 45,
            backgroundColor: '#2D81E0',
            flex: 1,
            justifyContent: 'center',
            alignItems: 'center',
            borderRadius: 10,
        },
        text: {
            fontSize: 17,
            color: '#FFF'
        },
    });

    return (
        <TouchableOpacity style={styles.button}>
            <Text style={styles.text}>
                {text}
            </Text>
        </TouchableOpacity>
    );
};

export default Button;