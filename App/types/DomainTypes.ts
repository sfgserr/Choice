export type Category = {
  categoryId: number,
  title: string,
  iconUri: string
};

export type CompanyMapMarker = {
  id: string,
  iconUri: string,
  averageGrade: number,
  latitude: string,
  longitude: string
};

export type OrderStatus = 'Active' | 'Canceled' | 'Finished';

export type OrderRequest = {
  id: string
  description: string
  creationDate: Date
  categoryTitle: string
  orderStatus: OrderStatus
};

export type OrderRequestDetails = {
  id: string
  status: OrderStatus
  categoryId: number
  description: string
  toKnowPrice: boolean
  toKnowDeadline: boolean
  toKnowEnrollmentDate: boolean
  photoUris: string[]
  distance: number
  creationDate: Date
};

export type OrderRequestRadius = {
  id: string
  categoryId: number
  description: string
  photoUris: string[]
  clientCreatedId: string
  iconUri: string
  clientName: string
  latitude: number
  longitude: number
  reviewsCount: number
  averageGrade: number
  distance: number
};

export type SubscriptionPayment = {
  id: string
  period: string
  cost: number
  payerId: string
  expirationDate: Date
  status: string
};

export type CompanyOrderRequest = {
  id: string
  toKnowPrice: boolean
  toKnowDeadline: boolean
  toKnowEnrollmentDate: boolean
};

type SocialMedia = {
  url: string
  platform: string
}

export type CompanyInfo = {
  id: string
  name: string
  email: string
  phoneNumber: string
  iconUri: string
  street: string
  city: string
  description: string
  photoUris: string[]
  socialMedias: SocialMedia[]
  reviewsCount: number
  averageGrade: number
  distance: number
}

export type ChatMessages = {
  user: ChatUser
  messages: Message[]
}

export type ChatUser = {
  id: string
  iconUri: string
  name: string
}

export type Message = {
  id: string
  fromUserId: string
  toUserId: string
  body: string | null
  type: string
  isRead: boolean
  orderResponseId: string | null
  creationDate: string
  enrollmentDate: Date | null
  isActive: boolean | null
}

export type OrderResponse = {
  id: string
  requestId: string
  clientId: string
  companyId: string
  price: number
  deadline: number
  enrollmentDate: Date | null
  prepayment: number
  status: string
  isEnrolled: boolean
  isPaid: boolean
  isEnrollmentDateConfirmed: boolean
  isActive: boolean
}

export type Chat = {
  userId: string
  iconUri: string
  userName: string
  lastMessageId: string
  lastMessage: string | null
  lastMessageIsRead: boolean
  lastMessageCreationDate: string
  lastMessageUserSenderId: string
}
