import React from "react";
import {
    View,
    Text,
    Image
} from 'react-native';
import ImageViewer from "./ImageViewer";
import env from "../env";
import { Icon } from "react-native-elements";

const CompanyPage = ({navigation, company, order}) => {
    return (
        <View
            style={{
                flex: 1,
                justifyContent: 'center'
            }}>
            <ImageViewer
                images={[
                    'https://media.istockphoto.com/id/1652160325/photo/aerial-view-of-idyllic-st%C3%A4ubifall-waterfall-and-village.webp?b=1&s=170667a&w=0&k=20&c=itQ8rlH98X7fKWPmRjZqyO2Q6Co2kWzNWTZsNAMwWnw=',
                    'https://media.istockphoto.com/id/1652160325/photo/aerial-view-of-idyllic-st%C3%A4ubifall-waterfall-and-village.webp?b=1&s=170667a&w=0&k=20&c=itQ8rlH98X7fKWPmRjZqyO2Q6Co2kWzNWTZsNAMwWnw=',
                    'https://media.istockphoto.com/id/1652160325/photo/aerial-view-of-idyllic-st%C3%A4ubifall-waterfall-and-village.webp?b=1&s=170667a&w=0&k=20&c=itQ8rlH98X7fKWPmRjZqyO2Q6Co2kWzNWTZsNAMwWnw='
                ]}/>
            <View
                style={{
                    flexDirection: 'row',
                    paddingTop: 10,
                    flex: 1
                }}>
                <Image
                    source={{uri: `${env.api_url}/api/objects/${company.iconUri}`}}
                    style={{
                        width: 45,
                        height: 45,
                        borderRadius: 360,
                        resizeMode: 'contain'
                    }}/>
                <View
                    style={{
                        flexDirection: 'column',
                        justifyContent: 'space-evenly',
                        flex: 1
                    }}>
                    <View
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                            flex: 1,
                        }}>
                        <Text
                            style={{
                                color: 'black',
                                fontWeight: '600',
                                fontSize: 16
                            }}>
                            {company.title}
                        </Text>
                        <View
                            style={{
                                flexDirection: 'row'
                            }}>
                            <Icon
                                type='material'
                                name='near-me'
                                color='#99A2AD'
                                size={20}/>
                            <Text
                                style={{
                                    color: '#99A2AD',
                                    fontSize: 13,
                                    fontWeight: '400'
                                }}>
                                {`${company.distance} м от Вас`}
                            </Text>
                        </View>    
                    </View>
                    <Text
                        style={{
                            color: '#99A2AD',
                            fontWeight: '400',
                            fontSize: 14
                        }}>
                        {`${company.address.city}, ${company.address.street}`}
                    </Text>
                </View>
            </View>
            {order == '' ?
            <>
            </>
            :
            <>
            </>}
        </View>
    )
}

export default CompanyPage;