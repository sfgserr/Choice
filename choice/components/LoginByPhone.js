import {
    View,
    Text,
} from 'react-native';
import * as React from 'react';
import tw from 'tailwind-react-native-classnames';
import PhoneBox from './PhoneBox';
import Button from './Button';

const LoginByPhone = () => {
    const [phone, setPhone] = React.useState('');
    const [isDisabled, setDisability] = React.useState(true);
    const [backgroundColor, setBackground] = React.useState('#abcdf3');

    function onPhoneChanged(text) {
        setPhone(text);
        if (phone.length < 7) {
            setDisability(true);
            setBackground('#abcdf3');
        }
        else {
            setDisability(false);
            setBackground('#2D81E0');
        }
    }

    return (
        <View style={tw`pt-5`}>
            <View style={tw`pl-10`}>
                <Text>
                    Телефон
                </Text>
            </View>
            <View style={tw`justify-center items-center flex-1 pt-2`}>
                <PhoneBox value={phone}
                          setValue={onPhoneChanged}/>
            </View>
            <View style={tw`pt-5 justify-center items-center flex-1`}>
                <Button text={'Отправить код'}
                        isDisabled={isDisabled}
                        backgroundColor={backgroundColor}/>
            </View>
        </View>
    );
};

export default LoginByPhone;