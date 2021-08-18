import {ICustomer, ICustomerOrders, IOrder, IState} from "./interfaces";

export class CustomerOrdersModel implements ICustomerOrders {
  customer: Partial<ICustomer>;
  orders: IOrder[];

  constructor(data?: ICustomerOrders) {
    this.customer = data?.customer || null;
    this.orders = data?.orders || [];
  }

  get OrderModels(): OrderModel[] {
    return this.orders.map(order => new OrderModel(order?.id, order));
  }

  get CustomerModel(): CustomerModel {
    return new CustomerModel(this.customer?.id, this.customer);
  }
}

export class CustomerModel implements ICustomer {
  address: string;
  city: string;
  email: string;
  firstName: string;
  gender: string;
  lastName: string;
  latitude: number;
  longitude: number;
  state: IState;
  zip: number;
  constructor(public id: number, data?: Partial<ICustomer>) {
    this.address = data?.address || '';
    this.city = data?.city || '';
    this.email = data?.email || '';
    this.gender = data?.gender || '';
    this.firstName = data?.firstName || '';
    this.lastName = data?.lastName || '';
    this.latitude = data?.latitude || 0;
    this.longitude = data?.longitude || 0;
    this.state = data?.state || null;
    this.zip = data?.zip || null;
  }

  get fullName(): string {
    return this.firstName + ' ' + this.lastName;
  }

}

export class OrderModel implements IOrder {
  price: number;
  product: string;
  quantity: number;
  status: string;
  constructor(public id: number, data?: IOrder) {
    this.price = data?.price || 0;
    this.product = data?.product || '';
    this.quantity = data?.quantity || 0;
    this.status = data?.status || '';
  }

  get orderTotal(): number {
    return this.quantity * this.price;
  }
}
