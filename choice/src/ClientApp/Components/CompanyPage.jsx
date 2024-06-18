import React from "react";
import {
    View,
    Text,
    Image,
    Dimensions,
    TouchableOpacity,
} from 'react-native';
import ImageViewer from "./ImageViewer";
import env from "../env";
import { Icon } from "react-native-elements";
import urlValidator from "../validators/urlValidator";
import styles from "../Styles";
import CompanyPageOrderCard from "./CompanyPageOrderCard";
import { Modalize } from "react-native-modalize";

const CompanyPage = ({navigation, company, order, onReviewPressed}) => {
    const wh = Dimensions.get('screen');

    const getImageSource = (url) => {
        if (urlValidator.validateInstagramUrl(url)) {
            return require('../resources/instagram.png');
        }

        if (urlValidator.validateFacebookUrl(url)) {
            return require('../resources/facebook.png');
        }

        if (urlValidator.validateVkUrl(url)) {
            return require('../resources/vk.png');
        }

        if (urlValidator.validateTgUrl(url)) {
            return require('../resources/telegram.png');
        }

        if (url.includes('@')) {
            return require('../resources/email.png');
        }

        return require('../resources/phone.png');
    }

    let contacts = company.socialMedias.filter(s => s != '');
    contacts.push(company.email)
    contacts.push(company.phoneNumber);

    contacts = Object.keys(contacts).map(i => ({
        id: i,
        url: contacts[i]
    }));

    return (
        <View
            style={{
                flex: 1,
                justifyContent: 'center'
            }}>
            <ImageViewer
                images={company.photoUris.filter(p => p != '')
                    .map(p => `${env.api_url}/api/objects/${p}`)}/>
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
                        borderRadius: 45,
                    }}/>
                <View
                    style={{
                        flexDirection: 'column',
                        justifyContent: 'space-evenly',
                        paddingLeft: 10,
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
            {order != '' ?
            <>
                <View
                    style={{paddingTop: 10}}>
                    <CompanyPageOrderCard
                        message={order}
                        navigation={navigation}/>
                </View>
            </>
            :
            <>
            </>}
            <View
                style={{
                    flexDirection: 'row',
                    paddingTop: 10
                }}>
                <TouchableOpacity
                    style={{
                        borderRadius: 15,
                        backgroundColor: 'white',
                        flexDirection: 'column',
                        justifyContent: 'center',
                        padding: 10,
                        shadowColor: 'black',
                        shadowOpacity: 0.1,
                        shadowRadius: 100,
                        elevation: 3,
                        shadowOffset: {
                            width: 50,
                            height: 50
                        }
                    }}
                    onPress={() => onReviewPressed()}>
                    <View
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'center'
                        }}>
                        <Icon
                            type='material'
                            name='star'
                            color='#E4E839'
                            size={25}
                            style={{alignSelf: 'center'}}/>
                        <Text
                            style={{
                                fontWeight: '600',
                                fontSize: 16,
                                color: 'black',
                                alignSelf: 'center'
                            }}>
                            {company.averageGrade}
                        </Text>
                    </View>
                    <Text
                        style={{
                            fontSize: 14,
                            fontWeight: '400',
                            color: '#99A2AD',
                            alignSelf: 'center',
                        }}>
                        {`${company.reviewCount} отзывов`}
                    </Text>
                </TouchableOpacity>
            </View>
            <View
                style={{
                    justifyContent: 'space-evenly',
                    flexDirection: 'row',
                    paddingTop: 20
                }}>
                {contacts.map(c => (
                    <TouchableOpacity
                        key={c.id}
                        style={{
                            height: wh.height/15,
                            width: wh.height/15,
                            borderRadius: 360,
                            backgroundColor: '#F4F4F4',
                            borderWidth: 1,
                            borderColor: '#EEEEEE'
                        }}>
                        <Image
                            source={getImageSource(c.url)}
                            style={{
                                height: wh.height/15,
                                width: wh.width/15,
                                alignSelf: 'center'
                            }}
                            resizeMode='contain'/>    
                    </TouchableOpacity>
                ))}    
            </View>
            {order == '' ?
            <>
                <View
                    style={{paddingTop: 20}}>
                    <TouchableOpacity
                        style={styles.button}
                        onPress={() => {
                            navigation.navigate('Chat', {chatId: company.guid})
                        }}>
                        <Text
                            style={styles.buttonText}>
                            Перейти в чат
                        </Text>
                    </TouchableOpacity>
                </View>
            </>
            :
            <>
            </>}
        </View>
    )
}

export default CompanyPage;