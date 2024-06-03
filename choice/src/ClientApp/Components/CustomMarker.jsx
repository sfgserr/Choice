import {
    View,
    Image,
    Text,
} from 'react-native';
import { Icon } from 'react-native-elements';
import { Callout, Marker } from "react-native-maps";

const CustomCallout = ({company}) => {
    <View>
        <View
            style={{
                width: 50, 
                height: 50,
                borderRadius: 10,
                backgroundColor: 'white',
                flexDirection: 'column',
            }}>
            <View
                style={{
                    flexDirection: 'row',
                    justifyContent: 'space-between'
                }}>
                <Text
                    style={{
                        fontSize: 13,
                        color: '#979797',
                        fontWeight: '500'
                    }}>
                    {company.title}
                </Text>
                <View
                    style={{
                        width: 2,
                        backgroundColor: '#979797'
                    }}/>
                <Icon
                    type='material'
                    name='star'
                    color='yellow'
                    size={20}/>  
                <Text>
                    {company.averageGrade} 
                </Text>  
            </View>
        </View>
    </View>
}

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

const CustomMarker = ({imageUri, coordinate, message, company}) => {
    return (
        <Marker
            coordinate={coordinate}>
            <ImageMarker
                imageUri={imageUri}/>
            <Callout tooltip>
                <CustomCallout company={company == undefined ? '' : company}/>
            </Callout>
        </Marker>
    );
}

export default CustomMarker;