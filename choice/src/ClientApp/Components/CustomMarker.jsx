import {
    View,
    Image
} from 'react-native';
import { Marker } from "react-native-maps";

const CustomMarker = ({imageUri, coordinate}) => {
    return (
        <Marker 
            coordinate={coordinate}
            style={{
                width: 40,
                height: 40
            }}>
            <Image 
                style={{
                    width: 40, 
                    height: 40,
                    borderRadius: 40/2,
                    borderWidth: 4,
                    borderColor: 'white',
                    backgroundColor: 'white'
                }}
                source={{uri: imageUri}}/>
        </Marker>
    );
}

export default CustomMarker;