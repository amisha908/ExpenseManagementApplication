// src/app/app.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ExpenseManagement';
  isLoggedIn: boolean = false; // Initialize based on authentication status

  // Example method for login action (replace with actual implementation)
  login() {
    // Example logic for login, set isLoggedIn to true on successful login
    this.isLoggedIn = true;
}
}


// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-root',
//   templateUrl: './app.component.html',
//   styleUrl: './app.component.css'
// })
// export class AppComponent {
//   title = 'UI';
// }
