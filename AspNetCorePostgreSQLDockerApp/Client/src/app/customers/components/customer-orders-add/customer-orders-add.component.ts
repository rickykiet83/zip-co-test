import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {CustomerModel, CustomerOrdersModel} from "../../../shared/customer-orders.model";
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ICustomerCreateOrders, IOrder} from "../../../shared/interfaces";
import {OrderService} from "../../../core/order.service";
import {statusList} from "../../../../common/statusList";

@Component({
  selector: 'app-customer-orders-add',
  templateUrl: './customer-orders-add.component.html',
  styleUrls: ['./customer-orders-add.component.css']
})
export class CustomerOrdersAddComponent implements OnInit {
  customerModel: CustomerModel = new CustomerModel(null);
  statusList = statusList;

  orderData: IOrder[] = [];
  serverError = false;
  errorMessages: any[];
  addOrderSuccess = false;

  form: FormGroup = this.fb.group({
    orders: this.fb.array([])
  });
  orderForm: FormGroup;

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private orderService: OrderService) {
    this.initForm();
  }

  ngOnInit(): void {
    this.customerModel = this.route.snapshot.data['customer'];
    this.addOrder();
  }

  get orders(): FormArray {
    return this.form.controls['orders'] as FormArray;
  }

  initForm(): FormGroup {
    return this.orderForm = this.fb.group({
      product: [null, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(150),
      ]],
      price: [0, Validators.required],
      quantity: [null, Validators.required],
      status: ['InProgress', Validators.required],
      customerId: [{value: this.customerModel.id, disabled: true}, Validators.required]
    });
  }

  addOrder() {
    this.addOrderSuccess = false;
    this.serverError = false;

    const orderForm = this.initForm();
    this.orders.push(orderForm);
  }

  removeOrder(idx: number) {
    if (this.canRemoveOrder)
      this.orders.removeAt(idx);
  }

  get canRemoveOrder(): boolean {
    return this.orders.length > 1;
  }

  onSubmitForm() {
    this.serverError = false;
    if (this.isFormValid) {
      const customerOrders: ICustomerCreateOrders = {
        customerId: this.customerModel.id,
        orders: this.orders.getRawValue() as IOrder[]
      };
      this.orderService.createOrders(customerOrders.customerId, customerOrders)
        .subscribe(result => {
          this.addOrderSuccess = result;
          this.orders.clear();
        },
          error => {
            this.serverError = true;
            this.errorMessages = error.error.errors;
          }
        );
    }
  }

  get isFormValid(): boolean {
    return this.orders.length > 0
     && (this.customerModel.id !== null && this.customerModel.id > 0)
      && this.totalInprogressStatus <= 4
      && this.orders.valid
      ;
  }

  get totalInprogressStatus(): number {
    const orders = this.orders.getRawValue() as IOrder[];
    return orders.filter(o => o.status === 'InProgress')?.length || 0;
  }

}
