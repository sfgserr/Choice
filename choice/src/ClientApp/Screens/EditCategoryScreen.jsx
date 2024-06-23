import React from "react";
import {
    View,
    TouchableOpacity,
    Text
} from 'react-native';
import { Icon } from "react-native-elements";

const EditCategoryScreen = ({category}) => {
    return (
        <View>
            <View
                style={{
                    flexDirection: 'row',
                    paddingHorizontal: 10,
                    justifyContent: 'space-between'
                }}>
                <TouchableOpacity>
                    <Icon
                        name='chevron-right'
                        type='material'
                        color='#2688EB'/>
                </TouchableOpacity>    
                <Text
                    style={{
                        fontWeight: '600',
                        fontSize: 21
                    }}>
                    Категория
                </Text>
                <Text></Text>
            </View>
        </View>
    )
}

export default EditCategoryScreen;