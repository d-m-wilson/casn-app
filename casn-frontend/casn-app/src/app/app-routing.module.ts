import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth-services/auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RidesComponent } from './rides/rides.component';
import { PatientsComponent } from './patients/patients.component'
import { LoginComponent } from './login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full', canActivate: [ AuthGuard ] },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [ AuthGuard ] },
  { path: 'patient', component: PatientsComponent, canActivate: [ AuthGuard ] },
  // { path: '', redirectTo: '/dashboard', pathMatch: 'full', canActivate: [ AuthGuard ] },
  // { path: 'dashboard', component: DashboardComponent, canActivate: [ AuthGuard ] },
  // { path: 'facts', component: FactsComponent, canActivate: [ AuthGuard ] },
  // { path: 'fact/:id', component: FactDetailComponent, canActivate: [ AuthGuard ] },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }