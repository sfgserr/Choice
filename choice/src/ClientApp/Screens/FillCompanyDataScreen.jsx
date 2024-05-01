import React from "react";
import {
    View,
    Text,
    Dimensions,
    FlatList
} from 'react-native';
import ContactDetailsScreen from "./ContactDetailsScreen";
import SocialMediasScreen from "./SocialMediasScreen";

const FillCompanyDataScreen = () => {
    const { width, height } = Dimensions.get('screen');
    const [siteUrl, setSiteUrl] = React.useState('');
    const [index, setIndex] = React.useState(1); 

    const ref = React.useRef();

    const data = [
        {
            screen: ContactDetailsScreen,
            handleState: (text) => {
                setSiteUrl(text);
                onItemPress(1);
                setIndex(2);
            }
        },
        {
            screen: SocialMediasScreen,
            handleState: () => {
                onItemPress(2);
                setIndex(3);
            }
        }
    ]

    const onItemPress = React.useCallback(itemIndex => {
        ref?.current?.scrollToOffset({
            offset: itemIndex * width
        });
    });

    return (
        <View
            style={{
                flex: 1,
                backgroundColor: 'white'
            }}>
            <View>
                <Text
                    style={{
                        color: 'black',
                        fontSize: 21,
                        fontWeight: '600',
                        paddingTop: 20,
                        alignSelf: 'center'
                    }}>
                    Карточкая компании
                </Text>
                <View style={{paddingHorizontal: 20}}>
                    <View
                        style={{
                            paddingTop: 30,
                            justifyContent: 'space-between',
                            flexDirection: 'row'
                        }}>
                        <View
                            style={{
                                height: 4.5,
                                width: width/3.5,
                                borderRadius: 10,
                                backgroundColor: 1 <= index ? '#2688EB' : '#DFDFDF'
                            }}/> 
                        <View
                            style={{
                                height: 4.5,
                                width: width/3.5,
                                borderRadius: 10,
                                backgroundColor: 2 <= index ? '#2688EB' : '#DFDFDF'
                            }}/>
                        <View
                            style={{
                                height: 4.5,
                                width: width/3.5,
                                borderRadius: 20,
                                backgroundColor: 3 <= index ? '#2688EB' : '#DFDFDF'
                            }}/>   
                    </View>
                    <View style={{paddingTop: 15}}>
                        <View style={{height: .6, backgroundColor: 'black'}}/>
                    </View>
                </View>
            </View>
            <FlatList
                    scrollEnabled={false}
                    ref={ref}
                    data={data}
                    horizontal
                    pagingEnabled
                    contentContainerStyle={{flexGrow: 1}}
                    renderItem={({item}) => {
                        return <View style={{width, flex: 1}}>
                                <item.screen handleState={(text) => item.handleState(text)}/>
                            </View>
                    }}/>
        </View>
    )
}

export default FillCompanyDataScreen;