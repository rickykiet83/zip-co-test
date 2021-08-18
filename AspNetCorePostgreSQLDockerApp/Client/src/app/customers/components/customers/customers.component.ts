import {Component, OnInit} from '@angular/core';
import {ICustomer} from "./../../../shared/interfaces";
import {DataService} from "./../../../core/data.service";


@Component({
  selector: 'app-customers',
  templateUrl: 'customers.component.html'
})
export class CustomersComponent implements OnInit {

  customers: ICustomer[] = [];
  editId: number = 0;
  errorMessage: string;
  editViewEnabled = false;

  constructor(private dataService: DataService) {
  }

  ngOnInit() {
    this.dataService.getCustomersSummary()
      .subscribe((data: ICustomer[]) => this.customers = data);
  }

  save(customer: ICustomer) {
    this.dataService.updateCustomer(customer)
      .subscribe((status: boolean) => {
        if (status) {
          this.editId = 0;
        } else {
          this.errorMessage = 'Unable to save customer';
        }
      })
  }

}
