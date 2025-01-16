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

};

export type SubscriptionPayment = {
  id: string
  period: string
  cost: number
  payerId: string
  expirationDate: Date
  status: string
}
