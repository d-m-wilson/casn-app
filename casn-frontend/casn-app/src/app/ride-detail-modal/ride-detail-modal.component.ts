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
  @Output() closeModalAndUpdateClick = new EventEmitter<boolean>();
  apptTypes: any;
  clinics: any;
  volunteers: any[];

  constructor( private ds: DefaultService,
               private driverService: DriverService,
               private dispatcherService: DispatcherService,
               public constants: Constants ) { }

  ngOnInit() {
    this.apptTypes = this.constants.APPT_TYPES;
    // TODO: Refactor so we cache this data
    this.getClinics();
    this.getVolunteers();
  }

/*********************************************************************
                          Service Calls
**********************************************************************/
  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      console.log("--Calling getClinics (TODO: needs to be cached)");
      this.clinics = c.reduce((map, obj) => (map[obj.id] = obj, map), {});
    });
  }

  getVolunteers(): void {
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.dispatcherService.getVolunteerDrives(id).subscribe(
      res => {
        this.volunteers = res;
      },
      err => {
        // TODO: Handle error
        console.error("ERROR:", err);
      }
    );
  }

/*********************************************************************
                            Click Handlers
**********************************************************************/
  handleCloseModalClick() {
    this.closeModalClick.emit(true);
  }

  handleApplyClick() {
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.driverService.addDriveApplicant({"driveId": id}).subscribe(
      res => {
        console.log("SUCCESS. Added drive applicant", res);
        this.closeModalAndUpdateClick.emit(true);
      },
      err => {
        // TODO: Handle error
        console.error("ERROR:", err);
      }
    );
  }

  handleApproveClick(volunteerId: number) {
    this.dispatcherService.addDriver({volunteerDriveId: volunteerId}).subscribe(
      res => {
        console.log("SUCCESS. Approved volunteer", res);
        this.closeModalAndUpdateClick.emit(true);
      },
      err => {
        // TODO: Handle error
        console.error("ERROR:", err);
      }
    );
  }

/*********************************************************************
                              Utilities
**********************************************************************/
  get startAddressGoogleMapLink() {
    const drive = this.isDriveTo ? 'driveTo' : 'driveFrom';
    const query = `${this.ride[drive].startAddress} ${this.ride[drive].startCity} ${this.ride[drive].startPostalCode}`;
    const urlEncodedQuery = encodeURI(query);
    return `https://www.google.com/maps/search/?api=1&query=${urlEncodedQuery}`;
  }

  get endAddressGoogleMapLink() {
    const drive = this.isDriveTo ? 'driveTo' : 'driveFrom';
    const query = `${this.ride[drive].endAddress} ${this.ride[drive].endCity} ${this.ride[drive].endPostalCode}`;
    const urlEncodedQuery = encodeURI(query);
    return `https://www.google.com/maps/search/?api=1&query=${urlEncodedQuery}`;
  }

  get driveIsApproved() {
    const driverId = this.isDriveTo ? this.ride.driveTo.driverId : this.ride.driveFrom.driverId;
    return !!driverId;
  }

}
