// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupManagementComponent } from './components/group-management/group-management.component';
import { ExpenseManagementComponent } from './components/expense-management/expense-management.component';
import { ExpenseSettlementComponent } from './components/expense-settlement/expense-settlement.component';
import { LoginComponent } from './components/login/login.component';
import { UserHomeComponent } from './components/user-home/user-home.component';
import { GroupDetailComponent } from './components/group-detail/group-detail.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { AuthGuard } from './guards/auth.service';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';


const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'group-management', component: GroupManagementComponent, canActivate:[AuthGuard] },
  { path: 'expense-management/:id', component: ExpenseManagementComponent, canActivate:[AuthGuard] },
  { path: 'expense-settlement', component: ExpenseSettlementComponent, canActivate:[AuthGuard] },
  { path: 'user-home', component: UserHomeComponent, canActivate:[AuthGuard]},
  { path: 'group/:id', component: GroupDetailComponent, canActivate:[AuthGuard] },
  { path: 'nav-bar', component:NavBarComponent, canActivate:[AuthGuard]},
  {path:'admin-home', component:AdminHomeComponent, canActivate:[AuthGuard]}
  
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


// import { NgModule } from '@angular/core';
// import { RouterModule, Routes } from '@angular/router';

// const routes: Routes = [];

// @NgModule({
//   imports: [RouterModule.forRoot(routes)],
//   exports: [RouterModule]
// })
// export class AppRoutingModule { }
