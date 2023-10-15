import {
    TextInput,
    View,
} from 'react-native';
import tw from 'tailwind-react-native-classnames';

const Input = ({placeholder, isPassword, value, setValue, onFocused, onBlured}) => {
    return (
        <View>
            <TextInput placeholder={placeholder}
                    style={tw`text-base text-black w-56`}
                    secureTextEntry={isPassword}
                    placeholderTextColor="#818c99"
                    onFocus={onFocused}
                    onBlur={onBlured}
                    value={value}
                    onChangeText={text => setValue(text)}
                    selectionColor={'#3F8AE0'}/>
        </View>
    );
}

export default Input;