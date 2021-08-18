import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {CustomerModel, CustomerOrdersModel} from "../shared/customer-orders.model";
import {ICustomer} from "../shared/interfaces";
import {Observable} from "rxjs";
import {CustomerService} from "./customer.service";

@Injectable()
export class CustomerResolverService implements Resolve<ICustomer | CustomerModel> {

  constructor(private customerService: CustomerService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ICustomer | CustomerModel> | Promise<ICustomer | CustomerModel> | ICustomer | CustomerModel {
    return this.customerService.getCustomer(route.params['id']);
  }
}
