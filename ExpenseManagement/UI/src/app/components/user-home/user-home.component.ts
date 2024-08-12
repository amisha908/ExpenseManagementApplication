import { Component, OnInit } from '@angular/core';
import { ExpenseService } from '../../services/expense.service';
import { AuthService } from '../../services/auth.service';
import { UserService } from '../../services/user.service';
import { Expense } from '../../models/expense.model';
import { Group } from '../../models/group.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css']
})
export class UserHomeComponent implements OnInit {
  loading = true;
  expenses: Expense[] = [];
  groups: Group[] = [];
  userId: string | null = null;
  userName: string | null = null;
  totalOwes: number = 0;
  totalOwns: number = 0;

  constructor(
    private expenseService: ExpenseService, 
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.userName = this.authService.getUserName();
    console.log(this.userName);

    if (this.userId) {
      this.expenseService.getGroupsForUser(this.userId).subscribe(
        (groups) => {
          this.groups = groups;
          this.loading = false;
        },
        (error) => {
          console.error('Error fetching groups', error);
          this.loading = false;
        }
      );

      this.userService.getTotalOwes(this.userId).subscribe(
        (totalOwes) => {
          this.totalOwes = totalOwes;
        },
        (error) => {
          console.error('Error fetching total owes', error);
        }
      );

      this.userService.getTotalOwns(this.userId).subscribe(
        (totalOwns) => {
          this.totalOwns = totalOwns;
        },
        (error) => {
          console.error('Error fetching total owns', error);
        }
      );
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}


// // src/app/components/user-home/user-home.component.ts

// import { Component, OnInit } from '@angular/core';
// import { ExpenseService } from '../../services/expense.service';
// import { AuthService } from '../../services/auth.service';
// import { Expense } from '../../models/expense.model';
// import { Group } from '../../models/group.model';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-user-home',
//   templateUrl: './user-home.component.html',
//   styleUrls: ['./user-home.component.css']
// })
// export class UserHomeComponent implements OnInit {
//   loading = true;
//   expenses: Expense[] = [];
//   groups: Group[] = [];
//   userId: string | null = null;
//   userName: string | null = null;
//   totalOwes: number = 0;
//   totalOwns: number = 0;

//   constructor(
//     private expenseService: ExpenseService, 
//     private authService: AuthService,
//     private router: Router
//   ) {}

//   ngOnInit(): void {
//     this.userId = this.authService.getUserId();
//     this.userName = this.authService.getUserName();
//     console.log(this.userName);

//     if (this.userId) {
//       this.expenseService.getGroupsForUser(this.userId).subscribe(
//         (groups) => {
//           this.groups = groups;
//           this.loading = false;
//         },
//         (error) => {
//           console.error('Error fetching groups', error);
//           this.loading = false;
//         }
//       );
//     }

//     // Fetch expenses logic if needed...
//   }

//   calculateBalances(): void {
//     let owns = 0;
//     let owes = 0;

//     this.expenses.forEach(expense => {
//       if (expense.paidById === this.userId) {
//         owns += expense.amount;

//         expense.expenseSplits.forEach(split => {
//           if (split.userId !== this.userId) {
//             owns -= split.amount;
//           }
//         });
//       } else {
//         const userSplit = expense.expenseSplits.find(split => split.userId === this.userId);
//         if (userSplit) {
//           owes += userSplit.amount;
//         }
//       }
//     });

//     this.totalOwns = owns;
//     this.totalOwes = owes;
//   }

//   logout(): void {
//     this.authService.logout();
//     this.router.navigate(['/login']);
//   }

  
//   private generateUniqueId(): string {
//     return Math.random().toString(36).substr(2, 9);
//   }
// }

