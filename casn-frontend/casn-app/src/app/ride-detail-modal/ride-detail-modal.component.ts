import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultService } from '../api/api/default.service';
import { DriverService } from '../api/api/driver.service';
import { DispatcherService } from '../api/api/dispatcher.service';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-ride-detail-modal',
  templateUrl: './ride-detail-modal.component.html',
  styleUrls: ['./ride-detail-modal.component.scss']
})
export class RideDetailModalComponent implements OnInit {
  @Input() ride: any = {};
  @Input() isDriveTo: boolean; // show driveTo or driveFrom details
  @Output() closeRideModalClick = new EventEmitter<boolean>();
  @Output() closeRideModalAndUpdateClick = new EventEmitter<boolean>();
  driveType: string;
  apptTypes: any;
  clinics: any;
  volunteers: any[];

  constructor( private ds: DefaultService,
               private driverService: DriverService,
               private dispatcherService: DispatcherService,
               public constants: Constants ) { }

  ngOnInit() {
    this.apptTypes = this.constants.APPT_TYPES;
    this.getClinics();
    this.getVolunteers();
    this.driveType = this.isDriveTo ? 'driveTo' : 'driveFrom';
  }

/*********************************************************************
                          Service Calls
**********************************************************************/
  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      this.clinics = c.reduce((map, obj) => (map[obj.id] = obj, map), {});
    });
  }

  getVolunteers(): void {
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.dispatcherService.getVolunteerDrives(id).subscribe(
      res => {
        if(res.length > 0) this.volunteers = res;
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
    this.closeRideModalClick.emit(true);
  }

  handleApplyClick() {
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.driverService.addDriveApplicant({"driveId": id}).subscribe(
      res => {
        this.closeRideModalAndUpdateClick.emit(true);
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
        this.closeRideModalAndUpdateClick.emit(true);
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
    const driverId = this.ride[this.driveType].driverId
    return !!driverId;
  }
}
