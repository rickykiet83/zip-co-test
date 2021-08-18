import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CustomerOrdersComponent} from './customer-orders.component';
import {CustomersModule} from "../../customers.module";
import {OrderService} from "../../../core/order.service";
import {ActivatedRoute} from "@angular/router";
import {Observable, of} from "rxjs";

describe('CustomerOrdersComponent', () => {
  let component: CustomerOrdersComponent;
  let fixture: ComponentFixture<CustomerOrdersComponent>;
  let orderService: any;

  const activatedRouteMock = {
    snapshot: {
      data: {
        courses: [
          {
            "id": 96,
            "product": "Needes",
            "quantity": 1,
            "price": 5.99,
            "customerId": 1,
            "status": "InProgress"
          },
          {
            "id": 97,
            "product": "Speakers",
            "quantity": 1,
            "price": 499.99,
            "customerId": 1,
            "status": "InProgress"
          },
        ],
      },
    },
    queryParams: of({id: 1})
  };

  beforeEach(async(() => {
    const orderServiceSpy = jasmine.createSpyObj('OrderService', ['getAllOrders'])

    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
      ],
      providers: [
        {provide: OrderService, useValue: orderServiceSpy},
        {provide: ActivatedRoute, useValue: activatedRouteMock},
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
