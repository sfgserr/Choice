import React from "react";
import {
    View,
    Image,
    Text,
    TouchableOpacity
} from 'react-native';
import env from "../env";
import { Icon } from "react-native-elements";

const Company = ({company}) => {
    return (
        <View
            style={{
                flexDirection: 'row',
                justifyContent: 'space-between'
            }}>
            <View
                style={{
                    flexDirection: 'row'
                }}>
                <Image
                    style={{
                        width: 45,
                        height: 45,
                        borderRadius: 360,
                    }}
                    source={{uri: `${env.api_url}/api/objects/${company.iconUri}`}}/>
                <View
                    style={{
                        flexDirection: 'column',
                        justifyContent: 'space-between',
                        paddingLeft: 10
                    }}>
                    <Text
                        style={{
                            fontSize: 16,
                            fontWeight: '600',
                            color: 'black'
                        }}>
                        {company.title}
                    </Text>
                    <Text
                        style={{
                            fontSize: 14,
                            fontWeight: '400',
                            color: '#99A2AD'
                        }}>
                        {`${company.address.city}, ${company.address.street}`}
                    </Text>    
                </View>    
            </View>
            <TouchableOpacity
                style={{
                    alignSelf: 'center'
                }}>
                <Icon
                    type='material'
                    name='chevron-right'
                    color='#99A2AD'
                    size={30}/>
            </TouchableOpacity>
        </View>
    )
}

export default Company;