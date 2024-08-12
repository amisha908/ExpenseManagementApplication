// src/app/models/group.model.ts
export interface Group {
    id: string;
    name: string;
    description: string;
    createdDate: Date
    memberIds: Member[];
    expenses: Expense[];
  }
  
  export interface Member {
    user: any;
    userId: string;
    id: string;
    name: string;
    email: string;
  }
  
  export interface Expense {
    id: string;
    description: string;
    amount: number;
    date: Date;
    paidBy: Member;
    splits: ExpenseSplit[];
  }
  
  export interface ExpenseSplit {
    id: string;
    amount: number;
    userId: string;
  }
  