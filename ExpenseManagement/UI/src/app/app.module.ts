// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { GroupManagementComponent } from './components/group-management/group-management.component';
import { ExpenseManagementComponent } from './components/expense-management/expense-management.component';
import { ExpenseSettlementComponent } from './components/expense-settlement/expense-settlement.component';
import { LoginComponent } from './components/login/login.component';
import { AllGroupsComponent } from './components/all-groups/all-groups.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { GroupDetailComponent } from './components/group-detail/group-detail.component';
import { UniquePipe } from './unique.pipe';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';




@NgModule({
  declarations: [
    AppComponent,
    GroupManagementComponent,
    ExpenseManagementComponent,
    ExpenseSettlementComponent,
    LoginComponent,
    AllGroupsComponent,
    UserHomeComponent,
    GroupDetailComponent,
    UniquePipe,
    NavBarComponent,
    AdminHomeComponent,
    
  
    
    
    
    
    
    
   ],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    MatCardModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatIconModule,
    MatButtonModule,
    HttpClientModule,
    AppRoutingModule,
    NgMultiSelectDropDownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right', // Position the toastr notification
      preventDuplicates: true, // Prevent duplicate toastr notifications
      progressBar: true, // Enable a progress bar
      closeButton: true // Show close button
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }


// import { NgModule } from '@angular/core';
// import { BrowserModule } from '@angular/platform-browser';

// import { AppRoutingModule } from './app-routing.module';
// import { AppComponent } from './app.component';
// import { GroupManagementComponent } from './components/group-management/group-management.component';

// @NgModule({
//   declarations: [
//     AppComponent,
//     GroupManagementComponent
//   ],
//   imports: [
//     BrowserModule,
//     AppRoutingModule
//   ],
//   providers: [],
//   bootstrap: [AppComponent]
// })
// export class AppModule { }
