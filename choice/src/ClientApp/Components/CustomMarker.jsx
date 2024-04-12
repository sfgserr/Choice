import {
    View
} from 'react-native';
import { Marker } from "react-native-maps";

const CustomMarker = ({imageUri}) => {
    return (
        <Marker>
            <View style={{borderRadius: 360}}>
                <Image style={{width: 35, height: 35}}
                       source={{uri: imageUri}}/>
            </View>
        </Marker>
    );
}

export default CustomMarker;