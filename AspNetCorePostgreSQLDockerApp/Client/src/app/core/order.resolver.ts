import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {IOrder} from "../shared/interfaces";
import {Observable} from "rxjs";
import {OrderService} from "./order.service";
import {OrderModel} from "../shared/order.model";
import {map} from "rxjs/operators";

@Injectable()
export class OrderResolveService implements Resolve<IOrder[] | OrderModel[]>{

  constructor(private orderService: OrderService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IOrder[] | OrderModel[]> | Promise<IOrder[] | OrderModel[]> | IOrder[] | OrderModel[] {
    return this.orderService.getAllOrders(route.params['id']).pipe(
      map(orders => orders.map(order => new OrderModel(order.id, order)))
    );
  }
}
