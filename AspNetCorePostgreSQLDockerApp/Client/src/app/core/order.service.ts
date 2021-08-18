import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IOrder} from "../shared/interfaces";
import {catchError} from "rxjs/operators";

@Injectable()
export class OrderService {
  private url: string = 'api/customers/';

  constructor(private http: HttpClient) {}

  getAllOrders(customerId: number): Observable<IOrder[]> {
    return this.http.get<IOrder[]>(this.url + `${customerId}/orders`)
      .pipe(
        catchError(this.handleError)
      );
  }

  handleError(error: any) {
    console.error(error);
    return Observable.throw(error.json().error || 'Server error');
  }
}
