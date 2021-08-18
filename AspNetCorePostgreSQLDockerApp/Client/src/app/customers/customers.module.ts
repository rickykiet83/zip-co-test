import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CustomersComponent} from "./components/customers/customers.component";
import {CustomerComponent} from './customer.component';
import {CustomerRoutingModule} from "./customer-routing.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { CustomerOrdersComponent } from './components/customer-orders/customer-orders.component';
import {OrderResolveService} from "../core/order.resolver";
import {OrderService} from "../core/order.service";

@NgModule({
  declarations: [CustomersComponent, CustomerComponent, CustomerOrdersComponent],
  imports: [
    CommonModule,
    FormsModule, ReactiveFormsModule,
    CustomerRoutingModule
  ],
  providers:[
    OrderService,
    OrderResolveService
  ]
})
export class CustomersModule {
}
