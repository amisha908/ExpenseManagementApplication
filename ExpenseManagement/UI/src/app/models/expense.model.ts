// src/app/models/expense.model.ts
export interface Expense {
    id: string;
    amount: number;
    date: string;
    description: string;
    groupId: string;
    paidById: string;
    name: string;
    isSettled: string;
    expenseSplits: ExpenseSplit[];
  }
  
  export interface ExpenseSplit {
    id: string;
    expenseId: string | null;
    userName: string;
    userId: string;
    amount: number;
    owe: number;
    own: number;
  }
  