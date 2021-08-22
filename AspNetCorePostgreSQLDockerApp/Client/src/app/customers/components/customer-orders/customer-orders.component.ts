import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CustomerOrdersModel, OrderModel} from "../../../shared/customer-orders.model";
import {OrderService} from "../../../core/order.service";
import * as _ from 'lodash';
import {IOrder} from "../../../shared/interfaces";

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.css']
})
export class CustomerOrdersComponent implements OnInit {
  customerOrders: CustomerOrdersModel;
  constructor(private route: ActivatedRoute, private orderService: OrderService) {}

  ngOnInit(): void {
    this.customerOrders = this.route.snapshot.data['orders'];
    this.sortOrders(this.customerOrders.orders);
  }

  onCancelOrder(orderId: number) {
    this.orderService.cancelOrder(this.customerOrders.customer.id, orderId)
      .subscribe(result => {
        const index = this.customerOrders.orders.findIndex(order => order.id === result.id);
        this.customerOrders.orders.splice(index, 1);
        const orders = [
          ...this.customerOrders.orders,
          result
        ];
        this.customerOrders.orders = this.sortOrders(orders);
      });
  }

  sortOrders(orders: OrderModel[] | IOrder[]): OrderModel[] {
    return this.customerOrders.orders = _.sortBy(orders, ['id']);
  }

}
