import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth-services/auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RidesComponent } from './rides/rides.component';
import { CallersComponent } from './callers/callers.component';
import { LoginComponent } from './login/login.component';
import { AppointmentFormComponent } from './appointment-form/appointment-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full', canActivate: [ AuthGuard ] },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [ AuthGuard ] },
  { path: 'caller', component: CallersComponent, canActivate: [ AuthGuard ] },
  { path: 'appointment', component: AppointmentFormComponent, canActivate: [ AuthGuard ] },
  { path: 'view-schedule', component: RidesComponent, canActivate: [ AuthGuard ] },
  { path: '**', component: DashboardComponent, canActivate: [ AuthGuard ] }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
