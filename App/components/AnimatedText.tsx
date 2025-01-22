import {TouchableOpacity} from 'react-native-gesture-handler';
import Animated, {useAnimatedStyle, withTiming} from 'react-native-reanimated';


export default function AnimatedText({ item, index, selectedIndex, setSelectedIndex }: {
  item: { title: string; seconds: number };
  index: number;
  selectedIndex: number;
  setSelectedIndex: (index: number) => void;
}){
  const delta = Math.abs(selectedIndex-index);
  const decayFactor = 0.4;
  const duration = 500;

  const animatedStyle = useAnimatedStyle(() => ({
    opacity: withTiming(selectedIndex == index ? 1 : Math.max(0.1, 1 - delta * decayFactor), {duration}),
  }));

  return (
    <TouchableOpacity
      onPress={() => setSelectedIndex(index)}
      activeOpacity={1}
      style={{width: 'auto', paddingBottom: 20}}>
      <Animated.Text style={[animatedStyle, {alignSelf: 'center', fontSize: 20}]}>
        {item.title}
      </Animated.Text>
    </TouchableOpacity>
  );
};
