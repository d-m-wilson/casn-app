import { BrowserModule, HammerModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskModule } from 'ngx-mask';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@danielmoncada/angular-datetime-picker';
import { AgmCoreModule, MapsAPILoader } from '@agm/core';
import { AgmSnazzyInfoWindowModule } from '@agm/snazzy-info-window';
import { DatePipe } from '@angular/common';
import { SharedModule } from './shared/shared.module';
/* Routing */
import { AppRoutingModule } from './app-routing.module';
/* Custom Services & HTTP Interceptors */
import { Constants } from './app.constants';
import { AuthGuard } from './auth-services/auth-guard.service';
import { AuthenticationService } from './auth-services/auth.service';
import { AppConfigService, appConfigInitializerFn } from './auth-services/appconfig.service';
import { GoogleMapConfigService } from './auth-services/google-map-config.service';
import { JwtInterceptor } from './auth-services/jwt.interceptor';
import { ErrorInterceptor } from './auth-services/error.interceptor';
import { UserService } from './auth-services/user.service';
import { ApiModule } from './api/api.module';
import { ServiceWorkerModule } from '@angular/service-worker';
/* Custom Components */
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { CallersComponent } from './callers/callers.component';
import { RidesComponent } from './rides/rides.component';
import { RideDetailModalComponent } from './ride-detail-modal/ride-detail-modal.component';
import { MapComponent } from './map/map.component';
import { environment } from '../environments/environment';
import { CancelDriveModalComponent } from './cancel-drive-modal/cancel-drive-modal.component';
import { AppointmentFormComponent } from './appointment-form/appointment-form.component';
import { UserStatsComponent } from './user-stats/user-stats.component';
import { LeaderboardComponent } from './leaderboard/leaderboard.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { MassTextComponent } from './mass-text/mass-text.component';
import { LoaderComponent } from './loader/loader.component';
import { MyDrivesComponent } from './my-drives/my-drives.component';
import { RideshareModalComponent } from './rideshare-modal/rideshare-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    CallersComponent,
    RidesComponent,
    LoginComponent,
    RideDetailModalComponent,
    MapComponent,
    CancelDriveModalComponent,
    AppointmentFormComponent,
    UserStatsComponent,
    LeaderboardComponent,
    ErrorPageComponent,
    MassTextComponent,
    LoaderComponent,
    MyDrivesComponent,
    RideshareModalComponent,
  ],
  imports: [
    NgxMaskModule.forRoot(),
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    // TODO: Move to lazy loader
    AgmCoreModule.forRoot(),
    AgmSnazzyInfoWindowModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    ServiceWorkerModule.register('./ngsw-worker.js', { enabled: environment.production }),
    HammerModule,
    SharedModule, // TODO: Remove once modules are broken up
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
    { provide: MapsAPILoader, useClass: GoogleMapConfigService },
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
