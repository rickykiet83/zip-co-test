import {async, ComponentFixture, fakeAsync, TestBed} from '@angular/core/testing';

import {CustomerOrdersComponent} from './customer-orders.component';
import {CustomersModule} from "../../customers.module";
import {ActivatedRoute, Router} from "@angular/router";
import {of} from "rxjs";
import {RouterTestingModule} from "@angular/router/testing";
import {CUSTOMERS} from "../../../../common/customer-data";
import {DebugElement} from "@angular/core";
import {By} from "@angular/platform-browser";
import {OrderService} from "../../../core/order.service";
import Order = jasmine.Order;

describe('CustomerOrdersComponent', () => {
  let component: CustomerOrdersComponent;
  let fixture: ComponentFixture<CustomerOrdersComponent>;
  let el: DebugElement;
  const customer = CUSTOMERS[0];
  const customerOrders = customer.orders;
  let orderService: any;
  const activatedRouteMock = {
    snapshot: {
      data: {
        customer,
        orders: customerOrders,
      },
    },
    params: of({id: customer.id})
  };

  beforeEach(async(() => {
    const orderServiceSpy = jasmine.createSpyObj('OrderService', ['getAllOrders'])

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
        fixture = TestBed.createComponent(CustomerOrdersComponent);
        el = fixture.debugElement;
        orderService = TestBed.inject(OrderService);
        component = fixture.componentInstance;
      });
  }));

  it('should create the component', async () => {

    expect(component).toBeTruthy();

  })

  it('should display all orders', async (() => {

    const customerOrdersComponent: any = component;
    const orders = customerOrdersComponent.route.snapshot.data.orders;
    expect(orders).toEqual(customerOrders);

  }));

  it('should display total Avenue', async (() => {

    let revenueElement = el.query(By.css('#revenue')).nativeElement;
    expect(revenueElement).toBeTruthy();

    let tableElement = el.query(By.css('table')).nativeElement;
    expect(tableElement).toBeTruthy();
  }));

});
