import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultService } from '../api/api/default.service';
import { DriverService } from '../api/api/driver.service';
import { DispatcherService } from '../api/api/dispatcher.service';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-ride-detail-modal',
  templateUrl: './ride-detail-modal.component.html',
  styleUrls: ['./ride-detail-modal.component.css']
})
export class RideDetailModalComponent implements OnInit {
  @Input() ride: any = {};
  @Input() isDriveTo: boolean; // show driveTo or driveFrom details
  @Output() closeModalClick = new EventEmitter<boolean>();
  apptTypes: any;
  clinics: any;

  constructor( private ds: DefaultService,
               private driverService: DriverService,
               private dispatcherService: DispatcherService,
               public constants: Constants ) { }

  ngOnInit() {
    this.apptTypes = this.constants.APPT_TYPES;
    // TODO: Refactor so we cache this data
    this.getClinics();
  }

/*********************************************************************
                          Service Calls
**********************************************************************/
  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      this.clinics = c.reduce((map, obj) => (map[obj.id] = obj, map), {});
    });
  }

/*********************************************************************
                            Click Handlers
**********************************************************************/
  handleCloseModalClick() {
    this.closeModalClick.emit(true);
  }

  handleApplyClick() {
    console.log("--You applied!");
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.driverService.addDriveApplicant({"driveId": id}).subscribe(res => {
      console.log("Added drive applicant", res);
    });
  }

  handleApproveClick(applicant: any) {
    console.log("--Approved", applicant);
    // TODO: Post approval to server
  }
}
