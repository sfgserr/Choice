import * as React from 'react';
import {
  Animated, Dimensions,
  Text, TouchableOpacity,
  View, ViewToken,
} from 'react-native';
import {ForwardedRef} from 'react';

type Tab = {
  element: React.JSX.Element,
  title: string
}

export type TabBarProps = {
  tabs: Tab[]
}

export default function TabBar({tabs}: TabBarProps) {
  const {width, height} = Dimensions.get('screen');

  const [currentIndex, setCurrentIndex] = React.useState(0);
  const onViewableItemsChanged = React.useCallback((info: { viewableItems: ViewToken<TabsType>[], changed: ViewToken<TabsType>[] }) => {
    if (info.viewableItems.length > 0)
      setCurrentIndex(+info.viewableItems[info.viewableItems.length-1].item.key);
  });

  type TabsType = {
    name: string,
    key: string,
    tab: React.JSX.Element,
    ref: React.RefObject<View>
  }

  type TabProps = {
    item: TabsType,
    onItemPress: Function,
    isPressed: boolean
  }

  type TabsProps = {
    scrollX: Animated.Value,
    data: TabsType[],
    onItemPress: Function
  }

  type MeasuresType = {
    x: number,
    y: number,
    height: number,
    width: number
  }

  type IndicatorProps = {
    measures: MeasuresType[],
    scrollX: Animated.Value
  }

  const data = Object.keys(tabs).map<TabsType>((v, i) => ({
    name: tabs[i].title,
    key: v,
    tab: tabs[i].element,
    ref: React.createRef<View>()
  }));

  const scrollX = React.useRef(new Animated.Value(0)).current;

  const Tab = React.forwardRef(({item, onItemPress, isPressed}: TabProps, ref: ForwardedRef<View>) => {

    return (
      <TouchableOpacity
        ref={ref}
        onPress={() => onItemPress()}>
        <Text
          style={{
            fontWeight: isPressed ? '600' : '500',
            fontSize: 16,
            color: isPressed ? 'black' : '#818C99',
          }}>
          {item.name}
        </Text>
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
                isPressed={index == currentIndex}
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
        onViewableItemsChanged={onViewableItemsChanged}
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
