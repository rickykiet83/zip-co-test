import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {ICustomerOrders, IOrder} from "../shared/interfaces";
import {Observable} from "rxjs";
import {OrderService} from "./order.service";
import {map} from "rxjs/operators";
import {CustomerOrdersModel} from "../shared/customer-orders.model";

@Injectable()
export class OrderResolveService implements Resolve<ICustomerOrders | CustomerOrdersModel>{

  constructor(private orderService: OrderService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ICustomerOrders | CustomerOrdersModel> | Promise<ICustomerOrders | CustomerOrdersModel> | ICustomerOrders | CustomerOrdersModel {
    return this.orderService.getAllOrders(route.params['id']).pipe(
      map(result => new CustomerOrdersModel(result))
    );
  }
}
