import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerOrdersAddComponent } from './customer-orders-add.component';

describe('CustomerOrdersAddComponent', () => {
  let component: CustomerOrdersAddComponent;
  let fixture: ComponentFixture<CustomerOrdersAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerOrdersAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerOrdersAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
