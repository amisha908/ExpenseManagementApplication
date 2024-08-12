import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Expense } from '../models/expense.model';
import { Group } from '../models/group.model';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private apiUrl = 'https://localhost:44377/api';

  constructor(private http: HttpClient) {}

  getExpensesForGroup(groupId: string): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${this.apiUrl}/Expense/group/${groupId}`);
  }

  getGroupsForUser(userId: string): Observable<Group[]> {
    return this.http.get<Group[]>(`${this.apiUrl}/Member/groups/${userId}`);
  }

  getGroupById(groupId: string): Observable<Group> {
    return this.http.get<Group>(`${this.apiUrl}/Group/${groupId}`);
  }

  addExpense(expenseObj: Expense): Observable<Expense> {
    return this.http.post<Expense>(`${this.apiUrl}/Expense/add`, expenseObj);
  }

  splitExpenses(groupId: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Expense/add-splits`, { groupId });
  }
  settleExpenses(expenseIds: string[]): Observable<any> {
    // Implement HTTP call to settle expenses
    return this.http.post<any>(`${this.apiUrl}/Expense/settle`, expenseIds);
  }
}


// // src/app/services/expense.service.ts

// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { Expense } from '../models/expense.model';
// import { Group } from '../models/group.model';

// @Injectable({
//   providedIn: 'root'
// })
// export class ExpenseService {
//   private apiUrl = 'https://localhost:44377/api';

//   constructor(private http: HttpClient) {}

//   getExpensesForGroup(groupId: string): Observable<Expense[]> {
//     return this.http.get<Expense[]>(`${this.apiUrl}/Expense/group/${groupId}`);
//   }

//   getGroupsForUser(userId: string): Observable<Group[]> {
//     return this.http.get<Group[]>(`${this.apiUrl}/Member/groups/${userId}`);
//   }
//   getGroupById(groupId: string): Observable<Group> {
//     return this.http.get<Group>(`${this.apiUrl}/Group/${groupId}`);
// }
// addExpense(expenseObj : Expense) : Observable<Expense>{
//   return this.http.post<Expense>(this.apiUrl + '/Expense/add', expenseObj)
// }

// }


// // src/app/services/expense.service.ts
// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs';
// import { Expense } from '../models/expense.model';
// import { environment } from '../../environments/environment';

// @Injectable({
//   providedIn: 'root'
// })
// export class ExpenseService {
//   private apiUrl = 'https://localhost:44377'; // Replace with your backend API URL

//   constructor(private http: HttpClient) {}

//   getExpensesForGroup(groupId: string): Observable<Expense[]> {
//     return this.http.get<Expense[]>(`${this.apiUrl}/api/Expense/group/${groupId}`);
//   }
// }
