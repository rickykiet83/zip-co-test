import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {IOrder} from "../shared/interfaces";
import {Observable} from "rxjs";
import {OrderService} from "./order.service";

@Injectable()
export class OrderResolveService implements Resolve<IOrder[]>{

  constructor(private orderService: OrderService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IOrder[]> | Promise<IOrder[]> | IOrder[] {
    return this.orderService.getAllOrders(route.params['id']);
  }
}
