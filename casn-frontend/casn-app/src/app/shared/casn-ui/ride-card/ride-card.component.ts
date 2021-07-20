import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-ride-card',
  templateUrl: './ride-card.component.html',
  styleUrls: ['./ride-card.component.scss']
})
export class RideCardComponent implements OnInit {
  // TODO: Create ride view-model & type-check
  @Input() ride: any;
  @Input() appointmentType: string = "None provided";
  @Input() serviceProviderType: string = "None provided";
  @Input() serviceProviderName: string = "None provided";
  @Input() appointmentEndTime: Date;
  // TODO: Refactor boolean
  @Output() showRideDetail = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit(): void { }
}
