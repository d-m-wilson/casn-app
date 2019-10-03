import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { DriverApiService } from '../api/api/driverApi.service';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';

@Component({
  selector: 'app-ride-detail-modal',
  templateUrl: './ride-detail-modal.component.html',
  styleUrls: ['./ride-detail-modal.component.scss']
})
export class RideDetailModalComponent implements OnInit {
  userRole: string;
  @Input() ride: any = {};
  @Input() isDriveTo: boolean; // show driveTo or driveFrom details
  @Output() closeRideModalClick = new EventEmitter<boolean>();
  @Output() closeRideModalAndUpdateClick = new EventEmitter<boolean>();
  driveType: string;
  apptTypes: any;
  clinics: any;
  volunteers: any[];
  // Details for Cancel Drive Modal
  showCancelDriveModal: boolean = false;
  driveToCancel: string;

  constructor( private ds: DefaultApiService,
               private driverService: DriverApiService,
               private dispatcherService: DispatcherApiService ) { }

  ngOnInit() {
    this.userRole = localStorage.getItem('userRole');
    if(this.userRole === '1') this.getVolunteers();
    this.getAppointmentTypes();
    this.getClinics();
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

  getAppointmentTypes(): void {
    this.ds.getAppointmentTypes().subscribe(a => {
      console.log("appt TYPESSSS", a)
      this.apptTypes = a.reduce((acc, cur) => {
        acc[cur.id] = { title: cur.title };
        return acc;
      }, {});
      // TODO: Remove once API is ready.
      this.apptTypes["3"].estimatedDurationMinutes = 210;
      this.apptTypes["4"].estimatedDurationMinutes = 150;
      this.apptTypes["5"].estimatedDurationMinutes = 90;
      this.apptTypes["6"].estimatedDurationMinutes = 180;
      this.apptTypes["7"].estimatedDurationMinutes = 60;
      this.apptTypes["8"].estimatedDurationMinutes = 30;
      console.log("appt Types", this.apptTypes);
      //3 Surgical: 3.5 hours (210 minutes)
      //4 Ultrasound: 2.5 hours (150 minutes)
      //5 Lam Insert: 1.5 hours (90 minutes)
      //6 Lam to Complete: 3 hours (180 minutes)
      //7 Courthouse Appointment: 1 hour (60 minutes)
      //8 Follow Up: .5 hours (30 minutes)
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

  handleCancelDriveClick() {
    this.driveToCancel = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.showCancelDriveModal = true;
  }

  hideCancelDriveModal(update?: boolean) {
    this.driveToCancel = null;
    this.showCancelDriveModal = false;
    if(update) this.closeRideModalAndUpdateClick.emit(true);
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

  getAppointmentEndTime(apptTime, apptType) {
    const date = new Date(apptTime);
    const minutes = this.apptTypes[apptType].estimatedDurationMinutes;
    return new Date(date.getTime() + minutes*60000);
  }
}
