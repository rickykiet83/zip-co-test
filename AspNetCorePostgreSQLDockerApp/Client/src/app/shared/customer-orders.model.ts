import {ICustomer, ICustomerOrders, IOrder, IState} from "./interfaces";
import {genderList} from "../../common/genderList";

export class CustomerOrdersModel implements ICustomerOrders {
  customer: Partial<ICustomer> = null;
  orders: IOrder[] = [];

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

  getTotalSales(): number {
    return this.OrderModels.reduce((a, b) => a + b.orderTotal, 0);
  }
}

export class CustomerModel implements ICustomer {
  address: string = '';
  city: string = '';
  email: string = '';
  firstName: string = '';
  gender: string = genderList[0];
  lastName: string = '';
  latitude: number = 0;
  longitude: number = 0;
  state: IState;
  zip: number = 0;
  constructor(public id: number, data?: Partial<ICustomer>) {
    this.address = data?.address || '';
    this.city = data?.city || '';
    this.email = data?.email || '';
    this.gender = data?.gender || genderList[0];
    this.firstName = data?.firstName || '';
    this.lastName = data?.lastName || '';
    this.latitude = data?.latitude || 0;
    this.longitude = data?.longitude || 0;
    this.state = data?.state || null;
    this.zip = data?.zip || 0;
  }

  get fullName(): string {
    return this.firstName + ' ' + this.lastName;
  }

  toJSON(): ICustomer {
    return {
      id: this.id,
      email: this.email,
      firstName: this.firstName,
      lastName: this.lastName,
      latitude: this.latitude,
      longitude: this.longitude,
      address: this.address,
      city: this.city,
      state: this.state,
      zip: this.zip,
      gender: this.gender,
    }
  }
}

export class OrderModel implements IOrder {
  price: number;
  product: string;
  quantity: number;
  status: string;
  customerId: number;
  customer?: ICustomer;
  constructor(public id: number, data?: IOrder) {
    this.price = data?.price || 0;
    this.product = data?.product || '';
    this.quantity = data?.quantity || 0;
    this.status = data?.status || '';
    this.customerId = data?.customerId || null;
    this.customer = data?.customer || null;
  }

  get CustomerModel() {
    return new CustomerModel(this.customer.id, this.customer);
  }

  get orderTotal(): number {
    return this.quantity * this.price;
  }
}
