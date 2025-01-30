import * as React from 'react';
import {Animated, View} from 'react-native';
import AnimatedInterpolation = Animated.AnimatedInterpolation;

export type TabsType = {
  name: string,
  key: string,
  tab: React.JSX.Element,
  ref: React.RefObject<View>
}

export type TabProps = {
  item: TabsType,
  onItemPress: Function,
  color: string | AnimatedInterpolation<string | number>
}

export type TabsProps = {
  scrollX: Animated.Value,
  data: TabsType[],
  onItemPress: Function
}

export type MeasuresType = {
  x: number,
  y: number,
  height: number,
  width: number
}

export type IndicatorProps = {
  measures: MeasuresType[],
  scrollX: Animated.Value
}
