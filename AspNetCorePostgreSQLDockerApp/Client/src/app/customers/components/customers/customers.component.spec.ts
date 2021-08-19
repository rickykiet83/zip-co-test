import {async, ComponentFixture, TestBed} from "@angular/core/testing";
import {CustomersComponent} from "./customers.component";
import {DebugElement} from "@angular/core";
import {CustomerService} from "../../../core/customer.service";
import {CustomersModule} from "../../customers.module";

describe('CustomersComponent', () => {
  let fixture: ComponentFixture<CustomersComponent>;
  let component: CustomersComponent;
  let el: DebugElement;
  let customerService: any;

  beforeEach(async (() => {

    const customerServiceSpy = jasmine.createSpyObj('CustomerService', ['getCustomersSummary', 'updateCustomer', 'searchCustomersByKeyword']);

    TestBed.configureTestingModule({
      imports: [
        CustomersModule,
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

  
})
