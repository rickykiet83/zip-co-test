import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {OrderService} from "../../../core/order.service";
import {OrderModel} from "../../../shared/customer-orders.model";
import {filter, switchMap, take, tap} from "rxjs/operators";
import {statusList} from "../../../../common/statusList";
import {IOrder} from "../../../shared/interfaces";

@Component({
  selector: 'app-customer-orders-edit',
  templateUrl: './customer-orders-edit.component.html',
  styleUrls: ['./customer-orders-edit.component.css']
})
export class CustomerOrdersEditComponent implements OnInit {
  orderModel: OrderModel;
  orderId: number;
  customerId: number;
  orderForm: FormGroup;
  serverError = false;
  errorMessages: any[];
  statusList = statusList;
  updateOrderSuccess = false;

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private orderService: OrderService) {

  }

  ngOnInit(): void {
    this.route.params.pipe(
      filter(params => !!params),
      tap(params => {
        this.customerId = params['customerId'];
        this.orderId = params['id'];
      }),
      switchMap(params => [
        this.getOrder()
      ])
    ).subscribe();
  }

  getOrder() {
    if (this.customerId && this.orderId)
      this.orderService.getOrder(this.customerId, this.orderId)
        .pipe(
          tap((order) => this.buildForm(order))
        )
        .subscribe(order => this.orderModel = order, error => {
          this.serverError = true;
          this.errorMessages = error.message
        });
  }

  buildForm(order: OrderModel) {
    this.orderForm = this.fb.group({
      product: [order.product, [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(150),
      ]],
      price: [order.price, Validators.required],
      quantity: [order.quantity, Validators.required],
      status: [order.status, Validators.required],
      customerId: [{value: order.customerId, disabled: true}, Validators.required],
      id: [{value: order.id, disabled: true}, Validators.required]
    });

  }

  saveOrder() {
    if (this.orderForm.valid) {
      const order: IOrder = this.orderForm.getRawValue();
      this.orderService.updateOrder(order).pipe(
        filter(result => result === true)
      ).subscribe(() => {
        this.updateOrderSuccess = true;
        this.orderForm.disable();
      });
    }

  }


}
