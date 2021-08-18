import { TestBed } from '@angular/core/testing';

import { CustomerResolverService } from './customer-resolver.service';
import {HttpClientTestingModule, HttpTestingController} from "@angular/common/http/testing";
import {CustomerService} from "./customer.service";

describe('CustomerResolverService', () => {
  let service: CustomerResolverService,
    httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ],
      providers: [CustomerResolverService, CustomerService]
    });
    service = TestBed.inject(CustomerResolverService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
