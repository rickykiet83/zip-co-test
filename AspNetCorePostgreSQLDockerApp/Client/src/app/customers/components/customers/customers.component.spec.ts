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
import {CUSTOMERS} from "../../../../common/customer-data";

describe('CustomersComponent', () => {
  let fixture: ComponentFixture<CustomersComponent>;
  let component: CustomersComponent;
  let el: DebugElement;
  let customerService: any;

  const customers = CUSTOMERS;

  beforeEach(async(() => {

    const customerServiceSpy = jasmine.createSpyObj('CustomerService', ['searchCustomersByKeyword', 'getCustomersSummary', 'updateCustomer']);

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

  it('should onSearchByKeyword is trigger by click Search button', fakeAsync(() => {

    spyOn(component, 'onSearchByKeyword');
    const searchButton = el.query(By.css('#btn-search')).nativeElement;
    searchButton.click();

    flush();
    expect(component.onSearchByKeyword).toHaveBeenCalled();
  }));

  it('should display customers with filtered email should work', fakeAsync(() => {

    const email = customers[0].email;
    const filteredCustomers = customers.filter(c => c.email === email);
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
