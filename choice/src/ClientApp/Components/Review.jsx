import React from "react";
import {
    View,
    Text
} from 'react-native';
import { Icon } from "react-native-elements";

const Review = ({review}) => {
    const stars = [1,2,3,4,5]
    console.log(review);
    return (
        <View
            style={{
                justifyContent: 'center',
                flexDirection: 'column'
            }}>
            <View
                style={{
                    flexDirection: 'row',
                    justifyContent: 'space-between'
                }}>
                <Text
                    style={{
                        fontWeight: '600',
                        fontSize: 16,
                        color: 'black'
                    }}>
                    {`${review.author.name.split(' ')[0]} ${review.author.name.split(' ')[1][0]}.`}
                </Text>
                <View
                    style={{
                        flexDirection: 'row'
                    }}>
                    {stars.map(s => (
                        <Icon
                            type='material'
                            name='star'
                            size={20}
                            color={s <= review.grade ? '#E4E839' : '#C8C8C8'}/>    
                        ))}
                </View>
            </View>
            <Text
                style={{
                    color: '#99A2AD',
                    fontSize: 14,
                    fontWeight: '400',
                    paddingTop: 10
                }}>
                {review.text}
            </Text>
        </View>
    )
}

export default Review;