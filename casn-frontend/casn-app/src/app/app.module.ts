import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskModule } from 'ngx-mask';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
/* Angular Material Components */
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatDatepickerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatMenuModule,
  MatNativeDateModule,
  MatSelectModule,
  MatSidenavModule,
  MatToolbarModule,
} from '@angular/material';
/* Routing */
import { AppRoutingModule } from './app-routing.module';
/* Custom Services & HTTP Interceptors */
import { AuthGuard } from './auth-services/auth-guard.service';
import { AuthenticationService } from './auth-services/auth.service';
import { fakeBackendProvider } from './auth-services/fake-backend';
import { JwtInterceptor } from './auth-services/jwt.interceptor';
import { ErrorInterceptor } from './auth-services/error.interceptor';
import { UserService } from './auth-services/user.service';
import { ApiModule } from './api/api.module';
/* Custom Components */
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { PatientsComponent } from './patients/patients.component';
import { RidesComponent } from './rides/rides.component';
import { AppointmentsComponent } from './appointments/appointments.component';
import { RideDetailModalComponent } from './ride-detail-modal/ride-detail-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    PatientsComponent,
    RidesComponent,
    LoginComponent,
    AppointmentsComponent,
    RideDetailModalComponent
  ],
  imports: [
    NgxMaskModule.forRoot(),
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatNativeDateModule,
    MatSelectModule,
    MatSidenavModule,
    MatToolbarModule,
  ],
  providers: [
    ApiModule,
    AuthGuard,
    AuthenticationService,
    UserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    // provider used to create fake backend for dev
    fakeBackendProvider
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
