import { TestBed } from '@angular/core/testing';

import { OrderResolveService } from './order.resolver';
import {OrderService} from "./order.service";
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";

describe('OrderResolveService', () => {
  let service: OrderResolveService,
    httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [OrderResolveService]
    });
    service = TestBed.inject(OrderResolveService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
