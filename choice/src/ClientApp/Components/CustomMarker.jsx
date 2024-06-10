import {
    View,
    Image,
    Text,
} from 'react-native';
import { Icon } from 'react-native-elements';
import { Callout, Marker } from "react-native-maps";

const ImageMarker = ({imageUri}) => {
    return (
        <Image 
            style={{
                width: 40, 
                height: 40,
                borderRadius: 40/2,
                borderWidth: 4,
                borderColor: 'white',
                backgroundColor: 'white'
            }}
            source={{uri: imageUri}}
            />
    );
}

const CustomMarker = ({imageUri, coordinate}) => {
    return (
        <Marker
            coordinate={coordinate}>
            <ImageMarker
                imageUri={imageUri}/>
        </Marker>
    );
}

export default CustomMarker;