import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CustomerComponent} from "./customer.component";
import {CustomersComponent} from "./components/customers/customers.component";
import {CustomerOrdersComponent} from "./components/customer-orders/customer-orders.component";
import {OrderResolveService} from "../core/order.resolver";


const routes: Routes = [
  {
    path: '',
    component: CustomerComponent,
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        component: CustomersComponent
      },
      {
        path: ':id/orders',
        component: CustomerOrdersComponent,
        resolve: {
          orders: OrderResolveService
        }
      },
    ]
  }
]


@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
