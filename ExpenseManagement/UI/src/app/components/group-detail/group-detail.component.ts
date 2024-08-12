import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService } from '../../services/expense.service';
import { Group, Member } from '../../models/group.model';
import { Expense, ExpenseSplit } from '../../models/expense.model';
import { GroupService } from '../../services/group.service';

@Component({
  selector: 'app-group-detail',
  templateUrl: './group-detail.component.html',
  styleUrls: ['./group-detail.component.css']
})
export class GroupDetailComponent implements OnInit {
  group!: Group;
  expenses: Expense[] = [];
  loading = true;
  members!: Member[];
 

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private expenseService: ExpenseService,
    private groupService: GroupService
  ) {}

  ngOnInit(): void {
    const groupId = this.route.snapshot.paramMap.get('id');
    if (groupId) {
      this.groupService.getMember(groupId).subscribe(
        (members) => {
          this.members = members;
          this.loadExpenses(groupId);
          this.loadMembers(groupId);
        },
        (error) => {
          console.error('Error fetching group members', error);
          this.loading = false;
        }
      );
      this.expenseService.getGroupById(groupId).subscribe(
        (group) => {
          this.group = group;
        },
        (error) => {
          console.error('Error fetching group details', error);
          this.loading = false;
        }
      );
    }
  }

  loadExpenses(groupId: string): void {
    this.expenseService.getExpensesForGroup(groupId).subscribe(
      (expenses) => {
        
        this.expenses = expenses.filter((expense, index, self) =>
          index === self.findIndex((e) => e.id === expense.id)
        
        
        );
        console.log(expenses);
        
        this.loading = false;
      },
      (error) => {
        console.error('Error fetching expenses', error);
        this.loading = false;
      }
      
    );
  }

  loadMembers(groupId: string): void {
    this.groupService.getMember(groupId).subscribe(
      (members) => {
        this.members = members;
      },
      (error) => {
        console.error('Error fetching members', error);
      }
    );
  }

  settleExpense(expense: Expense): void {
    const expenseIds = [expense.id];
    this.expenseService.settleExpenses(expenseIds).subscribe(
      () => {
        console.log(expense.isSettled);
        console.log('Expense settled successfully');
        console.log(typeof expense.isSettled); // should output 'boolean'

        expense.isSettled ="True"; // Update locally to reflect immediate change
      },
      (error) => {
        console.error('Error settling expense', error);
      }
    );
  }

  navigateToAddExpense(): void {
    const groupId = this.route.snapshot.paramMap.get('id');
    if (groupId) {
      this.router.navigate(['/expense-management', groupId]);
    }
  }
  splitExpenses(): void {
        const groupId = this.route.snapshot.paramMap.get('id');
        if (groupId) {
          this.expenseService.splitExpenses(groupId).subscribe(
            (response) => {
              console.log('Expenses split successfully:', response);
              this.loadExpenses(groupId); // Reload expenses to reflect the changes
            },
            (error) => {
              console.error('Error splitting expenses', error);
            }
          );
        }
      }

  goBack(): void {
    this.router.navigate(['/user-home']);
  }
}


// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ExpenseService } from '../../services/expense.service';
// import { Group, Member } from '../../models/group.model';
// import { Expense } from '../../models/expense.model';
// import { GroupService } from '../../services/group.service';

// @Component({
//   selector: 'app-group-detail',
//   templateUrl: './group-detail.component.html',
//   styleUrls: ['./group-detail.component.css']
// })
// export class GroupDetailComponent implements OnInit {
// settleExpense(_t28: Expense) {
// throw new Error('Method not implemented.');
// }
//   group!: Group;
//   expenses: Expense[] = [];
//   loading = true;
//   members!: Member[];

//   constructor(
//     private route: ActivatedRoute,
//     private router: Router,
//     private expenseService: ExpenseService,
//     private groupService: GroupService
//   ) {}

//   ngOnInit(): void {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.groupService.getMember(groupId).subscribe(
//         (members) => {
//           this.members = members;
//           console.log('members', members);
//           this.loadExpenses(groupId);
//           this.loadMembers(groupId);
//         },
//         (error) => {
//           console.error('Error fetching group details', error);
//           this.loading = false;
//         }
//       );
//       this.expenseService.getGroupById(groupId).subscribe(
//         (group) => {
//           this.group = group;
//           this.loadExpenses(groupId);
//         },
//         (error) => {
//           console.error('Error fetching group details', error);
//           this.loading = false;
//         }
//       );
//     }
//   }

//   loadExpenses(groupId: string): void {
//     this.expenseService.getExpensesForGroup(groupId).subscribe(
//       (expenses) => {
//         console.log('Fetched expenses:', expenses);

//         this.expenses = expenses.filter((expense, index, self) =>
//           index === self.findIndex((e) => e.id === expense.id)
//         );
//         this.loading = false;
//       },
//       (error) => {
//         console.error('Error fetching expenses', error);
//         this.loading = false;
//       }
//     );
//   }

//   navigateToAddExpense() {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.router.navigate(['/expense-management', groupId]);
//     }
//   }

//   loadMembers(groupId: string): void {
//     this.groupService.getMember(groupId).subscribe(
//       (members) => {
//         this.members = members;
//       },
//       (error) => {
//         console.error('Error fetching members', error);
//       }
//     );
//   }

//   splitExpenses(): void {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.expenseService.splitExpenses(groupId).subscribe(
//         (response) => {
//           console.log('Expenses split successfully:', response);
//           this.loadExpenses(groupId); // Reload expenses to reflect the changes
//         },
//         (error) => {
//           console.error('Error splitting expenses', error);
//         }
//       );
//     }
//   }

//   getMemberNameById(userId: string) {
//     const member = this.expenses.find((m) => m.expenseSplits.map((u) => u.id));
//     console.log(member);
//   }

//   goBack(): void {
//     this.router.navigate(['/user-home']);
//   }
// }


// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ExpenseService } from '../../services/expense.service';
// import { Group, Member } from '../../models/group.model';
// import { Expense } from '../../models/expense.model';
// import { GroupService } from '../../services/group.service';

// @Component({
//   selector: 'app-group-detail',
//   templateUrl: './group-detail.component.html',
//   styleUrls: ['./group-detail.component.css']
// })
// export class GroupDetailComponent implements OnInit {
// splitExpenses() {
// throw new Error('Method not implemented.');
// }
//   group!:Group;
//   expenses: Expense[] = [];
//   loading = true;
//   members!:Member[];
  
//   constructor(
//     private route: ActivatedRoute,
//     private router: Router,
//     private expenseService: ExpenseService,
//     private groupService: GroupService
//   ) { }

//   ngOnInit(): void {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.groupService.getMember(groupId).subscribe(
//         (members) => {
//           this.members=members;
//          console.log("members",members);
//           this.loadExpenses(groupId);
//           this.loadMembers(groupId);
//         },
//         (error) => {
//           console.error('Error fetching group details', error);
//           this.loading = false;
//         }
//       );
//       this.expenseService.getGroupById(groupId).subscribe(
//                 (group) => {
//                   this.group = group;
//                   this.loadExpenses(groupId);
//                 },
//                 (error) => {
//                   console.error('Error fetching group details', error);
//                   this.loading = false;
//                 }
//               );
//     }
//   }

//    loadExpenses(groupId: string): void {
//     this.expenseService.getExpensesForGroup(groupId).subscribe(
//       (expenses) => {
//         console.log('Fetched expenses:', expenses);

//         this.expenses = expenses.filter((expense, index, self) =>
//           index === self.findIndex((e) => (
//             e.id === expense.id
//           ))
//         );
//         // console.log('Filtered expenses:', this.expenses);
//         this.loading = false;
//       },
//       (error) => {
//         console.error('Error fetching expenses', error);
//         this.loading = false;
//       }
//     );
//   }
//   navigateToAddExpense() {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.router.navigate(['/expense-management', groupId]);
//     }
  
//   }

//   loadMembers(groupId: string): void {
//     this.groupService.getMember(groupId).subscribe(
//       (members) => {
//         this.members = members;
//       },
//       (error) => {
//         console.error('Error fetching members', error);
//       }
//     );
//   }


//   getMemberNameById(userId: string) {
//     const member = this.expenses.find(m => m.expenseSplits.map(u=>u.id));
//       console.log(member)
//     // return member ? member.name : 'Unknown';
//   }
//   goBack(): void {
//     this.router.navigate(['/user-home']);
//   }
// }



// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
// import { ExpenseService } from '../../services/expense.service';
// import { Group } from '../../models/group.model';
// import { Expense } from '../../models/expense.model';

// @Component({
//   selector: 'app-group-detail',
//   templateUrl: './group-detail.component.html',
//   styleUrls: ['./group-detail.component.css']
// })
// export class GroupDetailComponent implements OnInit {
//   group: Group | null = null;
//   expenses: Expense[] = [];
//   loading = true;

//   constructor(
//     private route: ActivatedRoute,
//     private expenseService: ExpenseService
//   ) { }

//   ngOnInit(): void {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.expenseService.getGroupById(groupId).subscribe(
//         (group) => {
//           this.group = group;
//           this.loadExpenses(groupId);
//         },
//         (error) => {
//           console.error('Error fetching group details', error);
//           this.loading = false;
//         }
//       );
//     }
//   }

//   loadExpenses(groupId: string): void {
//     this.expenseService.getExpensesForGroup(groupId).subscribe(
//       (expenses) => {
//         console.log('Fetched expenses:', expenses);
//         this.expenses = expenses.filter((expense, index, self) => 
//           index === self.findIndex((e) => (
//             e.id === expense.id
//           ))
//         );
//         console.log('Filtered expenses:', this.expenses);
//         this.loading = false;
//       },
//       (error) => {
//         console.error('Error fetching expenses', error);
//         this.loading = false;
//       }
//     );
//   }

//   getMemberNameById(userId: string): string {
//     const member = this.group?.memberIds.find(m => m.id === userId);
//     return member ? member.name : 'Unknown';
//   }
// }
