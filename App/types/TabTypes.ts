import * as React from 'react';
import {Animated, View} from 'react-native';

export type TabsType = {
  name: string,
  key: string,
  tab: React.JSX.Element,
  ref: React.RefObject<View>
}

export type TabProps = {
  item: TabsType,
  onItemPress: Function,
  isPressed: boolean
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
