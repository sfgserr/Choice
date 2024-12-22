import {TokenService} from '../services/TokenService.ts';
import {CategoriesService} from '../services/CategoriesService.ts';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {ObjectGraph} from '../di/ObjectGraph.ts';
import {Category} from './DomainTypes.ts';

export type StackProps = {
  Login: {tokenService: TokenService};
  Loading: undefined;
  Map: {category: Category};
  Tab: {graph: ObjectGraph};
};

export type ClientTabProps = {
  Categories: {categoriesService: CategoriesService};
  OrderRequests: undefined;
  Chat: undefined;
  Account: undefined;
};

export type LoginScreenProps = NativeStackScreenProps<StackProps, 'Login'>;

export type OrderRequestsScreenProps = BottomTabScreenProps<ClientTabProps, 'OrderRequests'>;

export type ChatScreenProps = BottomTabScreenProps<ClientTabProps, 'Chat'>;

export type CategoriesScreenProps = BottomTabScreenProps<ClientTabProps, 'Categories'>;

export type AccountScreenProps = BottomTabScreenProps<ClientTabProps, 'Account'>;

export type TabScreenProps = NativeStackScreenProps<StackProps, 'Tab'>;

export type MapScreenProps = NativeStackScreenProps<StackProps, 'Map'>;
