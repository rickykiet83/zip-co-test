import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CustomersComponent} from "./components/customers/customers.component";
import { CustomerComponent } from './customer.component';
import {CustomerRoutingModule} from "./customer-routing.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [CustomersComponent, CustomerComponent],
  imports: [
    CommonModule,
    FormsModule, ReactiveFormsModule,
    CustomerRoutingModule
  ]
})
export class CustomersModule { }
