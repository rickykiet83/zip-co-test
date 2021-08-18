import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CustomerOrdersAddComponent} from './customer-orders-add.component';
import {of} from "rxjs";
import {CustomersModule} from "../../customers.module";
import {RouterTestingModule} from "@angular/router/testing";
import {ActivatedRoute} from "@angular/router";
import {OrderService} from "../../../core/order.service";

describe('CustomerOrdersAddComponent', () => {
  let component: CustomerOrdersAddComponent;
  let fixture: ComponentFixture<CustomerOrdersAddComponent>;
  let orderService: any;

  const activatedRouteMock = {
    snapshot: {
      data: {
        customer: {
          "id": 1,
          "firstName": "Pinal",
          "lastName": "Dave",
          "email": "Pinal.Dave@gmail.com",
          "fullName": "Pinal Dave"
        },
      },
    },
    queryParams: of({id: 1})
  };

  beforeEach(async(() => {
    const orderServiceSpy = jasmine.createSpyObj('OrderService', ['createOrders', 'getAllOrders'])

    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
        RouterTestingModule
      ],
      providers: [
        {provide: ActivatedRoute, useValue: activatedRouteMock},
        {provide: OrderService, useValue: orderServiceSpy},
      ]
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(CustomerOrdersAddComponent);
        component = fixture.componentInstance;
        orderService = TestBed.inject(OrderService);
      });
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

