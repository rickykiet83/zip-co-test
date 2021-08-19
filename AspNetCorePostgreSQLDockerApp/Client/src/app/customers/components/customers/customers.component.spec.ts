import {async, ComponentFixture, TestBed} from "@angular/core/testing";
import {CustomersComponent} from "./customers.component";
import {DebugElement} from "@angular/core";
import {CustomerService} from "../../../core/customer.service";
import {CustomersModule} from "../../customers.module";
import {of} from "rxjs";
import {By} from "@angular/platform-browser";
import {ICustomer} from "../../../shared/interfaces";
import {RouterTestingModule} from "@angular/router/testing";

describe('CustomersComponent', () => {
  let fixture: ComponentFixture<CustomersComponent>;
  let component: CustomersComponent;
  let el: DebugElement;
  let customerService: any;

  const customers: ICustomer[] = [
    {
      id: 1,
      firstName: "Michelle 123",
      lastName: "Avery",
      email: "Michelle.Avery@acmecorp.com",
      address: "346 Cedar Ave.",
      city: "Dallas",
      state: null,
      zip: 85237,
      gender: "Female",
      orderCount: 7,
      orders: null,
      latitude: 0,
      longitude: 0
    },
    {
      id: 2,
      firstName: "Ward",
      "lastName": "Bell",
      "email": "Ward.Bell@gmail.com",
      "address": "12 Ocean View St.",
      "city": "Dallas",
      "state": null,
      "zip": 85233,
      "gender": "Male",
      "orderCount": 11,
      "orders": null,
      latitude: 0,
      longitude: 0
    },
  ];

  beforeEach(async(() => {

    const customerServiceSpy = jasmine.createSpyObj('CustomerService', ['getCustomersSummary', 'updateCustomer', 'searchCustomersByKeyword']);

    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
        RouterTestingModule
      ],
      providers: [
        {provide: CustomerService, useValue: customerServiceSpy}
      ]
    }).compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(CustomersComponent);
        component = fixture.componentInstance;
        el = fixture.debugElement;
        customerService = TestBed.inject(CustomerService);
      });

  }));

  it("should create the component", () => {

    expect(component).toBeTruthy();

  });


  it("should display the customer list", () => {

    customerService.getCustomersSummary.and.returnValue(of(customers));

    fixture.detectChanges();

    component.customers = customers;
    expect(component.customers).toBeTruthy();
    expect(component.customers.length >= 2);

  });


})
