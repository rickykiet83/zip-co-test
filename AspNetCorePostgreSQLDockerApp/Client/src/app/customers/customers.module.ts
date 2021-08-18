import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CustomersComponent} from "./components/customers/customers.component";
import { CustomerComponent } from './customer.component';
import {CustomerRoutingModule} from "./customer-routing.module";

@NgModule({
  declarations: [CustomersComponent, CustomerComponent],
  imports: [
    CommonModule,
    CustomerRoutingModule
  ]
})
export class CustomersModule { }
