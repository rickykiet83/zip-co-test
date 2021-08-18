import {Component, OnInit} from '@angular/core';
import {ICustomer} from "./../../../shared/interfaces";
import {CustomerService} from "../../../core/customer.service";


@Component({
  selector: 'app-customers',
  templateUrl: 'customers.component.html'
})
export class CustomersComponent implements OnInit {

  customers: ICustomer[] = [];
  editId: number = 0;
  errorMessage: string;
  editViewEnabled = false;

  constructor(private customerService: CustomerService) {
  }

  ngOnInit() {
    this.customerService.getCustomersSummary()
      .subscribe((data: ICustomer[]) => this.customers = data);
  }

  save(customer: ICustomer) {
    this.customerService.updateCustomer(customer)
      .subscribe((status: boolean) => {
        if (status) {
          this.editId = 0;
        } else {
          this.errorMessage = 'Unable to save customer';
        }
      })
  }

}
