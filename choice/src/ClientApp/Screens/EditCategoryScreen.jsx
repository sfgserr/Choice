import React from "react";
import {
    View,
    TouchableOpacity,
    Text,
    Image
} from 'react-native';
import { Icon } from "react-native-elements";
import env from "../env";
import CustomTextInput from "../Components/CustomTextInput";
import styles from "../Styles";

const EditCategoryScreen = ({navigation, route}) => {
    const { category } = route.params;

    const [title, setTitle] = React.useState(category.title);

    return (
        <View
            style={{
                flex: 1,
                backgroundColor: 'white'
            }}>
            <View
                style={{
                    flexDirection: 'row',
                    paddingTop: 10,
                    paddingHorizontal: 10,
                    justifyContent: 'space-between'
                }}>
                <TouchableOpacity
                    style={{
                        alignSelf:'center'
                    }}
                    onPress={() => navigation.goBack()}>
                    <Icon
                        name='chevron-left'
                        type='material'
                        color='#2688EB'
                        size={40}/>
                </TouchableOpacity>  
                <Text></Text>
            </View>
            <Text
                style={{
                    fontWeight: '600',
                    color: 'black',
                    fontSize: 21,
                    alignSelf: 'center',
                    position: 'absolute',
                    top: 15
                }}>
                Категория
            </Text>
            <View
                style={{
                    paddingTop: 20
                }}>
                <View
                    style={{
                        height: 100,
                        width: 100,
                        borderRadius: 360,
                        alignSelf: 'center',
                        justifyContent: 'center',
                        backgroundColor: '#47A4F9'
                    }}>
                    <Image
                        source={{uri: `${env.api_url}/api/objects/${category.iconUri}`}}
                        style={{
                            alignSelf: 'center',
                            height: 30,
                            width: 30 
                        }}/>
                </View>
            </View>
            <View
                style={{
                    paddingTop: 20
                }}>
                <TouchableOpacity>
                    <Text
                        style={{
                            fontSize: 15,
                            fontWeight: '500',
                            color: '#2D81E0',
                            alignSelf: 'center'
                        }}>
                        Изменить иконку    
                    </Text>
                </TouchableOpacity>
            </View>
            <View
                style={{
                    paddingTop: 20,
                    paddingHorizontal: 20
                }}>
                <View
                    style={{
                        borderRadius: 20,
                        paddingHorizontal: 10,
                        backgroundColor: 'white',
                        flexDirection: 'row',
                        shadowColor: 'black',
                        shadowOpacity: 0.1,
                        shadowRadius: 100,
                        elevation: 3,
                        shadowOffset: {
                            width: 50,
                            height: 50
                        }
                    }}>
                    <Icon
                        type='material'
                        name='error'
                        color='#818C99'
                        size={30}
                        style={{
                            alignSelf: 'center',
                            paddingTop: 10,
                            paddingBottom: 10
                        }}/>
                    <Text
                        style={{
                            color: '#6D7885',
                            fontWeight: '400',
                            fontSize: 14,
                            paddingTop: 10,
                            paddingBottom: 10,
                            alignSelf: 'center',
                            paddingLeft: 5
                        }}>
                        {'Иконки PNG на прозрачном фоне.\nЦвет заливки - белый. Стиль - Outline'}
                    </Text>    
                </View>
            </View>
            <View
                style={{
                    paddingHorizontal: 20
                }}>
                <Text
                    style={{
                        color: '#F2F3F5',
                        fontSize: 14,
                        fontWeight: '400',
                        color: '#6D7885',
                        paddingTop: 30,
                        paddingBottom: 5
                    }}>
                    Название категории
                </Text>
                <CustomTextInput
                    value={title}
                    changed={setTitle}
                    readonly={category.id >= 1 && category.id <= 7}/>
            </View>
            {!(category.id >= 1 && category.id <= 7) ?
            <>
                <View
                    style={{
                        flex: 1,
                        justifyContent: 'flex-end',
                        bottom: 10,
                        paddingHorizontal: 20
                    }}>
                    <TouchableOpacity
                        style={styles.button}>
                        <Text
                            style={styles.buttonText}>
                            Сохранить изменения
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

export default EditCategoryScreen;