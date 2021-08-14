import {TestBed} from "@angular/core/testing";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {DataService} from "./data.service";

describe("DataService", () => {
  let dataService: DataService,
    httpTestingController: HttpTestingController;

  const customers: any = [
    {
      "id": 15,
      "firstName": "Michelle",
      "lastName": "Avery",
      "email": "Michelle.Avery@acmecorp.com",
      "address": "346 Cedar Ave.",
      "city": "Dallas",
      "state": null,
      "zip": 85237,
      "gender": "Female",
      "orderCount": 7,
      "orders": null
    },
    {
      "id": 22,
      "firstName": "Ward",
      "lastName": "Bell",
      "email": "Ward.Bell@gmail.com",
      "address": "12 Ocean View St.",
      "city": "Dallas",
      "state": null,
      "zip": 85233,
      "gender": "Male",
      "orderCount": 10,
      "orders": null
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [DataService]
    });
    dataService = TestBed.inject(DataService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should retrieve all customers', () => {
    dataService.getCustomersSummary()
      .subscribe(customers => {
        expect(customers).toBeTruthy('No customers returned.');
        expect(customers.length).toEqual(2);
      });

    const req = httpTestingController.expectOne('api/customersservice/customers/');
    expect(req.request.method).toEqual('GET');
    req.flush(Object.values(customers));
  });

  it('should update a customer', () => {
    pending();
  });
})
