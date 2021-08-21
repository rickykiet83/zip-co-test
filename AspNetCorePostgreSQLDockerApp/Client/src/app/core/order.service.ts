import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {ICustomerCreateOrders, ICustomerOrders, IOrder} from "../shared/interfaces";
import {catchError, map} from "rxjs/operators";
import {OrderModel} from "../shared/customer-orders.model";

@Injectable()
export class OrderService {
  private url: string = 'api/customers/';

  constructor(private http: HttpClient) {}

  getAllOrders(customerId: number): Observable<ICustomerOrders> {
    return this.http.get<ICustomerOrders>(this.url + `${customerId}/orders`)
      .pipe(
        catchError(this.handleError)
      );
  }

  createOrders(customerId: number, orders: ICustomerCreateOrders): Observable<boolean> {
    const url = this.url + customerId + '/orders';
    return this.http.post<ICustomerCreateOrders>(url, orders)
      .pipe(
        catchError(this.handleError),
        map(result => result && result.customerId !== null)
      );
  }

  updateOrder(order: IOrder): Observable<boolean> {
    const url = this.url + order.customerId + '/orders/' + order.id;
    return this.http.put<IOrder>(url, order)
      .pipe(
        catchError(this.handleError),
        map(result => result && result.id !== null)
      );
  }

  getOrder(customerId: number, orderId: number): Observable<OrderModel> {
    const url = this.url + customerId + '/orders/' + orderId;
    return this.http.get<IOrder>(url)
      .pipe(
        catchError(this.handleError),
        map(result => new OrderModel(result.id, result))
      );
  }


  cancelOrder(customerId: number, orderId: number): Observable<IOrder> {
    const url = this.url + customerId + '/orders/' + orderId + '/cancel';
    return this.http.get<IOrder>(url)
      .pipe(
        catchError(this.handleError),
      );
  }

  handleError(error: any) {
    console.error(error);
    return throwError(error || 'Server error');
  }
}
