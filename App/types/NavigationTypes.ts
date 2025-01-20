import {TokenService} from '../services/auth/TokenService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {ObjectGraph} from '../services/ObjectGraph.ts';
import {Category, OrderRequestRadius} from './DomainTypes.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';
import {OrderRequestService} from '../services/domain/OrderRequestService.ts';
import {OrderRequest} from './DomainTypes.ts';
import {ClientService} from '../services/domain/ClientService.ts';
import {SubscriptionPaymentService} from '../services/domain/SubscriptionPaymentService.ts';

export type StackProps = {
  Login: undefined;
  Loading: undefined;
  Map: {
    categoryId: number;
    categories: Category[];
  };
  Tab: undefined;
  CreateOrderRequest: {
    categories: Category[];
    categoryIndex: number;
    onGoBack: (orderRequest: OrderRequest) => Promise<void>;
  };
  EditOrderRequest: {
    orderRequestId: string;
  };
  RegisterClient: undefined;
  RegisterCompany: undefined;
  FillData: undefined
  SubscriptionPlans: undefined;
  PaySubscription: {price: number};
  ImageView: {uri: string};
  CreateOrderResponse: {
    orderRequest: OrderRequestRadius,
    categories: Category[]
  };
};

export type ClientTabProps = {
  Categories: undefined
  OrderRequests: undefined
  Chat: undefined
  Account: undefined
};

export type CompanyTabProps = {
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

export type CreateOrderRequestScreenProps = NativeStackScreenProps<StackProps, 'CreateOrderRequest'>;

export type EditOrderRequestScreenProps = NativeStackScreenProps<StackProps, 'EditOrderRequest'>;

export type RegisterClientScreenProps = NativeStackScreenProps<StackProps, 'RegisterClient'>;

export type RegisterCompanyScreenProps = NativeStackScreenProps<StackProps, 'RegisterCompany'>;

export type FillDataScreenProps = NativeStackScreenProps<StackProps, 'FillData'>;

export type CompanyRequestsScreenProps = BottomTabScreenProps<CompanyTabProps, 'OrderRequests'>;

export type SubscriptionScreenProps = NativeStackScreenProps<StackProps, 'SubscriptionPlans'>;

export type PaySubscriptionScreenProps = NativeStackScreenProps<StackProps, 'PaySubscription'>;

export type ImageViewScreenProps = NativeStackScreenProps<StackProps, 'ImageView'>;

export type CreateOrderResponseScreenProps = NativeStackScreenProps<StackProps, 'CreateOrderResponse'>;

export type AboutScreenProps = {
  next: (description: string, photoUris: string[], prepaymentAvailable: boolean) => Promise<void>
  categoriesTitle: string
  onChevronPressed: () => void
};
