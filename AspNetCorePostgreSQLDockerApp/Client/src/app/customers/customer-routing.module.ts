import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CustomerComponent} from "./customer.component";
import {CustomersComponent} from "./components/customers/customers.component";
import {CustomerOrdersComponent} from "./components/customer-orders/customer-orders.component";
import {OrderResolveService} from "../core/order.resolver";
import {CustomerOrdersAddComponent} from "./components/customer-orders-add/customer-orders-add.component";
import {CustomerResolverService} from "../core/customer-resolver.service";
import {CustomerOrdersEditComponent} from "./components/customer-orders-edit/customer-orders-edit.component";


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
      {
        path: ':id/orders/add',
        component: CustomerOrdersAddComponent,
        resolve: {
          customer: CustomerResolverService
        }
      },
      {
        path: ':customerId/orders/:id/edit',
        component: CustomerOrdersEditComponent,
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
