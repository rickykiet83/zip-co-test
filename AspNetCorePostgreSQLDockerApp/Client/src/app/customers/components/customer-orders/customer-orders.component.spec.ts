import {async, ComponentFixture, fakeAsync, TestBed} from '@angular/core/testing';

import {CustomerOrdersComponent} from './customer-orders.component';
import {CustomersModule} from "../../customers.module";
import {ActivatedRoute, Router} from "@angular/router";
import {of} from "rxjs";
import {RouterTestingModule} from "@angular/router/testing";
import {CUSTOMERS} from "../../../../common/customer-data";

describe('CustomerOrdersComponent', () => {
  let component: CustomerOrdersComponent;
  let fixture: ComponentFixture<CustomerOrdersComponent>;
  const customer = CUSTOMERS[0];
  const customerOrders = customer.orders;

  const activatedRouteMock = {
    snapshot: {
      data: {
        customer,
        orders: customerOrders,
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

    expect(component).toBeTruthy();

  })

  it('should display all orders', fakeAsync(() => {

    const customerOrdersComponent: any = component;
    console.log(customerOrdersComponent.route.snapshot.data);
    const orders = customerOrdersComponent.route.snapshot.data.orders;
    expect(orders).toEqual(customerOrders);

  }));

  

});
