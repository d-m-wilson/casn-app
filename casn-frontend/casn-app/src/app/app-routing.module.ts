import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth-services/auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { RidesComponent } from './rides/rides.component';
import { CallersComponent } from './callers/callers.component';
import { LoginComponent } from './login/login.component';
import { AppointmentFormComponent } from './appointment-form/appointment-form.component';
import { UserStatsComponent } from './user-stats/user-stats.component';
import { MassTextComponent } from './mass-text/mass-text.component';
import { MyDrivesComponent } from './my-drives/my-drives.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full', canActivate: [ AuthGuard ] },
  { path: 'login', component: LoginComponent },
  { path: 'error', component: ErrorPageComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [ AuthGuard ] },
  { path: 'caller', component: CallersComponent, canActivate: [ AuthGuard ] },
  { path: 'appointment', component: AppointmentFormComponent, canActivate: [ AuthGuard ] },
  { path: 'view-schedule', component: RidesComponent, canActivate: [ AuthGuard ] },
  { path: 'stats', component: UserStatsComponent, canActivate: [ AuthGuard ] },
  { path: 'message', component: MassTextComponent, canActivate: [ AuthGuard ] },
  { path: 'my-drives', component: MyDrivesComponent, canActivate: [ AuthGuard ]},
  { path: '**', component: ErrorPageComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
