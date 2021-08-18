export interface ICustomer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  address: string;
  city: string;
  state: IState;
  zip: number;
  gender: string;
  latitude: number;
  longitude: number;
  orderCount?: number;
  orders?: IOrder[];
  ordersTotal?: number;
}

export interface IState {
  abbreviation: string;
  name: string;
}

export interface IOrder {
  id: number;
  product: string;
  price: number;
  quantity: number;
  status: string;
  orderTotal?: number;
}

export interface ICustomerOrders {
  customer: Partial<ICustomer>,
  orders: IOrder[];
}
