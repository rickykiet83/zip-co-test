import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CustomerModel} from "../../../shared/customer-orders.model";

@Component({
  selector: 'app-customer-orders-add',
  templateUrl: './customer-orders-add.component.html',
  styleUrls: ['./customer-orders-add.component.css']
})
export class CustomerOrdersAddComponent implements OnInit {
  customer: CustomerModel = new CustomerModel(null);
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.customer = this.route.snapshot.data['customer'];
    console.log(this.customer);
  }

}
