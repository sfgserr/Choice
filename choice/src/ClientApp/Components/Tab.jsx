import React from 'react';
import {
  View,
  Text
} from 'react-native';


const Tab = React.forwardRef(({ item }, ref) => {
  return (
    <View ref={ref}>
        <Text 
          style={{
            color: 'black', 
            fontSize: 16, 
            fontWeight: '600'
          }}
        >
            {item.title}
        </Text>
    </View>
  );
});

export default Tab;