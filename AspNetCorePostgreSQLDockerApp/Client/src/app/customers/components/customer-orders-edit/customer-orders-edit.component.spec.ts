import {ComponentFixture, fakeAsync, TestBed} from '@angular/core/testing';

import {of} from "rxjs";
import {CustomersModule} from "../../customers.module";
import {RouterTestingModule} from "@angular/router/testing";
import {ActivatedRoute, Router} from "@angular/router";
import {OrderService} from "../../../core/order.service";
import {CUSTOMERS} from "../../../../common/customer-data";
import {DebugElement} from "@angular/core";
import {CustomerOrdersEditComponent} from "./customer-orders-edit.component";
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('CustomerOrdersEditComponent', () => {
  let component: CustomerOrdersEditComponent;
  let fixture: ComponentFixture<CustomerOrdersEditComponent>;
  let el: DebugElement;
  let orderService: any;
  let orderData = CUSTOMERS[0].orders[0];

  const activatedRouteMock = {
    snapshot: {
      data: {
        order: orderData,
      },
    },
    params: of({customerId: orderData.customerId ,id: orderData.id})
  };

  beforeEach(fakeAsync(() => {
    const orderServiceSpy = jasmine.createSpyObj('OrderService', ['getOrder', 'updateOrder'])

    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [
        {provide: ActivatedRoute, useValue: activatedRouteMock},
        {provide: OrderService, useValue: orderServiceSpy},
      ]
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(CustomerOrdersEditComponent);
        component = fixture.componentInstance;
        el = fixture.debugElement;
        orderService = TestBed.inject(OrderService);
      });
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});

