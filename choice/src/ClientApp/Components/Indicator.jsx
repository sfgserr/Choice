import React from 'react';
import {
  View,
  Animated,
  findNodeHandle,
  Dimensions
} from 'react-native';

const {width, height} = Dimensions.get('screen');

const Indicator = ({measures, scrollX, data}) => {
    const inputRange = data.map((_, i) => i * width);
    const translateX = scrollX.interpolate({
        inputRange: inputRange,
        outputRange: measures.map(measure => measure.x-width/5+measure.width/2)
    });
    return (
        <Animated.View style={{
            position: 'absolute',
            height: 4,
            width: width/2.5,
            backgroundColor: '#3F8AE0',
            borderRadius: 10,
            bottom: -10,
            transform: [{
                translateX
            }]
        }}/>
    );
}

export default Indicator;