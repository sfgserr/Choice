import {UserService} from '../services/UserService.ts';
import {CategoriesService} from '../services/CategoriesService.ts';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';

export type StackProps = {
  Login: {userService: UserService};
  Loading: undefined;
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
