import {TokenService} from '../services/TokenService.ts';
import {CategoryService} from '../services/CategoryService.ts';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {ObjectGraph} from '../di/ObjectGraph.ts';
import {Category} from './DomainTypes.ts';
import {CompanyService} from '../services/CompanyService.ts';
import {OrderRequestService} from '../services/OrderRequestService.ts';
import {OrderRequestPopup} from './ComponentTypes.ts';

export type StackProps = {
  Login: {tokenService: TokenService}
  Loading: undefined
  Map: {
    categoryId: number
    categories: Category[]
    companyService: CompanyService
  }
  Tab: {graph: ObjectGraph}
  CreateOrderRequestScreen: {
    categories: Category[]
    categoryIndex: number
    orderRequestService: OrderRequestService
    onGoBack: (orderRequest: OrderRequestPopup) => Promise<void>
  }
};

export type ClientTabProps = {
  Categories: {categoryService: CategoryService}
  OrderRequests: undefined
  Chat: undefined
  Account: undefined
};

export type LoginScreenProps = NativeStackScreenProps<StackProps, 'Login'>;

export type OrderRequestsScreenProps = BottomTabScreenProps<ClientTabProps, 'OrderRequests'>;

export type ChatScreenProps = BottomTabScreenProps<ClientTabProps, 'Chat'>;

export type CategoriesScreenProps = BottomTabScreenProps<ClientTabProps, 'Categories'>;

export type AccountScreenProps = BottomTabScreenProps<ClientTabProps, 'Account'>;

export type TabScreenProps = NativeStackScreenProps<StackProps, 'Tab'>;

export type MapScreenProps = NativeStackScreenProps<StackProps, 'Map'>;

export type CreateOrderRequestScreenProps = NativeStackScreenProps<StackProps, 'CreateOrderRequestScreen'>;
