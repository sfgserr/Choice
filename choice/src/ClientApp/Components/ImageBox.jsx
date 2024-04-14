import React from 'react';
import {
    View,
    Image,
    TouchableOpacity,
    Dimensions
} from 'react-native';
import { Icon } from 'react-native-elements';
import * as ImagePicker from 'react-native-image-picker';

const ImageBox = ({handleState}) => {
    const { width, height } = Dimensions.get('screen');

    const [imageUri, setImageUri] = React.useState('');

    const addImage = async () => {
        let response = await ImagePicker.launchImageLibrary();

        if (!response.didCancel) {
            setImageUri(response.assets[0].uri);
            handleState(response.assets[0].uri);
        }
    }

    const removeImage = () => {
        setImageUri('');
        handleState('');
    }

    return (
        <View>
            {
                imageUri == '' ?
                <>
                    <View style={{width: height/7, height: height/7, backgroundColor: '#F9F9F9', borderWidth: 2, borderRadius: 8, borderStyle: 'dashed', borderColor: '#C8C8C8', justifyContent: 'center'}}>
                        <TouchableOpacity onPress={async () => await addImage()}>
                            <Icon 
                                name='arrow-circle-down'
                                type='material'
                                size={40}
                                color={'#2D81E0'}/>
                        </TouchableOpacity>
                    </View>
                    </>
                    :
                    <>
                        <View 
                            style={{
                                width: height/7+height/350,
                                height: height/7+height/350,
                                borderRadius: 8,
                                borderStyle: 'dashed',
                                borderWidth: 2,
                                borderColor: 'black',
                                backgroundColor: 'transparent',
                                justifyContent: 'center',
                                position: 'relative'
                            }}>
                            <View 
                                style={{
                                    position: 'absolute',
                                    top: -12,
                                    right: -13,
                                    width: '100%',
                                    alignItems: 'flex-end',
                                    zIndex: 1,
                                }}>
                                <TouchableOpacity
                                    style={{
                                        backgroundColor: 'white',
                                        borderColor: '#E7E7E7',
                                        borderRadius: 10
                                    }}
                                    onPress={() => removeImage()}>
                                    <Icon 
                                        name='close'
                                        type='material'
                                        size={20}
                                        color={'#818C99'}/>
                                </TouchableOpacity>
                            </View>
                            <Image 
                                style={{
                                width: height/7,
                                height: height/7,
                                borderRadius: 8,
                                alignSelf: 'center',
                                padding: 10
                            }}
                            source={{uri: `file://${imageUri}`}}/>
                    </View>
                </>
            }
        </View>
    );
}

export default ImageBox;