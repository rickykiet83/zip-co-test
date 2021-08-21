import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerOrdersEditComponent } from './customer-orders-edit.component';

describe('CustomerOrdersEditComponent', () => {
  let component: CustomerOrdersEditComponent;
  let fixture: ComponentFixture<CustomerOrdersEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerOrdersEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerOrdersEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
