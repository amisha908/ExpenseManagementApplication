import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  UserName: string | null = null;
  userId: string | null = null;
  isLoginPage: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this.router.events.subscribe(() => {
      this.isLoginPage = this.router.url === '/login';
    });
  }

  ngOnInit(): void {
    // this.userId = this.authService.getUserId();
    this.authService.userName$.subscribe(userName => {
      this.UserName = userName;
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}


// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';

// @Component({
//   selector: 'app-nav-bar',
//   templateUrl: './nav-bar.component.html',
//   styleUrl: './nav-bar.component.css'
// })
// export class NavBarComponent implements OnInit {
//   UserName: string | null = null;
//   userId: string | null = null;
//   isLoginPage: boolean = false;

//   constructor(
//     private authService: AuthService,
//     private router: Router
   
      
    
//   ) {
//     this.router.events.subscribe(() => {
//       this.isLoginPage = this.router.url === '/login';
//     });
//   }

//   ngOnInit(): void {
   
//     this.userId = this.authService.getUserId();
//     // this.UserName = this.authService.getUserName();
//     this.loadName();


    
//     console.log(this.UserName);
//   }
//   loadName() {
//     this.UserName = this.authService.getUserName();
//   }
  
//   logout(): void {
//     this.authService.logout();
//     this.router.navigate(['/login']);
//   }
// }


// import { Component, OnInit } from '@angular/core';
// import { ExpenseService } from '../../services/expense.service';
// import { AuthService } from '../../services/auth.service';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-nav-bar',
//   templateUrl: './nav-bar.component.html',
//   styleUrl: './nav-bar.component.css'
// })

// export class NavBarComponent implements OnInit{

//   userName: string | null = null;
//   userId: string | null = null;

//   constructor(
//     private expenseService: ExpenseService, 
//     private authService: AuthService,
//     private router: Router
//   ) {}

//   ngOnInit(): void {
//     this.userId = this.authService.getUserId();
//     this.userName = this.authService.getUserName();
//     console.log(this.userName);
//   }
  
//   logout(): void {
//     this.authService.logout();
//     this.router.navigate(['/login']);
//   }
// }
