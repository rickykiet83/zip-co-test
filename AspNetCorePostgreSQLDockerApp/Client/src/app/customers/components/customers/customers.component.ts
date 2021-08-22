import {Component, OnInit} from '@angular/core';
import {ICustomer} from "./../../../shared/interfaces";
import {CustomerService} from "../../../core/customer.service";
import {ModalService} from "../../../shared/modal/modal.service";
import {CustomerModel, OrderModel} from "../../../shared/customer-orders.model";
import {genderList} from "../../../../common/genderList";
import {catchError, filter} from "rxjs/operators";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";


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
  customerAddForm: FormGroup;

  constructor(private customerService: CustomerService, private modalService: ModalService, private fb: FormBuilder) {
    this.buildForm();
  }

  ngOnInit() {
    this.customerService.getCustomersSummary()
      .subscribe((data: ICustomer[]) => this.customers = data);
  }

  buildForm() {
    this.customerAddForm = this.fb.group({
      firstName: [null, [
        Validators.required,
      ]],
      lastName: [null, Validators.required],
      email: [null, [
        Validators.required,
        Validators.email,
      ]],
      gender: [genderList[0], Validators.required],
      city: [null, Validators.required],
      address: [null, Validators.required],
    });
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
      const data = new CustomerModel(null, this.customerAddForm.value).toJSON();
      this.customerService.addCustomer(data)
        .subscribe(result => {
            this.customers = [...this.customers, result];
            this.closeModal('customer-modal-add');
            this.serverError = false;
          },
          error => {
            this.serverError = true;
            this.errorMessage = error.error.message;
          }
        );
    }

  }

  openModal(id: string) {
    this.customerModel = new CustomerModel(null);
    this.modalService.open(id);
  }

  closeModal(id: string) {
    this.serverError = false;
    this.errorMessage = '';
    this.modalService.close(id);
  }

  onKeywordChange(keyword: string) {
    this.keyword = keyword;
  }

  onSearchByKeyword() {
    this.customerService.searchCustomersByKeyword(this.keyword)
      .subscribe((data: ICustomer[]) => this.customers = data);
  }

  get totalCustomer(): number {
    return this.customers.length;
  }

}
