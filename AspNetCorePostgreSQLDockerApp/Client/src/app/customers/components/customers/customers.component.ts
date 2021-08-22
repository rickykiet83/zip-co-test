import {Component, OnInit} from '@angular/core';
import {ICustomer} from "./../../../shared/interfaces";
import {CustomerService} from "../../../core/customer.service";
import {ModalService} from "../../../shared/modal/modal.service";
import {CustomerModel} from "../../../shared/customer-orders.model";
import {genderList} from "../../../../common/genderList";
import {catchError, filter} from "rxjs/operators";


@Component({
  selector: 'app-customers',
  styleUrls: ['./customers.component.css'],
  templateUrl: 'customers.component.html'
})
export class CustomersComponent implements OnInit {
  customers: ICustomer[] = [];
  editId: number = 0;
  errorMessage: string;
  editViewEnabled = false;
  keyword = '';
  customerModel = new CustomerModel(null);
  genderList = genderList;
  serverError = false;

  constructor(private customerService: CustomerService, private modalService: ModalService) {
  }

  ngOnInit() {
    this.customerService.getCustomersSummary()
      .subscribe((data: ICustomer[]) => this.customers = data);
  }

  save(customer: ICustomer | CustomerModel) {
    if (customer.id !== null) {
      this.customerService.updateCustomer(customer)
        .subscribe((status: boolean) => {
          if (status) {
            this.editId = 0;
          } else {
            this.errorMessage = 'Unable to save customer';
          }
        })
    } else {
      const data = new CustomerModel(null, customer).toJSON();
      this.customerService.addCustomer(data)
        .pipe(
          filter(result => !!result),
        )
        .subscribe(result => {
            this.customers = [...this.customers, result];
            this.closeModal('customer-modal-add');
          },
          error => {
            this.serverError = true;
            this.errorMessage = error.error.message;
          }
        );
    }

  }

  openModal(id: string) {
    this.modalService.open(id);
  }

  closeModal(id: string) {
    this.modalService.close(id);
  }

  onKeywordChange(keyword: string) {
    this.keyword = keyword;
  }

  onSearchByKeyword() {
    this.customerService.searchCustomersByKeyword(this.keyword)
      .subscribe((data: ICustomer[]) => this.customers = data);
  }

}
