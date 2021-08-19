import {TestBed} from '@angular/core/testing';

import {OrderService} from './order.service';
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {CUSTOMERS} from "../../common/customer-data";

describe('OrderService', () => {
  let service: OrderService,
    httpTestingController: HttpTestingController;

  const customers = CUSTOMERS;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [OrderService]
    });
    service = TestBed.inject(OrderService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it("should retrieve all customer's order", () => {
    const customer = customers[0];
    const customerOrders = customer.orders;

    service.getAllOrders(customer.id)
      .subscribe(orders => {
        expect(orders).toBeTruthy('No orders returned.');
      });

    const req = httpTestingController.expectOne(`api/customers/${customer.id}/orders`);
    expect(req.request.method).toEqual('GET');

    req.flush(Object.values(customerOrders));
  });
});
