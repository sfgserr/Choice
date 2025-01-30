import * as React from 'react';
import {
  Animated, Dimensions, NativeScrollEvent, NativeSyntheticEvent,
  Text, TouchableOpacity,
  View, ViewToken,
} from 'react-native';
import {ForwardedRef} from 'react';
import {TabBarProps} from '../types/ComponentTypes.ts';
import {
  IndicatorProps,
  MeasuresType,
  TabProps,
  TabsProps,
  TabsType,
} from '../types/TabTypes.ts';

export default function TabBar({tabs}: TabBarProps) {
  const { width } = Dimensions.get('screen');

  const data = Object.keys(tabs).map<TabsType>((v, i) => ({
    name: tabs[i].title,
    key: v,
    tab: tabs[i].element,
    ref: React.createRef<View>()
  }));

  const scrollX = React.useRef(new Animated.Value(0)).current;

  const Tab = React.forwardRef(({item, onItemPress, color}: TabProps, ref: ForwardedRef<View>) => {

    return (
      <TouchableOpacity
        ref={ref}
        onPress={() => onItemPress()}>
        <Animated.Text
          style={{
            fontWeight: color == 'black' ? '600' : '500',
            fontSize: 16,
            color: color,
          }}>
          {item.name}
        </Animated.Text>
      </TouchableOpacity>
    );
  });

  const Indicator = ({scrollX, measures}: IndicatorProps) => {
    const inputRange = data.map((_, i) => i*width);
    const indicatorWidth = scrollX.interpolate({
      inputRange,
      outputRange: measures.map((m) => m.width*2.0)
    });
    const translateX = scrollX.interpolate({
      inputRange,
      outputRange: measures.map((m) => m.x-m.width*0.5)
    });

    return (
      <Animated.View
        style={{
          height: 2,
          borderRadius: 20,
          width: indicatorWidth,
          left: 0,
          transform: [{
            translateX
          }],
          backgroundColor: '#3F8AE0'
        }}/>
    )
  }

  const Tabs = ({scrollX, data, onItemPress}: TabsProps) => {
    const containerRef = React.useRef<View>(null);

    const [measures, setMeasures] = React.useState<MeasuresType[]>([]);

    const getColors = React.useCallback((index: number) => {
      if (measures.length > 0) {
        const inputRange = data.map((_, i) => i*width);
        const outputRange = data.map((_, i) => i == index ? 'black' : '#818C99');
        return scrollX.interpolate({
          inputRange,
          outputRange
        });
      }
      return 'white'
    }, [measures]);

    React.useEffect(() => {
      let m: MeasuresType[] = [];
      data.forEach(item => {
        if (containerRef.current != null) {
          item.ref.current?.measureLayout(
            containerRef.current,
            (x, y, width, height) => {
              m.push({
                x, y, height, width
              })

              if (m.length == data.length) {
                setMeasures(m);
              }
            });
        }
      })
    }, [data]);

    return (
      <View>
        <View
          ref={containerRef}
          style={{
            width,
            flexDirection: 'row',
            justifyContent: 'space-evenly',
          }}>
          {data.map((item: TabsType, index) => {
            return (
              <Tab
                color={getColors(index)}
                key={item.key}
                item={item}
                ref={item.ref}
                onItemPress={() => {
                  onItemPress(index);
                }}
              />
            );
          })}
        </View>
        {measures.length > 0 && <Indicator measures={measures} scrollX={scrollX}/>}
      </View>
    );
  }

  const ref = React.useRef<Animated.FlatList<TabsType>>(null);
  const onItemPress = React.useCallback((itemIndex) => {
    ref?.current?.scrollToOffset({
      offset: itemIndex * width
    });
  });

  return (
    <View
      style={{
        flex: 1,
      }}>
      <View style={{paddingTop: 20}}>
        <Tabs data={data} scrollX={scrollX} onItemPress={onItemPress} />
      </View>
      <Animated.FlatList
        ref={ref}
        data={data}
        keyExtractor={item => item.key}
        viewabilityConfig={{viewAreaCoveragePercentThreshold: 100}}
        pagingEnabled
        horizontal
        showsHorizontalScrollIndicator={false}
        onScroll={Animated.event(
          [{nativeEvent: {contentOffset: {x: scrollX}}}],
          {useNativeDriver: false},
        )}
        renderItem={({item}) => {
          return (
            <View
              style={{
                width,
              }}>
              {item.tab}
            </View>
          );
        }}
      />
    </View>
  );
}
