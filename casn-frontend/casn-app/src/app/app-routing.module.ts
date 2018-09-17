import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// import { AuthGuard } from './auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ScheduleRideComponent } from './schedule-ride/schedule-ride.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'schedule-ride', component: ScheduleRideComponent },
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
