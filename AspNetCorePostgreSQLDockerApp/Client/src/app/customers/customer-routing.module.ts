import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CustomerComponent} from "./customer.component";
import {CustomersComponent} from "./components/customers/customers.component";


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
      }
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
