import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {CustomerModel} from "../shared/customer-orders.model";
import {ICustomer} from "../shared/interfaces";
import {Observable} from "rxjs";
import {CustomerService} from "./customer.service";
import {map} from "rxjs/operators";

@Injectable()
export class CustomerResolverService implements Resolve<ICustomer | CustomerModel> {

  constructor(private customerService: CustomerService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ICustomer | CustomerModel> | Promise<ICustomer | CustomerModel> | ICustomer | CustomerModel {
    return this.customerService.getCustomer(route.params['id']).pipe(
      map(customer => new CustomerModel(customer.id, customer))
    );
  }
}
