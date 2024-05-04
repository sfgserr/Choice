import React from "react";
import {
    View,
    Text,
    TouchableOpacity
} from 'react-native';
import { Icon } from "react-native-elements";
import styles from "../Styles";
import { Modalize } from "react-native-modalize";

const AboutScreen = ({handleState}) => {
    const modalRef = React.useRef(null);

    const [categoryString, setCategoryString] = React.useState('');

    return (
        <View
            style={{
                flex: 1,
                backgroundColor: 'white',
                paddingTop: 10,
                paddingHorizontal: 20,
            }}>
            <Modalize
                ref={modalRef}
                adjustToContentHeight={true}
                childrenStyle={{height: '50%'}}>

            </Modalize>
            <Text
                style={{
                    color: 'black',
                    fontWeight: '700',
                    fontSize: 17
                }}>
                О работе    
            </Text>
            <Text
                style={{
                    color: '#6D7885', 
                    fontWeight: '400', 
                    fontSize: 14, 
                    paddingTop: 20,
                    paddingBottom: 5
                }}>
                Виды деятельности        
            </Text>
            <View>
                <View style={[styles.textInput, {justifyContent: 'center'}]}>
                    <View
                        style={{
                            justifyContent: 'space-between',
                            flexDirection: 'row'
                        }}>
                        <Text
                            style={{
                                color: '#818C99',
                                fontSize: 16,
                                fontWeight: '400',
                                flex: 2
                            }}>
                            Выбрать деятельность
                        </Text>
                        <TouchableOpacity 
                            style={{
                                alignSelf: 'center'    
                            }}
                            onPress={() => modalRef.current?.open()}>
                            <Icon
                                color='gray'
                                type='material'
                                name='expand-more'/>
                        </TouchableOpacity>
                    </View>
                </View>
            </View>
        </View>
    )
}

export default AboutScreen;