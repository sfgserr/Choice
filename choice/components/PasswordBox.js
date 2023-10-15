import {
    TouchableOpacity,
    Image,
} from 'react-native';
import * as React from 'react';
import TextBox from './TextBox';

const PasswordBox = ({value, setValue}) => {
    const [isVisible, setPasswordVisibility] = React.useState(true);

    return (
        <TextBox placeholder={"Введите пароль"}
                 isPassword={isVisible}
                 value={value}
                 setValue={setValue}
                 isPhoneBox={false}
                 child={
                    <TouchableOpacity style={{width: 15, height: 15}}
                                      onPress={() => setPasswordVisibility(!isVisible)}>
                        <Image source={require("../resources/images/passwordButton.png")}
                            style={{width: 15, height: 15}}/>
                    </TouchableOpacity> 
                }/>
    );
};

export default PasswordBox;