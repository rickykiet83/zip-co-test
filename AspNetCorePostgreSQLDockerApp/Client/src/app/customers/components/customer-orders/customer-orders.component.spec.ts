import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CustomerOrdersComponent} from './customer-orders.component';
import {CustomersModule} from "../../customers.module";
import {OrderService} from "../../../core/order.service";
import {ActivatedRoute, Router} from "@angular/router";
import {of} from "rxjs";

describe('CustomerOrdersComponent', () => {
  let component: CustomerOrdersComponent;
  let fixture: ComponentFixture<CustomerOrdersComponent>;
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
        orders: [
          {
            "id": 96,
            "product": "Needes",
            "quantity": 1,
            "price": 5.99,
            "customerId": 1,
            "status": "InProgress",
            "orderTotal": 5.99
          },
          {
            "id": 97,
            "product": "Speakers",
            "quantity": 1,
            "price": 499.99,
            "customerId": 1,
            "orderTotal": 499.99,
            "status": "InProgress"
          },
        ],
      },
    },
    queryParams: of({id: 1})
  };

  const routerMock = {

  }

  beforeEach(async(() => {
    const orderServiceSpy = jasmine.createSpyObj('OrderService', ['getAllOrders'])

    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
      ],
      providers: [
        {provide: OrderService, useValue: orderServiceSpy},
        {provide: ActivatedRoute, useValue: activatedRouteMock},
        {provide: Router, useValue: routerMock},
      ]
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(CustomerOrdersComponent);
        component = fixture.componentInstance;
        orderService = TestBed.inject(OrderService);
      });
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
