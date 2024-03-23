import React from 'react';
import {
  View,
  Dimensions,
  findNodeHandle
} from 'react-native';
import Tab from './Tab.jsx';
import Indicator from './Indicator.jsx';

const {width, height} = Dimensions.get('screen');

const Tabs = ({ data, scrollX }) => {
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

                    console.log(x, y, width, height);

                    if (m.length === data.length) {
                        setMeasures(m);
                    }
                }
            );
        })
    }, []);

    return (
        <View style={{position: 'absolute', width, paddingTop: 60}}>
            <View style={{justifyContent: 'space-evenly', flex: 1, flexDirection: 'row'}} ref={containerRef}>
                {data.map((item) => {
                    return <Tab key={item.key} item={item} ref={item.ref}/>
                })}
            </View>
            { measures.length > 0 && <Indicator measures={measures} scrollX={scrollX} data={data}/>}
        </View>
    );
}

export default Tabs;