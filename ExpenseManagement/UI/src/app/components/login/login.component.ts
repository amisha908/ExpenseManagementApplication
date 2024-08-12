// src/app/components/login/login.component.ts

import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, LoginResponse } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}
  login(): void {
    this.authService.login(this.email, this.password).subscribe(
      (response: LoginResponse) => {
        if (response && response.token) {
          const userRole = this.authService.getUserRole();
          if (userRole === 'Admin') {
            this.router.navigate(['/admin-home']);
          } else {
            this.router.navigate(['/user-home']);
          }
        } else {
          this.errorMessage = 'Invalid login credentials';
        }
      },
      (error: any) => {
        this.errorMessage = error.message || 'Error logging in. Please try again later.';
      }
    );
  }
  // login(): void {
  //   this.authService.login(this.email, this.password).subscribe(
  //     (response: LoginResponse) => {
  //       if (response && response.token) {
  //         this.router.navigate(['/user-home']);
  //       } else {
  //         this.errorMessage = 'Invalid login credentials';
  //       }
  //     },
  //     (error: any) => {
  //       // Display the error message received from the AuthService
  //       this.errorMessage = error.message || 'Error logging in. Please try again later.';
  //     }
  //   );
  // }
}
