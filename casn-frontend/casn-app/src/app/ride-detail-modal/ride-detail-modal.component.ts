import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { DriverApiService } from '../api/api/driverApi.service';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';
import { AppointmentDataService } from "../appointment-data.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-ride-detail-modal',
  templateUrl: './ride-detail-modal.component.html',
  styleUrls: ['./ride-detail-modal.component.scss']
})
export class RideDetailModalComponent implements OnInit {
  loading: boolean = false;
  userRole: string;
  @Input() ride: any = {};
  @Input() isDriveTo: boolean; // show driveTo or driveFrom details
  @Output() closeRideModalClick = new EventEmitter<boolean>();
  @Output() closeRideModalAndUpdateClick = new EventEmitter<boolean>();
  driveType: string;
  apptTypes: any;
  serviceProviders: any;
  volunteers: any[];
  // Details for Cancel Drive Modal
  showCancelDriveModal: boolean = false;
  driveToCancel: string;

  constructor( private ds: DefaultApiService,
               private driverService: DriverApiService,
               private dispatcherService: DispatcherApiService,
               private sharedApptDataService: AppointmentDataService,
               private router: Router ) { }

  ngOnInit() {
    this.userRole = localStorage.getItem('userRole');
    if(this.userRole === '1') this.getVolunteers();
    this.getAppointmentTypes();
    this.getServiceProviders();
    this.driveType = this.isDriveTo ? 'driveTo' : 'driveFrom';
  }

/*********************************************************************
                          Service Calls
**********************************************************************/
  getServiceProviders(): void {
    this.ds.getServiceProviders().subscribe(s => {
      this.serviceProviders = s.reduce((map, obj) => (map[obj.id] = obj, map), {});
    });
  }

  getAppointmentTypes(): void {
    this.ds.getAppointmentTypes().subscribe(a => {
      this.apptTypes = a.reduce((acc, cur) => {
        acc[cur.id] = {
          title: cur.title,
          estimatedDurationMinutes: cur.estimatedDurationMinutes
        };
        return acc;
      }, {});
    });
  }

  getVolunteers(): void {
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.dispatcherService.getVolunteerDrives(id).subscribe(
      res => {
        if(res.length > 0) this.volunteers = res;
        console.log("Volunteers", this.volunteers);
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
    this.loading = true;
    const id = this.isDriveTo ? this.ride.driveTo.id : this.ride.driveFrom.id;
    this.driverService.addDriveApplicant({"driveId": id}).subscribe(
      res => {
        this.loading = false;
        this.closeRideModalAndUpdateClick.emit(true);
      },
      err => {
        this.loading = false;
        if(err.status === 409) {
          alert("Our records show you've already applied for this drive. We'll contact you soon to let you know if you're approved.")
        }
        console.error("ERROR:", err);
      }
    );
  }

  handleApproveClick(volunteerId: number) {
    this.loading = true;
    this.dispatcherService.addDriver({volunteerDriveId: volunteerId}).subscribe(
      res => {
        this.loading = false;
        this.closeRideModalAndUpdateClick.emit(true);
      },
      err => {
        this.loading = false;
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

  handleUnapproveClick(driveId: number, driverName: string): void {
    if(confirm(`This will remove ${driverName} from this drive. Are you sure?`)) {
      this.loading = true;
      this.dispatcherService.unapproveDriver({driveId}).subscribe(
        res => {
          this.loading = false;
          this.closeRideModalAndUpdateClick.emit(true);
        },
        err => {
          this.loading = false;
          // TODO: Handle error
          console.error("ERROR:", err);
        }
      )
    }
  }

  handleDenyClick(volunteerDriveId: number, driverName: string): void {
    if(confirm(`This will remove ${driverName}'s application from this drive. Are you sure?`)) {
      this.loading = true;
      this.dispatcherService.denyDriver({volunteerDriveId}).subscribe(
        res => {
          this.loading = false;
          this.closeRideModalAndUpdateClick.emit(true);
        },
        err => {
          this.loading = false;
          // TODO: Handle error
          console.error("ERROR:", err);
        }
      )
    }
  }

  handleRetractClick(driveId: number): void {
    if(confirm(`If you do this, you will no longer be applied for this drive. Are you sure?`)) {
      this.loading = true;
      this.driverService.removeDriveApplicant({driveId}).subscribe(
        res => {
          this.loading = false;
          this.closeRideModalAndUpdateClick.emit(true);
        },
        err => {
          this.loading = false;
          // TODO: Handle error
          console.error("ERROR:", err);
        }
      )
    }
  }

  editAppointment(): void {
    // Pass the appt data to a data sharing service to populate the edit forms.
    this.sharedApptDataService.changeMessage(this.ride);
    // Navigate to the edit forms.
    this.router.navigate(['/caller', {
      callerIdentifier: this.ride.caller.callerIdentifier,
      appointmentId: this.ride.appointment.id
    }]);
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
