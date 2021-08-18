import {TestBed} from '@angular/core/testing';

import {OrderService} from './order.service';
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {CustomerService} from "./customer.service";

describe('OrderService', () => {
  let service: OrderService,
    httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [CustomerService]
    });
    service = TestBed.inject(OrderService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
