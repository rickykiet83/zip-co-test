import {IOrder} from "./interfaces";

export class OrderModel implements IOrder {
  product: string;
  price: number;
  quantity: number;
  status: string;
  constructor(public id: number, data?: IOrder) {
    this.product = data.product || '';
    this.price = data.price || 0;
    this.quantity = data.quantity || 0;
    this.status = data.status || '';
  }

  get orderTotal(): number {
    return this.price * this.quantity || 0;
  }
}
