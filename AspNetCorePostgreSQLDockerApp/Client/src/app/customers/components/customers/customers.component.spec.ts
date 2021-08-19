import {async, ComponentFixture, fakeAsync, flush, TestBed} from "@angular/core/testing";
import {CustomersComponent} from "./customers.component";
import {DebugElement, Input} from "@angular/core";
import {CustomerService} from "../../../core/customer.service";
import {CustomersModule} from "../../customers.module";
import {of} from "rxjs";
import {By} from "@angular/platform-browser";
import {ICustomer} from "../../../shared/interfaces";
import {RouterTestingModule} from "@angular/router/testing";
import {log} from "util";
import {click} from "../../../../common/test-utils";

describe('CustomersComponent', () => {
  let fixture: ComponentFixture<CustomersComponent>;
  let component: CustomersComponent;
  let el: DebugElement;
  let customerService: any;

  const allCustomers: ICustomer[] = [
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

    const customerServiceSpy = jasmine.createSpyObj('CustomerService', ['searchCustomersByKeyword','getCustomersSummary', 'updateCustomer']);

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

    customerService.getCustomersSummary.and.returnValue(of(allCustomers));

    fixture.detectChanges();

    component.customers = allCustomers;
    expect(component.customers).toBeTruthy();
    expect(component.customers.length >= 2);

  });

  it('should click Search button', fakeAsync(() => {

    spyOn(component, 'onSearchByKeyword');
    const searchButton = el.query(By.css('#btn-search')).nativeElement;
    searchButton.click();

    flush();
    expect(component.onSearchByKeyword).toHaveBeenCalled();
  }));

  it('should display customers with filtered email', fakeAsync(() => {

    const email = allCustomers[0].email;
    const filteredCustomers = allCustomers.filter(c => c.email === email);
    component.keyword = email;

    spyOn(component, 'onSearchByKeyword');

    customerService.getCustomersSummary.and.returnValue(of(filteredCustomers));

    fixture.detectChanges();

    let inputText = el.query(By.css('#input-search')).nativeElement;
    inputText.textContent = email;

    const searchButton = el.query(By.css('#btn-search')).nativeElement;
    searchButton.click();

    fixture.detectChanges();

    customerService.searchCustomersByKeyword.and.returnValue(of(filteredCustomers));

    fixture.detectChanges();
    flush();

    expect(component.customers).toEqual(filteredCustomers);
    expect(inputText.textContent).toContain(email);
  }));



})
