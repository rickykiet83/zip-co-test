import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../../core/order.service";
import {Observable} from "rxjs";
import {IOrder} from "../../../shared/interfaces";

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.css']
})
export class CustomerOrdersComponent implements OnInit {
  orders$: Observable<IOrder[]>
  constructor(private orderService: OrderService) {
    this.orders$ = this.orderService.getAllOrders(1);
  }

  ngOnInit(): void {
    this.orders$.subscribe();
  }

}
