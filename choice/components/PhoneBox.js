import {

} from 'react-native';

import TextBox from './TextBox'; 

const PhoneBox = ({value, setValue}) => {
    return (
        <TextBox value={value}
                 setValue={setValue}
                 isPhoneBox={true}/>
    );
}

export default PhoneBox;