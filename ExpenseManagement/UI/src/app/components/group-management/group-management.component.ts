import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';
import { AuthService } from '../../services/auth.service';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { animate, style, transition, trigger, state } from '@angular/animations';

@Component({
  selector: 'app-group-management',
  templateUrl: './group-management.component.html',
  styleUrls: ['./group-management.component.css'],
  animations: [
    trigger('flyInOut', [
      state('in', style({ transform: 'translateX(0)' })),
      transition('void => *', [
        style({ transform: 'translateX(-100%)' }),
        animate(300)
      ]),
      transition('* => void', [
        animate(300, style({ transform: 'translateX(100%)' }))
      ])
    ])
  ]
})
export class GroupManagementComponent implements OnInit {

  newGroup: any = {};
  groups: any[] = [];
  dropdownList = [];
  selectedItems = [];
  users: User[] = [];
  selectedUsers: User[] = [];
  dropdownSettings: IDropdownSettings = {};
  addGroupRequest: Group = {
    id: '',
    name: '',
    description: '',
    createdDate: new Date(),
    memberIds: [],
    expenses: []
  };
  loading: boolean = true; // Initialize loading state
  userName: string | null = ''; // Initialize userName
  memberLimitWarning: boolean = false; // To display warning when more than 10 members are selected

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private groupService: GroupService,
    private router: Router,
    private toastr: ToastrService,
  ) {}

  ngOnInit() {
    this.userName = this.authService.getUserName(); // Set userName
    

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Select All',
      unSelectAllText: 'Unselect All',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };

    this.userService.getAllUsers().subscribe({
      next: (users) => {
        this.users = users;
        this.loading = false; // Set loading to false after users are loaded
      },
      error: (err) => {
        console.error('Error fetching users', err);
        this.loading = false; // Set loading to false in case of error
      }
    });

    this.groupService.getAllGroup().subscribe({
      next: (groups) => {
        this.groups = groups;
      },
      error: (err) => {
        console.error('Error fetching groups', err);
      }
    });
  }

  navigateToUserHome() {
    this.router.navigate(['/user-home']);
  }

  validateFormField(form: NgForm, fieldName: string) {
    const control = form.controls[fieldName];
    
    if (control) {
      control.markAsTouched();
      if (control.valid) {
        control.markAsUntouched();
      }
    }
  }

  validateMembersField(form: NgForm) {
    if (this.selectedUsers.length) {
      form.controls['members'].setErrors(null);
    } else {
      form.controls['members'].setErrors({ required: true });
    }
  }

  onItemSelect(item: any, form: NgForm) {
    if (this.addGroupRequest.memberIds.length >= 10) {
      this.memberLimitWarning = true;
      setTimeout(() => {
        const index = this.selectedUsers.findIndex(user => user.id === item.id);
        if (index !== -1) {
          this.selectedUsers.splice(index, 1);
        }
      }, 0);
    } else {
      this.addGroupRequest.memberIds.push(item.id);
      this.validateMembersField(form);
      this.checkMemberLimitWarning();
    }
  }

  onItemDeselect(item: any, form: NgForm) {
    const index = this.addGroupRequest.memberIds.indexOf(item.id);
    if (index !== -1) {
      this.addGroupRequest.memberIds.splice(index, 1);
    }
    this.validateMembersField(form);
    this.checkMemberLimitWarning();
  }

  onSelectAll(items: any, form: NgForm) {
    if (items.length > 10) {
      this.memberLimitWarning = true;
      this.selectedUsers = this.selectedUsers.slice(0, 10);
    } else {
      this.addGroupRequest.memberIds = items.map((item: User) => item.id);
      this.memberLimitWarning = false;
    }
    this.validateMembersField(form);
  }

  onDeselectAll(form: NgForm) {
    this.addGroupRequest.memberIds = [];
    this.validateMembersField(form);
    this.checkMemberLimitWarning();
  }

  checkMemberLimitWarning() {
    this.memberLimitWarning = this.addGroupRequest.memberIds.length > 10;
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  createGroup(form: NgForm) {
    if (form.invalid) {
      this.markFormTouched(form);
      return;
    }

    console.log(this.addGroupRequest);
    this.groupService.addGroup(this.addGroupRequest).subscribe({
      next: (group) => {
        console.log(group);
        this.toastr.success('Group created Successfully', 'Success'); // Toastr notification
        setTimeout(() => {
          this.router.navigate(['user-home']);
        }, 3000); // Navigate after 3 seconds
      },
      error: (err) => {
        console.error('Error creating group', err);
        this.toastr.error('Failed to create group', 'Error');
      }
    });
  }

  markFormTouched(form: NgForm) {
    Object.keys(form.controls).forEach(field => {
      const control = form.controls[field];
      control.markAsTouched({ onlySelf: true });
    });
  }
}


// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { IDropdownSettings } from 'ng-multiselect-dropdown';
// import { UserService } from '../../services/user.service';
// import { User } from '../../models/user.model';
// import { GroupService } from '../../services/group.service';
// import { Group } from '../../models/group.model';
// import { AuthService } from '../../services/auth.service';
// import { NgForm } from '@angular/forms';
// import { ToastrService } from 'ngx-toastr';
// import { animate, style, transition, trigger, state } from '@angular/animations';

// @Component({
//   selector: 'app-group-management',
//   templateUrl: './group-management.component.html',
//   styleUrls: ['./group-management.component.css'],
//   animations: [
//     trigger('flyInOut', [
//       state('in', style({ transform: 'translateX(0)' })),
//       transition('void => *', [
//         style({ transform: 'translateX(-100%)' }),
//         animate(300)
//       ]),
//       transition('* => void', [
//         animate(300, style({ transform: 'translateX(100%)' }))
//       ])
//     ])
//   ]
// })
// export class GroupManagementComponent implements OnInit {

//   newGroup: any = {};
//   groups: any[] = [];
//   dropdownList = [];
//   selectedItems = [];
//   users: User[] = [];
//   selectedUsers: User[] = [];
//   dropdownSettings: IDropdownSettings = {};
//   addGroupRequest: Group = {
//     id: '',
//     name: '',
//     description: '',
//     createdDate: new Date(),
//     memberIds: [],
//     expenses: []
//   };
//   loading: boolean = true; // Initialize loading state
//   userName: string | null = ''; // Initialize userName

//   constructor(
//     private userService: UserService,
//     private authService: AuthService,
//     private groupService: GroupService,
//     private router: Router,
//     private toastr: ToastrService,
//   ) {}

//   ngOnInit() {
//     this.userName = this.authService.getUserName(); // Set userName
    

//     this.dropdownSettings = {
//       singleSelection: false,
//       idField: 'id',
//       textField: 'name',
//       selectAllText: 'Select All',
//       unSelectAllText: 'Unselect All',
//       itemsShowLimit: 3,
//       allowSearchFilter: true
//     };

//     this.userService.getAllUsers().subscribe({
//       next: (users) => {
//         this.users = users;
//         this.loading = false; // Set loading to false after users are loaded
//       },
//       error: (err) => {
//         console.error('Error fetching users', err);
//         this.loading = false; // Set loading to false in case of error
//       }
//     });

//     this.groupService.getAllGroup().subscribe({
//       next: (groups) => {
//         this.groups = groups;
//       },
//       error: (err) => {
//         console.error('Error fetching groups', err);
//       }
//     });
//   }

//   navigateToUserHome() {
//     this.router.navigate(['/user-home']);
//   }

//   validateFormField(form: NgForm, fieldName: string) {
//     const control = form.controls[fieldName];
    
//     if (control) {
//       control.markAsTouched();
//       if (control.valid) {
//         control.markAsUntouched();
//       }
//     }
//   }

//   validateMembersField(form: NgForm) {
//     if (this.selectedUsers.length) {
//       form.controls['members'].setErrors(null);
//     } else {
//       form.controls['members'].setErrors({ required: true });
//     }
//   }

//   onItemSelect(item: any, form: NgForm) {
//     this.addGroupRequest.memberIds.push(item.id);
//     console.log(this.addGroupRequest.memberIds);
//     this.validateMembersField(form);
//   }

//   onSelectAll(items: any, form: NgForm) {
//     this.addGroupRequest.memberIds = items.map((item: User) => item.id);
//     console.log(this.addGroupRequest.memberIds);
//     this.validateMembersField(form);
//   }

//   logout(): void {
//     this.authService.logout();
//     this.router.navigate(['/login']);
//   }

//   createGroup(form: NgForm) {
//     if (form.invalid) {
//       this.markFormTouched(form);
//       return;
//     }

//     console.log(this.addGroupRequest);
//     this.groupService.addGroup(this.addGroupRequest).subscribe({
//       next: (group) => {
//         console.log(group);
//         // alert('Group created Successfully');
//         this.toastr.success('Group created Successfully', 'Success'); // Toastr notification
//         setTimeout(() => {
//           this.router.navigate(['user-home']);
//         }, 3000); // Navigate after 3 seconds
//       },
//       error: (err) => {
//         console.error('Error creating group', err);
//         this.toastr.error('Failed to create group', 'Error');
//       }
//     });
//   }

//   markFormTouched(form: NgForm) {
//     Object.keys(form.controls).forEach(field => {
//       const control = form.controls[field];
//       control.markAsTouched({ onlySelf: true });
//     });
//   }
// }

