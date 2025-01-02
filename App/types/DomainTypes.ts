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
