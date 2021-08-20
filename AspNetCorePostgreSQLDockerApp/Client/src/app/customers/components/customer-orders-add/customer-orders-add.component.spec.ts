import {async, ComponentFixture, TestBed} from '@angular/core/testing';

import {CustomerOrdersAddComponent} from './customer-orders-add.component';
import {of} from "rxjs";
import {CustomersModule} from "../../customers.module";
import {RouterTestingModule} from "@angular/router/testing";
import {ActivatedRoute} from "@angular/router";
import {OrderService} from "../../../core/order.service";
import {CUSTOMERS} from "../../../../common/customer-data";
import {DebugElement} from "@angular/core";
import {By} from "@angular/platform-browser";

describe('CustomerOrdersAddComponent', () => {
  let component: CustomerOrdersAddComponent;
  let fixture: ComponentFixture<CustomerOrdersAddComponent>;
  let el: DebugElement;
  let orderService: any;

  const activatedRouteMock = {
    snapshot: {
      data: {
        customer: CUSTOMERS[0],
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
        el = fixture.debugElement;
        orderService = TestBed.inject(OrderService);
        fixture.detectChanges();
      });
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  fit('should init order form and inputs', () => {

    const containerForm = el.query(By.css('.container-border-form')).nativeElement;
    expect(containerForm).toBeTruthy();

    const inputs = el.queryAll(By.css('input'));
    const inputProduct = inputs.find(i => i.attributes.formControlName === 'product');
    expect(inputProduct).toBeTruthy();
    const inputPrice = inputs.find(i => i.attributes.formControlName === 'price');
    expect(inputPrice).toBeTruthy();
    const inputQuantity = inputs.find(i => i.attributes.formControlName === 'quantity');
    expect(inputQuantity).toBeTruthy();
    const inputStatus = el.query(By.css('select')).attributes;
    expect(inputStatus.formControlName).toEqual('status');

  });
});

