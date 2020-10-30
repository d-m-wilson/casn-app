import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

/* Angular Material */
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';

import { RideCardComponent } from './casn-ui/ride-card/ride-card.component';
import { RideCTAPipe } from './pipes/ride-cta.pipe';
import { RidesHeaderComponent } from './casn-ui/rides-header/rides-header.component';
import { SafeUrlPipe } from './pipes/safe-url.pipe';
import { PhonePipe } from './pipes/phone.pipe';
import { MatVerticalStepperScrollerDirective } from './directives/mat-vertical-stepper.directive';


@NgModule({
  declarations: [
    MatVerticalStepperScrollerDirective,
    SafeUrlPipe,
    PhonePipe,
    RideCardComponent,
    RideCTAPipe,
    RidesHeaderComponent,
  ],
  imports: [
    CommonModule,

    /* Angular Material */
    MatAutocompleteModule,
    MatBadgeModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
  ],
  exports: [
    MatVerticalStepperScrollerDirective,
    SafeUrlPipe,
    PhonePipe,
    RideCTAPipe,
    RideCardComponent,
    RidesHeaderComponent,

    /* Angular Material */
    MatAutocompleteModule,
    MatBadgeModule,
    MatButtonModule,
    MatCardModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule,
    MatSidenavModule,
    MatSlideToggleModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
  ]
})
export class SharedModule { }
