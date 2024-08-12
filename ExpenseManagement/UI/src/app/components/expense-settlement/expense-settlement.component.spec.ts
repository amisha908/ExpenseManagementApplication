import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseSettlementComponent } from './expense-settlement.component';

describe('ExpenseSettlementComponent', () => {
  let component: ExpenseSettlementComponent;
  let fixture: ComponentFixture<ExpenseSettlementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ExpenseSettlementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpenseSettlementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
