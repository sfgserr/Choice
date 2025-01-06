import {TokenService} from '../services/auth/TokenService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {ObjectGraph} from '../di/ObjectGraph.ts';
import {Category} from './DomainTypes.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';
import {OrderRequest} from './DomainTypes.ts';
import {ClientService} from '../services/domain/ClientService.ts';

export type StackProps = {
  Login: {tokenService: TokenService};
  Loading: undefined;
  Map: {
    categoryId: number;
    categories: Category[];
    companyService: CompanyService;
  };
  Tab: {graph: ObjectGraph};
  CreateOrderRequest: {
    categories: Category[];
    categoryIndex: number;
    orderRequestService: OrderRequestService;
    onGoBack: (orderRequest: OrderRequest) => Promise<void>;
  };
  EditOrderRequest: {
    orderRequestId: string;
    orderRequestService: OrderRequestService;
    categoryService: CategoryService;
  };
  RegisterClient: {clientService: ClientService};
  RegisterCompany: {companyService: CompanyService};
};

export type ClientTabProps = {
  Categories: {categoryService: CategoryService}
  OrderRequests: {orderRequestService: OrderRequestService, categoryService: CategoryService}
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

export type CreateOrderRequestScreenProps = NativeStackScreenProps<StackProps, 'CreateOrderRequest'>;

export type EditOrderRequestScreenProps = NativeStackScreenProps<StackProps, 'EditOrderRequest'>;

export type RegisterClientScreenProps = NativeStackScreenProps<StackProps, 'RegisterClient'>;

export type RegisterCompanyScreenProps = NativeStackScreenProps<StackProps, 'RegisterCompany'>;
