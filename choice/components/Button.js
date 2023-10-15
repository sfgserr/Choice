import { 
    TouchableOpacity,
    Text, 
} from 'react-native';
import tw from 'tailwind-react-native-classnames';

const Button = ({text, isDisabled, backgroundColor}) => {

    return (
        <TouchableOpacity style={[{backgroundColor: backgroundColor,}, tw`flex-1 justify-center items-center rounded-xl w-80 h-12`]}
                          disabled={isDisabled}>
            <Text style={tw`text-white text-lg`}>
                {text}
            </Text>
        </TouchableOpacity>
    );
};

export default Button;