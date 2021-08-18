import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CustomerModel, CustomerOrdersModel} from "../../../shared/customer-orders.model";
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ICustomerCreateOrders, IOrder} from "../../../shared/interfaces";

@Component({
  selector: 'app-customer-orders-add',
  templateUrl: './customer-orders-add.component.html',
  styleUrls: ['./customer-orders-add.component.css']
})
export class CustomerOrdersAddComponent implements OnInit {
  customerModel: CustomerModel = new CustomerModel(null);
  statusList = ['InProgress', 'Delivered', 'Cancelled'];

  orderData: IOrder[] = [];

  form: FormGroup = this.fb.group({
    orders: this.fb.array([])
  });

  constructor(private route: ActivatedRoute, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.customerModel = this.route.snapshot.data['customer'];
  }

  get orders(): FormArray {
    return this.form.controls['orders'] as FormArray;
  }

  addOrder() {

    const orderForm = this.fb.group({
      product: [null, Validators.required],
      price: ['', Validators.required],
      quantity: ['', Validators.required],
      status: ['InProgress', Validators.required],
      customerId: [{value: this.customerModel.id, disabled: true}]
    });

    this.orders.push(orderForm);
  }

  onSubmitForm() {
    if (this.isFormValid) {
      const customerOrders: ICustomerCreateOrders = {
        customerId: this.customerModel.id,
        orders: this.orders.getRawValue() as IOrder[]
      };
      console.log(customerOrders);

    }

  }

  get isFormValid(): boolean {
    return this.orders.length > 0
     && (this.customerModel.id !== null && this.customerModel.id > 0)
      && this.totalInprogressStatus <= 4;
  }

  get totalInprogressStatus(): number {
    const orders = this.orders.getRawValue() as IOrder[];
    return orders.filter(o => o.status === 'InProgress')?.length || 0;
  }

}
