import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {CustomerService} from "./customer.service";
import {ICustomer} from "../shared/interfaces";
import {CUSTOMERS} from "../../common/customer-data";

describe("CustomerService", () => {
  let customerService: CustomerService,
    httpTestingController: HttpTestingController;

  const customers = CUSTOMERS;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [CustomerService]
    });
    customerService = TestBed.inject(CustomerService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should retrieve all customers', () => {
    customerService.getCustomersSummary()
      .subscribe(customers => {
        expect(customers).toBeTruthy('No customers returned.');
        expect(customers.length).toEqual(2);
      });

    const req = httpTestingController.expectOne('api/customersservice/customers/');
    expect(req.request.method).toEqual('GET');

    req.flush(Object.values(customers));
  });

  it('should update a customer', () => {
    const change: ICustomer = {
      id: customers[0].id,
      firstName: "Test",
      lastName: "Avery",
      email: "Michelle.Avery@acmecorp.com",
      address: "346 Cedar Ave.",
      city: "Dallas",
      state: null,
      zip: 85237,
      gender: "Female",
      latitude: 0,
      longitude: 0,
    };
    customerService.updateCustomer(change)
      .subscribe((result) => {
        expect(result).toBeTruthy('Update customer failed.');
      });

    const req = httpTestingController.expectOne(`api/customersservice/customers/${change.id}`);
    expect(req.request.method).toEqual('PUT');

    expect(req.request.body.firstName)
      .toEqual(change.firstName);

    req.flush({
      ...customers[0],
      ...change
    });
  });
})
