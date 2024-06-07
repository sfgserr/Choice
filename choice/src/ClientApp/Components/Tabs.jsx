import React from 'react';
import {
  View,
  Dimensions,
  findNodeHandle,
  TouchableOpacity,
  Text
} from 'react-native';
import Tab from './Tab.jsx';
import Indicator from './Indicator.jsx';

const {width, height} = Dimensions.get('screen');

const Tabs = ({ data, scrollX, onItemPress }) => {
    const [measures, setMeasures] = React.useState([]);
    const containerRef = React.useRef();
    React.useEffect(() => {
        let m = [];
        data.forEach(item => {
            item.ref.current.measureLayout(
                containerRef.current,
                (x, y, width, height) => {
                    m.push({
                        x, 
                        y, 
                        width, 
                        height
                    });

                    if (m.length === data.length) {
                        setMeasures(m);
                    }
                }
            );
        })
    }, [containerRef.current]);
    
    return (
        <View style={{position: 'absolute', width, paddingTop: 60}}>
            <View style={{justifyContent: 'space-evenly', flex: 1, flexDirection: 'row'}} ref={containerRef}>
                {data.map((item, index) => {
                    return <Tab key={item.key} item={item} ref={item.ref} onItemPress={() => onItemPress(index)}/>
                })}
            </View>
            { measures.length > 0 && <Indicator measures={measures} scrollX={scrollX} data={data}/>}
        </View>
    );
}

export default Tabs;