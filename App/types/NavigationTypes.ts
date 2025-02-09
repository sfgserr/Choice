import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {Category, OrderRequestRadius} from './DomainTypes.ts';
import {OrderRequest} from './DomainTypes.ts';

export type LoadingStackProps = {
  Loading: undefined;
};

export type SignOutStackProps = {
  Login: undefined;
  RegisterClient: undefined;
  RegisterCompany: undefined;
}

export type ClientStackProps = {
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
  Chat: {
    id: string
    onGoBack: () => void;
  };
}

export type CompanyStackProps = {
  Tab: undefined;
  ImageView: {uri: string};
  CreateOrderResponse: {
    orderRequest: OrderRequestRadius;
    categories: Category[];
  };
  Chat: {
    id: string;
    onGoBack: () => void;
  };
}

export type UserStackProps = {
  FillData: undefined;
}

export type UnsubscribeStackProps = {
  SubscriptionPlans: undefined;
  PaySubscription: {price: number};
}

export type ClientTabProps = {
  Categories: undefined;
  OrderRequests: undefined;
  Chats: undefined;
  Account: undefined;
};

export type CompanyTabProps = {
  OrderRequests: undefined;
  Chats: undefined;
  Account: undefined;
};

export type LoginScreenProps = NativeStackScreenProps<SignOutStackProps, 'Login'>;

export type OrderRequestsScreenProps = BottomTabScreenProps<ClientTabProps, 'OrderRequests'>;

export type ClientChatsScreenProps = BottomTabScreenProps<ClientTabProps, 'Chats'>;

export type CompanyChatsScreenProps = BottomTabScreenProps<ClientTabProps, 'Chats'>;

export type CategoriesScreenProps = BottomTabScreenProps<ClientTabProps, 'Categories'>;

export type AccountScreenProps = BottomTabScreenProps<ClientTabProps, 'Account'>;

export type ClientTabScreenProps = NativeStackScreenProps<ClientStackProps, 'Tab'>;

export type CompanyTabScreenProps = NativeStackScreenProps<CompanyStackProps, 'Tab'>;

export type MapScreenProps = NativeStackScreenProps<ClientStackProps, 'Map'>;

export type CreateOrderRequestScreenProps = NativeStackScreenProps<ClientStackProps, 'CreateOrderRequest'>;

export type EditOrderRequestScreenProps = NativeStackScreenProps<ClientStackProps, 'EditOrderRequest'>;

export type RegisterClientScreenProps = NativeStackScreenProps<SignOutStackProps, 'RegisterClient'>;

export type RegisterCompanyScreenProps = NativeStackScreenProps<SignOutStackProps, 'RegisterCompany'>;

export type FillDataScreenProps = NativeStackScreenProps<UserStackProps, 'FillData'>;

export type CompanyRequestsScreenProps = BottomTabScreenProps<CompanyTabProps, 'OrderRequests'>;

export type SubscriptionScreenProps = NativeStackScreenProps<UnsubscribeStackProps, 'SubscriptionPlans'>;

export type PaySubscriptionScreenProps = NativeStackScreenProps<UnsubscribeStackProps, 'PaySubscription'>;

export type ImageViewScreenProps = NativeStackScreenProps<CompanyStackProps, 'ImageView'>;

export type CreateOrderResponseScreenProps = NativeStackScreenProps<CompanyStackProps, 'CreateOrderResponse'>;

export type CompanyChatScreenProps = NativeStackScreenProps<CompanyStackProps, 'Chat'>;

export type ClientChatScreenProps = NativeStackScreenProps<ClientStackProps, 'Chat'>;

export type AboutScreenProps = {
  next: (description: string, photoUris: string[], prepaymentAvailable: boolean) => Promise<void>
  categoriesTitle: string
  onChevronPressed: () => void
};
