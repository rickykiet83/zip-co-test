import {Component, OnInit} from '@angular/core';
import {OrderService} from "../../../core/order.service";
import {ActivatedRoute} from "@angular/router";
import {OrderModel} from "../../../shared/order.model";

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.css']
})
export class CustomerOrdersComponent implements OnInit {
  orders: OrderModel[];
  constructor(private route: ActivatedRoute, private orderService: OrderService) {}

  ngOnInit(): void {
    this.orders = this.route.snapshot.data['orders'];
  }

}
