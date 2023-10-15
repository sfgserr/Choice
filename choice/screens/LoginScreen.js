import React, {useState, useRef, useEffect } from 'react';
import {
    findNodeHandle,
    Text,
    View,
    Image,
    SafeAreaView,
    TouchableOpacity,
    Animated,
    FlatList,
    Dimensions,
    Alert,
} from 'react-native';
import tw from 'tailwind-react-native-classnames';
import LoginByEmail from '../components/LoginByEmail';
import LoginByPhone from '../components/LoginByPhone';

const getNameByTitle = (title) => {
  if (title == 'byEmail') {
    return "E-mail"
  }
  else {
    return "Телефон"
  } 
};

const {width} = Dimensions.get('screen');

const Tab = React.forwardRef(({item, onItemPress}, ref) => {
  const [isActive, setActive] = useState(false);

  const onFocus = () => setActive(true);
  const onBlur = () => setActive(false);
  return (
    <TouchableOpacity 
      onPress={onItemPress}
      onFocus={onFocus}
      onBlur={onBlur}>
      <View ref={ref}>
        <Text style={[tw`text-base font-medium`, {color: isActive ? '#000' : '#818C99'}]}>{item.title}</Text>
      </View>
    </TouchableOpacity>
  );
});

const Indicator = ({measures, scrollX}) => {
  const inputRange = data.map((_, i) => i * width);
  const translateX = scrollX.interpolate({
    inputRange,
    outputRange: measures.map((measure) => measure.x+measure.width/2-75),
  });

  return (
    <Animated.View 
      style={{
        backgroundColor: '#3F8AE0',
        height: 3,
        width: 150,
        left: 0,
        borderRadius: 5,
        transform: [{
          translateX
        }]
      }}/>
  );
}

const Tabs = ({scrollX, data, onItemPress}) => {
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
            height,
          });

          if (m.length === data.length) {
            setMeasures(m);
          }
      });
    });
  }, []);

  return <View>
    <View style={tw`flex-row flex-1 justify-evenly`}
      ref={containerRef}>
      {data.map((item, index) => {
        return (
          <Tab 
            key={item.key}
            item={item}
            ref={item.ref}
            onItemPress={() => onItemPress(index)}/>
        );
      })}
    </View>
    {measures.length > 0 && (
      <Indicator measures={measures} scrollX={scrollX}/>
    )}
  </View>
};

const views = {
  byEmail: <LoginByEmail/>,
  byPhone: <LoginByPhone/>,
};

const data = Object.keys(views).map((i) => ({
  key: i,
  title: getNameByTitle(i),
  view: views[i],
  ref: React.createRef(),
}));

export default function LoginScreen() {
    const scrollX = React.useRef(new Animated.Value(0)).current;
    const ref = React.useRef();
    const onItemPress = React.useCallback(itemIndex => {
      ref.current?.scrollToOffset({
        offset: itemIndex * width,
      })
    });

    return (
        <SafeAreaView>
          <View>
             <View style={[{flex: 1, justifyContent: 'center', alignItems: 'center'}, tw`pt-5`]}>
              <Image source={require('../resources/images/choice.png')}
                     style={{resizeMode: 'contain'}}/>
                <Text style={[tw`text-2xl font-semibold`, {color:'#313131'}]}>
                  В Ы Б О Р
                </Text>
                <View style={[tw`pt-1`, {flex: 1, justifyContent: 'center', alignItems: 'center'}]}>
                  <Text style={[tw`text-base font-normal`, {color: '#9C9C9C'}]}>
                    Приложение для выбора
                  </Text>
                  <Text style={[tw`text-base font-normal`, {color: '#9C9C9C'}]}>
                    лучших условий
                  </Text>
                </View>
             </View>
             <View style={tw`pt-12 items-center flex-row flex-1`}>
                <View style={tw`pl-7`}>
                  <Text style={[tw`text-2xl font-bold`, {color: '#313131'}]}>
                    Авторизация
                  </Text>
                </View>
                <View style={tw`flex-row-reverse flex-1 items-center`}>
                    <TouchableOpacity style={tw`pr-7`}>
                      <Text style={[{color: '#2D81E0'}, tw`text-base font-normal`]}>
                        Создать аккаунт
                      </Text>
                    </TouchableOpacity>
                </View>
             </View>
             <View style={tw`pt-5`}>
              <Tabs scrollX={scrollX} data={data} onItemPress={onItemPress}/>
             </View>
             <Animated.FlatList
              ref={ref}
              data={data}
              keyExtractor={item => item.key}
              horizontal
              showsHorizontalScrollIndicator={false}
              pagingEnabled
              bounces={false}
              onScroll={Animated.event(
                [{nativeEvent: {contentOffset: {x: scrollX}}}],
                {useNativeDriver: false},
              )}
              renderItem={({item}) => {
                return <View style={[tw``, {width: width}]}>
                  {item.view}
                </View>
              }}>

             </Animated.FlatList>
          </View>
        </SafeAreaView>
    );
}