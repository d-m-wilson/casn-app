import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskModule } from 'ngx-mask';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
/* Angular Material Components */
import {
  MatAutocompleteModule,
  MatBadgeModule,
  MatButtonModule,
  MatCardModule,
  MatCheckboxModule,
  MatDatepickerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatSelectModule,
  MatSidenavModule,
  MatSlideToggleModule,
  MatToolbarModule,
} from '@angular/material';
/* Routing */
import { AppRoutingModule } from './app-routing.module';
/* Custom Services & HTTP Interceptors */
import { Constants } from './app.constants';
import { AuthGuard } from './auth-services/auth-guard.service';
import { AuthenticationService } from './auth-services/auth.service';
//import { fakeBackendProvider } from './auth-services/fake-backend';
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
import { AppConfigService, appConfigInitializerFn } from './auth-services/appconfig.service';

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
    MatBadgeModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatToolbarModule,
  ],
  providers: [
    AppConfigService,
    { provide: APP_INITIALIZER, useFactory: appConfigInitializerFn, multi: true, deps: [ AppConfigService ] },
    Constants,
    ApiModule,
    AuthGuard,
    AuthenticationService,
    UserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
