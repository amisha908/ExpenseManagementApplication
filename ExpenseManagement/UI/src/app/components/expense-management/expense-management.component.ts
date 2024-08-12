import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseService } from '../../services/expense.service';
import { AuthService } from '../../services/auth.service';
import { Expense } from '../../models/expense.model';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-expense-management',
  templateUrl: './expense-management.component.html',
  styleUrls: ['./expense-management.component.css']
})
export class ExpenseManagementComponent implements OnInit {

  newExpense: any = {};
  groupId!: string;
  paidById!: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private expenseService: ExpenseService,
    private authService: AuthService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.groupId = this.route.snapshot.paramMap.get('id') || '';
    this.paidById = this.authService.getUserId() || ''; // Assuming this method exists
    console.log('Group ID:', this.groupId);
    console.log('Paid By ID:', this.paidById);
  }

  navigateToGroupDetail() {
    const groupId = this.route.snapshot.paramMap.get('id');
    if (groupId) {
      this.router.navigate(['/group', groupId]);
    }
  }

  validateFormField(form: NgForm, fieldName: string) {
    const control = form.controls[fieldName];
    if (control) {
      control.markAsTouched();
    }
  }

  addExpense(form: NgForm) {
    if (form.invalid) {
      this.markFormTouched(form);
      return;
    }

    const expenseData: Expense = {
      id: '', // provide an appropriate value
      amount: this.newExpense.amount,
      description: this.newExpense.description,
      date: new Date().toISOString(), // Use the current date and time
      groupId: this.groupId, // use the groupId retrieved from the route
      paidById: this.paidById, // use the paidById from the logged-in user
      name: '', // provide an appropriate value if required
      isSettled: "False",
      expenseSplits: [] // Assuming an empty array since no splitAmong field is being used
    };

    this.expenseService.addExpense(expenseData).subscribe(
      response => {
        console.log('Expense added successfully', response);
        this.toastr.success('Expense Added Successfully', 'Success'); // Toastr notification
        setTimeout(() => {
          const groupId = this.route.snapshot.paramMap.get('id');
        if (groupId) {
          this.router.navigate(['/group', groupId]);
        }
        }, 3000);
        
        // Optionally, navigate to another page or reset the form
      },
      error => {
        console.error('Error adding expense', error);
        this.toastr.error('Failed to create group', 'Error');
      }
    );
  }

  markFormTouched(form: NgForm) {
    Object.keys(form.controls).forEach(field => {
      const control = form.controls[field];
      control.markAsTouched({ onlySelf: true });
    });
  }
}



// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ExpenseService } from '../../services/expense.service';
// import { Expense } from '../../models/expense.model';

// @Component({
//   selector: 'app-expense-management',
//   templateUrl: './expense-management.component.html',
//   styleUrls: ['./expense-management.component.css']
// })
// export class ExpenseManagementComponent implements OnInit {

//   newExpense: any = {};
//   groupId!: string;

//   constructor(
//     private route: ActivatedRoute,
//     private router: Router,
//     private expenseService: ExpenseService
//   ) { }

//   ngOnInit(): void {
//     this.groupId = this.route.snapshot.paramMap.get('id') || '';
//     console.log('Group ID:', this.groupId);
//   }
//   navigateToGroupDetail() {
//     const groupId = this.route.snapshot.paramMap.get('id');
//     if (groupId) {
//       this.router.navigate(['/group', groupId]);
//     }
//     }

//   addExpense() {
//     const expenseData: Expense = {
//       id: '', // provide an appropriate value
//       amount: this.newExpense.amount,
//       description: this.newExpense.description,
//       date: this.newExpense.date,
//       groupId: this.groupId, // use the groupId retrieved from the route
//       paidById: this.newExpense.paidBy,
//       name: '', // provide an appropriate value if required
//       expenseSplits: this.newExpense.splitAmong.split(',').map((item: string) => item.trim())
//     };

//     this.expenseService.addExpense(expenseData).subscribe(
//       response => {
//         console.log('Expense added successfully', response);
//         // Optionally, navigate to another page or reset the form
//       },
//       error => {
//         console.error('Error adding expense', error);
//       }
//     );
//   }
// }




// // // src/app/components/expense-management/expense-management.component.ts

// // import { Component } from '@angular/core';
// // import { Router } from '@angular/router';
// // import { User } from '../../models/user.model';
// // import { IDropdownSettings } from 'ng-multiselect-dropdown';
// // import { Expense } from '../../models/group.model';
// // import { UserService } from '../../services/user.service';
// // import { AuthService } from '../../services/auth.service';
// // import { GroupService } from '../../services/group.service';
// // import { ExpenseService } from '../../services/expense.service';
// // @Component({
// //   selector: 'app-expense-management',
// //   templateUrl: './expense-management.component.html',
// //   styleUrls: ['./expense-management.component.css']
// // })
// // export class ExpenseManagementComponent {
// // navigateToGroupDetail() {
// //  this.router.navigate(['/group-detail']);
// // }

// // newExpense: any = {};
// //   expense: any[] = [];
// //   dropdownList = [];
// //   selectedItems = [];
// //   users: User[] = [];
// //   selectedUsers: User[] = [];
// //   dropdownSettings: IDropdownSettings = {};
// //   addExpenseRequest: Expense = {
// //     id: '',
// //     amount: 0,
// //     description: '',
// //     date: new Date(),
// //     groupId: '',
// //     paidbyId:'',
// //     name:''
   
    
// //   };
// //   loading: boolean = true; // Initialize loading state
// //   userName: string | null = ''; // Initialize userName

// //   constructor(
// //     private userService: UserService,
// //     private authService: AuthService,
// //     private groupService: GroupService,
// //     private router: Router,
// //     private expenseService: ExpenseService
// //   ) {}

// //   ngOnInit(): void {
// //     //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
// //     //Add 'implements OnInit' to the class.
    
// //   }
// //   // No logic here, just an empty component for UI
// // }
