import {
    View,
    StyleSheet,
    Text,
} from 'react-native';
import TextBox from './TextBox';
import tw from 'tailwind-react-native-classnames';
import PasswordBox from './PasswordBox';

const styles = StyleSheet.create({
    container: {
        justifyContent: 'center',
        flex: 1,
        alignItems: 'center',
    },
});

const LoginByEmail = () => {
    return (
        <View style={tw`pt-5`}>
            <Text style={tw`ml-10`}>
                E-mail
            </Text>
            <View style={[styles.container, tw`pt-2`]}>
                <TextBox width={250}
                         height={40}
                         placeholder={"Введите E-mail"}/>
            </View>

            <Text style={tw`ml-10 mt-5`}>
                Пароль
            </Text>
            <View style={[styles.container, tw`pt-2`]}>
                <PasswordBox/>
            </View>
        </View>
    );
};

export default LoginByEmail;
