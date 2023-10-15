import {
    View,
    StyleSheet,
    Text,
    Alert,
} from 'react-native';
import TextBox from './TextBox';
import tw from 'tailwind-react-native-classnames';
import PasswordBox from './PasswordBox';
import Button from './Button';
import { useState } from 'react';

const styles = StyleSheet.create({
    container: {
        justifyContent: 'center',
        flex: 1,
        alignItems: 'center',
    },
});

const LoginByEmail = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [isDisabled, setDisability] = useState(true);
    const [backgroundColor, setBackground] = useState('#abcdf3');

    function onButtonEnabled() {
        if (email.length < 1 || password.length < 1) {
            setDisability(true);
            setBackground('#abcdf3');
        }
        else {
            setDisability(false);
            setBackground('#2d81e0');
        }
    }

    function onEmailChanged(text) {
        setEmail(text);
        onButtonEnabled();
    }

    function onPasswordChanged(text) {
        setPassword(text);
        onButtonEnabled();
    }

    return (
        <View style={tw`pt-5`}>
            <View style={tw`pl-10`}>
                <Text>
                    E-mail
                </Text>
            </View>
            <View style={[styles.container, tw`pt-2`]}>
                <TextBox setValue={onEmailChanged} 
                         placeholder={"Введите E-mail"}
                         value={email}
                         isPhoneBox={false}/>
            </View>

            <View style={tw`pl-10 pt-5`}>
                <Text>
                    Пароль
                </Text>
            </View>
            <View style={[styles.container, tw`pt-2`]}>
                <PasswordBox value={password} setValue={onPasswordChanged}/>
            </View>
            <View style={[styles.container, tw`pt-10`]}>
                <Button text={'Войти'}
                        isDisabled={isDisabled}
                        backgroundColor={backgroundColor}/>
            </View>
        </View>
    );
};

export default LoginByEmail;
