import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RideCardComponent } from './casn-ui/ride-card/ride-card.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RideCTAPipe } from '../pipes/ride-cta.pipe';
import { RidesHeaderComponent } from './casn-ui/rides-header/rides-header.component';
import { SafeUrlPipe } from '../pipes/safe-url.pipe';
import { PhonePipe } from '../pipes/phone.pipe';

@NgModule({
  declarations: [
    SafeUrlPipe,
    PhonePipe,
    RideCardComponent,
    RideCTAPipe,
    RidesHeaderComponent,
  ],
  imports: [
    CommonModule,
    MatExpansionModule,
    MatIconModule,
    MatButtonModule,
  ],
  exports: [
    SafeUrlPipe,
    PhonePipe,
    RideCardComponent,
    RidesHeaderComponent,
  ]
})
export class SharedModule { }
