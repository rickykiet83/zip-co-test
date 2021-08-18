import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CustomerOrdersComponent} from './customer-orders.component';
import {CustomersModule} from "../../customers.module";
import {ActivatedRoute, Router} from "@angular/router";
import {of} from "rxjs";
import {RouterTestingModule} from "@angular/router/testing";

describe('CustomerOrdersComponent', () => {
  let component: CustomerOrdersComponent;
  let fixture: ComponentFixture<CustomerOrdersComponent>;

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

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
        RouterTestingModule
      ],
      providers: [
        {provide: ActivatedRoute, useValue: activatedRouteMock},
      ]
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(CustomerOrdersComponent);
        component = fixture.componentInstance;
      });
  }));

  it('should create the component', () => {
    console.log(component);
  })

});
