import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CustomerOrdersModel} from "../../../shared/customer-orders.model";

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrls: ['./customer-orders.component.css']
})
export class CustomerOrdersComponent implements OnInit {
  customerOrders: CustomerOrdersModel;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.customerOrders = this.route.snapshot.data['orders'];
  }
}
