import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RideCardComponent } from './casn-ui/ride-card/ride-card.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RideCTAPipe } from '../pipes/ride-cta.pipe';

@NgModule({
  declarations: [
    RideCardComponent,
    RideCTAPipe,
  ],
  imports: [
    CommonModule,
    MatExpansionModule,
    MatIconModule,
    MatButtonModule,
  ],
  exports: [
    RideCardComponent,
  ]
})
export class SharedModule { }
