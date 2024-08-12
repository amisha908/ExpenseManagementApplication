import { Component, OnInit } from '@angular/core';
import { GroupService } from '../../services/group.service';
import { Group } from '../../models/group.model';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})
export class AdminHomeComponent implements OnInit {
  groups: Group[] = [];
  users: User[] = [];

  constructor(
    private groupService: GroupService,
    private userService: UserService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.groupService.getAllGroup().subscribe(
      (data) => {
        this.groups = data;
      },
      (error) => {
        console.error('Error fetching groups', error);
      }
    );

    this.userService.getAllUsers().subscribe(
      (data) => {
        this.users = data;
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }

  deleteGroup(groupId: string): void {
    if (confirm('Are you sure you want to delete this group?')) {
      this.groupService.deleteGroup(groupId).subscribe(
        () => {
          this.groups = this.groups.filter(group => group.id !== groupId);
          this.toastr.success('Group deleted successfully');
        },
        (error) => {
          console.error('Error deleting group', error);
          this.toastr.error('Failed to delete group');
        }
      );
    }
  }

}


// import { Component, OnInit } from '@angular/core';
// import { GroupService } from '../../services/group.service';
// import { Group } from '../../models/group.model';
// import { ToastrService } from 'ngx-toastr';

// @Component({
//   selector: 'app-admin-home',
//   templateUrl: './admin-home.component.html',
//   styleUrls: ['./admin-home.component.css']
// })
// export class AdminHomeComponent implements OnInit {
//   groups: Group[] = [];

//   constructor(private groupService: GroupService,private toastr: ToastrService) { }

//   ngOnInit(): void {
//     this.groupService.getAllGroup().subscribe(
//       (data) => {
//         this.groups = data;
//       },
//       (error) => {
//         console.error('Error fetching groups', error);
//       }
//     );
//   }

//   deleteGroup(groupId: string): void {
//     if (confirm('Are you sure you want to delete this group?')) {
//       this.groupService.deleteGroup(groupId).subscribe(
//         () => {
//           this.groups = this.groups.filter(group => group.id !== groupId);
//           this.toastr.success('Group deleted successfully');
//         },
//         (error) => {
//           console.error('Error deleting group', error);
//           this.toastr.error('Failed to delete group');
//         }
//       );
//     }
//   }

// }
