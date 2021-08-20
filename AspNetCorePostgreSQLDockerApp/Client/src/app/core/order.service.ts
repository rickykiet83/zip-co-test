import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable, throwError} from "rxjs";
import {ICustomerCreateOrders, ICustomerOrders, IOrder} from "../shared/interfaces";
import {catchError, map} from "rxjs/operators";

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
    return this.http.post(url, orders)
      .pipe(
        catchError(this.handleError),
        map(result => result !== null)
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
