import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

import {Observable, throwError} from 'rxjs';
import {catchError, map} from 'rxjs/operators';

import {ICustomer} from '../shared/interfaces';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private url: string = 'api/customersservice/customers/';

  constructor(private http: HttpClient) {
  }

  getCustomersSummary(): Observable<ICustomer[]> {
    return this.http.get<ICustomer[]>(this.url)
      .pipe(
        catchError(this.handleError)
      );
  }

  searchCustomersByKeyword(keyword: string): Observable<ICustomer[]> {
    const url = this.url + 'search?email=' + keyword;
    return this.http.get<ICustomer[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  getCustomer(id: number): Observable<ICustomer> {
    return this.http.get<ICustomer>(this.url + id)
      .pipe(
        catchError(this.handleError),
        map(result => result)
      );
  }

  updateCustomer(customer: ICustomer): Observable<boolean> {
    return this.http.put(this.url + customer.id, customer)
      .pipe(
        catchError(this.handleError),
        map((result: boolean) => result)
      );
  }

  addCustomer(customer: ICustomer): Observable<ICustomer> {
    return this.http.post<ICustomer>(this.url, customer)
      .pipe(
        catchError(this.handleError),
      );
  }

  handleError(error: any) {
    console.error(error);
    return throwError(error || 'Server error');
  }

}
