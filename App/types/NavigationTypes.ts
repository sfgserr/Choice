import {NativeStackScreenProps} from '@react-navigation/native-stack';
import {BottomTabScreenProps} from '@react-navigation/bottom-tabs';
import {Category, OrderRequestRadius, Review} from './DomainTypes.ts';
import {OrderRequest} from './DomainTypes.ts';
import {ImageBoxObject, MinioBlob} from "../components/ImageBox.tsx";

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
  ImageView: {uri: string};
  ChangePassword: {};
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
  ChangePassword: {};
}

export type UserStackProps = {
  FillData: undefined;
}

export type UnsubscribeStackProps = {
  SubscriptionPlans: undefined;
  PaySubscription: {price: number};
}

export type AdminStackProps = {
  Panel: undefined;
  EditClient: {clientId: string};
  EditCompany: {companyId: string};
  EditCategory: {
    category: Category;
  };
  CreateCategory: undefined;
  ClientReviews: {clientId: string};
  EditReview: {review: Review};
};

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

export type CompanyImageViewScreenProps = NativeStackScreenProps<CompanyStackProps, 'ImageView'>;

export type ClientImageViewScreenProps = NativeStackScreenProps<ClientStackProps, 'ImageView'>;

export type CreateOrderResponseScreenProps = NativeStackScreenProps<CompanyStackProps, 'CreateOrderResponse'>;

export type CompanyChatScreenProps = NativeStackScreenProps<CompanyStackProps, 'Chat'>;

export type ClientChatScreenProps = NativeStackScreenProps<ClientStackProps, 'Chat'>;

export type ClientChangePasswordScreenProps = NativeStackScreenProps<ClientStackProps, 'ChangePassword'>;

export type CompanyChangePasswordScreenProps = NativeStackScreenProps<CompanyStackProps, 'ChangePassword'>;

export type ChangePasswordScreenProps = { navigation: any };

export type PanelScreenProps = NativeStackScreenProps<AdminStackProps, 'Panel'>;

export type EditCategoryScreenProps = NativeStackScreenProps<AdminStackProps, 'EditCategory'>;

export type CreateCategoryScreenProps = NativeStackScreenProps<AdminStackProps, 'CreateCategory'>;

export type EditClientScreenProps = NativeStackScreenProps<AdminStackProps, 'EditClient'>;

export type EditCompanyScreenProps = NativeStackScreenProps<AdminStackProps, 'EditCompany'>;

export type ClientReviewsScreenProps = NativeStackScreenProps<AdminStackProps, 'ClientReviews'>;

export type EditReviewScreenProps = NativeStackScreenProps<AdminStackProps, 'EditReview'>;

export type AboutScreenProps = {
  next: (description: string, photoUris: ImageBoxObject[], prepaymentAvailable: boolean) => Promise<void>
  categoriesTitle: string
  onChevronPressed: () => void
};
