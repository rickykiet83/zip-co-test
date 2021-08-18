import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../../core/order.service";
import {IOrder} from "../../../shared/interfaces";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.css']
})
export class CustomerOrdersComponent implements OnInit {
  orders: IOrder[];
  constructor(private route: ActivatedRoute, private orderService: OrderService) {
  }

  ngOnInit(): void {
    this.orders = this.route.snapshot.data['orders'];
    console.log(this.orders);
  }

}
