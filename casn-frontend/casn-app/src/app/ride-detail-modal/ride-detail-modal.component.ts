import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-ride-detail-modal',
  templateUrl: './ride-detail-modal.component.html',
  styleUrls: ['./ride-detail-modal.component.css']
})
export class RideDetailModalComponent implements OnInit {
  @Input() ride: any = {};
  @Input() isDriveTo: boolean; // Show details for driveTo or driveFrom
  @Output() closeModalClick = new EventEmitter<boolean>();

  apptTypes: any;

  constructor( public constants: Constants ) { }

  ngOnInit() {
    this.apptTypes = this.constants.APPT_TYPES;
  }

  handleCloseModalClick() {
    this.closeModalClick.emit(true);
  }
}
