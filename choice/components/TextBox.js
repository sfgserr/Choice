import { useState } from 'react';
import {
    View,
    Text,
} from 'react-native';
import tw from 'tailwind-react-native-classnames';
import Input from './Input';

const TextBox = ({placeholder, isPassword, child, value, setValue, isPhoneBox}) => {

    const [borderColor, setBorderColor] = useState('#d5d5d7');

    function onFocused() {
        setBorderColor('#3f8ae0');
    }

    function onBlured() {
        setBorderColor('#d5d5d7');
    }
    
    if (!isPhoneBox) {
        return (
            <View style={[tw`border-2 rounded-xl flex-row pl-5 items-center w-80 h-12`, {backgroundColor: '#f2f3f5', borderColor: borderColor}]}>
                <Input placeholder={placeholder}
                           isPassword={isPassword}
                           value={value}
                           setValue={setValue}
                           onFocused={onFocused}
                           onBlured={onBlured}/>
                <View style={tw`flex-row-reverse flex-1`}>
                    <View style={tw`pr-5`}>
                        {child}
                    </View>
                </View>
            </View>
        );
    }
    else {
        return (
            <View style={[tw`border-2 rounded-xl flex-row pl-5 items-center w-80 h-12`, {backgroundColor: '#f2f3f5', borderColor: borderColor}]}>
                <View style={tw`items-center flex-row`}>
                    <View style={tw`pr-2`}>
                        <Text style={tw`text-base text-black font-normal`}>
                            +7
                        </Text>
                    </View>
                    <View style={{width: 2, height: 20, backgroundColor: '#d5d5d7'}}/>
                </View>

                <View style={tw`pl-3`}>
                    <Input placeholder={'(000) 000-00-00'}
                        isPassword={false}
                       value={value}
                       setValue={setValue}
                       onFocused={onFocused}
                       onBlured={onBlured}/>
                </View>
            </View>
        );
    }
}

export default TextBox;