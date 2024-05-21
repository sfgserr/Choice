import React from "react";
import {
    View,
    Text,
    TouchableOpacity,
    Image
} from 'react-native';
import categoryStore from "../services/categoryStore";
import { Icon } from "react-native-elements";
import styles from "../Styles";
import env from "../env";

const CompanyRequestCard = ({orderRequest, navigation, button}) => {
    const [categories, setCategories] = React.useState(categoryStore.getCategories());

    return (
        <View
            style={{
                borderRadius: 15,
                backgroundColor: 'white',
                paddingHorizontal: 15,
                width: '98%',
                alignSelf: 'center',
                shadowColor: 'black',
                shadowOpacity: 0.1,
                shadowRadius: 100,
                elevation: 3,
                shadowOffset: {
                    width: 50,
                    height: 50
                }
            }}>
            <Text
                style={{
                    color: 'black',
                    fontSize: 14,
                    paddingTop: 20,
                    fontWeight: '600'
                }}>
                {categories[categories.findIndex(c => c.id == orderRequest.categoryId)].title}
            </Text>
            <Text
                style={{
                    color: '#313131',
                    fontSize: 15,
                    fontWeight: '400',
                    width: '70%',
                    paddingTop: 10
                }}>
                {orderRequest.description}
            </Text>
            {
                orderRequest.photoUris[0] == '' ?
                <>
                </>
                :
                <>
                    <View
                        style={{flexDirection: 'row', paddingTop: 10}}>
                        <Icon
                            type='material'
                            name='image'
                            color='#2D81E0'
                            style={{
                                alignSelf: 'center'
                            }}/>

                        <View
                            style={{
                                alignSelf: 'center',
                                paddingLeft: 5,
                                width: '50%'
                            }}>
                            <TouchableOpacity
                                style={{
                                    borderColor: '#2D81E0',
                                    borderBottomWidth: 1
                                }}>
                                <Text
                                    style={{
                                        fontSize: 14,
                                        fontWeight: '400',
                                        color: '#2D81E0',
                                    }}
                                    numberOfLines={1}>
                                    {orderRequest.photoUris[0]}
                                </Text>
                            </TouchableOpacity>
                        </View>       
                    </View>
                </>
            }
            {
                orderRequest.photoUris[1] == '' ?
                <>
                </>
                :
                <>
                    <View
                        style={{flexDirection: 'row', paddingTop: 10}}>
                        <Icon
                            type='material'
                            name='image'
                            color='#2D81E0'
                            style={{
                                alignSelf: 'center'
                            }}/>

                        <View
                            style={{
                                alignSelf: 'center',
                                paddingLeft: 5,
                                width: '50%'
                            }}>
                            <TouchableOpacity
                                style={{
                                    borderColor: '#2D81E0',
                                    borderBottomWidth: 1
                                }}>
                                <Text
                                    style={{
                                        fontSize: 14,
                                        fontWeight: '400',
                                        color: '#2D81E0',
                                    }}
                                    numberOfLines={1}>
                                    {orderRequest.photoUris[1]}
                                </Text>
                            </TouchableOpacity>
                        </View>       
                    </View>
                </>
            }
            {
                orderRequest.photoUris[2] == '' ?
                <>
                </>
                :
                <>
                    <View
                        style={{flexDirection: 'row', paddingTop: 10}}>
                        <Icon
                            type='material'
                            name='image'
                            color='#2D81E0'
                            style={{
                                alignSelf: 'center'
                            }}/>

                        <View
                            style={{
                                alignSelf: 'center',
                                paddingLeft: 5,
                                width: '50%'
                            }}>
                            <TouchableOpacity
                                style={{
                                    borderColor: '#2D81E0',
                                    borderBottomWidth: 1
                                }}>
                                <Text
                                    style={{
                                        fontSize: 14,
                                        fontWeight: '400',
                                        color: '#2D81E0',
                                    }}
                                    numberOfLines={1}>
                                    {orderRequest.photoUris[2]}
                                </Text>
                            </TouchableOpacity>
                        </View>       
                    </View>
                </>
            }
            <View style={{paddingTop: 10}}>
                <View
                    style={{
                        height: 2,
                        backgroundColor: '#f0f0f0'
                    }}/>
            </View>
            <View
                style={{
                    paddingTop: 10,
                    paddingBottom: 5,
                    flexDirection: 'row'
                }}>
                <Image
                    source={{uri: `${env.api_url}/api/objects/${orderRequest.client.iconUri}`}}
                    style={{
                        width: 40,
                        height: 40,
                        borderRadius: 360
                    }}/>
                <View
                    style={{
                        flexDirection: 'column',
                        justifyContent: 'center',
                        paddingLeft: 10,
                        flex: 1
                    }}>
                    <View
                        style={{
                            flexDirection: 'row',
                            justifyContent: 'space-between',
                        }}>
                        <Text
                            style={{
                                color: 'black',
                                fontWeight: '500',
                                fontSize: 15,
                                alignSelf: 'center'
                            }}>
                            {`${orderRequest.client.name} ${orderRequest.client.surname}`}
                        </Text>
                        <View
                            style={{
                                flexDirection: 'row'
                            }}>
                            <Icon
                                type='material'
                                name='star'
                                color='#E4E839'
                                size={20}
                                style={{
                                    alignSelf: 'center'
                                }}/>
                            <Text
                                style={{
                                    alignSelf: 'center',
                                    fontSize: 12,
                                    fontWeight: '600',
                                    color: '#575757'
                                }}>
                                {orderRequest.client.averageGrade}
                            </Text>   
                        </View>                         
                    </View>
                    <Text
                        style={{
                            color: '#909499',
                            fontWeight: '400',
                            fontSize: 12
                        }}>
                        {`Совершенно заказов: ${orderRequest.client.finishedOrdersCount}`}
                    </Text>
                </View>    
            </View>
            {
                button ?
                <>
                    <View
                        style={{
                            paddingTop: 5,
                            paddingBottom: 5
                        }}>
                        <TouchableOpacity
                            style={styles.button}
                            onPress={() => navigation.navigate('CompanyRequestCreation', {
                                orderRequest
                            })}>
                            <Text
                                style={styles.buttonText}>
                                Ответить
                            </Text>
                        </TouchableOpacity>
                    </View>
                </>
                :
                <>
                </>
            }   
        </View>
    )
}

export default CompanyRequestCard;