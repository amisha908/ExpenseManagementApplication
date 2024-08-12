import { Component } from '@angular/core';

@Component({
  selector: 'app-expense-settlement',
  templateUrl: './expense-settlement.component.html',
  styleUrls: ['./expense-settlement.component.css']
})
export class ExpenseSettlementComponent {
  settlement: any = {};
  balances: { user: string, amount: number }[] = [
    { user: 'User 1', amount: 100 },
    { user: 'User 2', amount: -50 },
    { user: 'User 3', amount: -50 }
  ];

  settleExpense() {
    // Placeholder function to handle expense settlement logic
    console.log("Expense settled:", this.settlement);
    this.settlement = {};
  }
}
