import { Component, OnInit, ViewChild } from '@angular/core';
import { DefaultService } from '../api/api/default.service';
import { RideDetailModalComponent } from '../ride-detail-modal/ride-detail-modal.component';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css']
})
export class RidesComponent implements OnInit {
  objectKeys: any = Object.keys;
  startDate: string;
  endDate: string;
  activeDate: string;
  datesForDateRange: any[]; // All dates from startDate to endDate
  rides: any;
  clinics: any;
  apptTypes: any;

  // Settings Modal
  showSettingsModal: boolean = false;
  // Display flags for rides. 0=open, 1=pending, 2=approved
  displayRides: boolean[] = [true, true, true];

  // Ride Modal
  displayRideModal: boolean = false;
  rideModalContent: any;
  @ViewChild(RideDetailModalComponent)
  rideModal: RideDetailModalComponent;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultService,
               public constants: Constants ) { }

  ngOnInit() {
    this.apptTypes = this.constants.APPT_TYPES;
    this.setDateRange();
    this.getClinics();
    this.getRides();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getRides(): void {
    this.ds.getAllAppointments(this.startDate, this.endDate).subscribe(appts => {
      appts = appts.sort((a,b) => new Date(a.appointment.appointmentDate).valueOf() - new Date(b.appointment.appointmentDate).valueOf());
      this.rides = {};
      this.datesForDateRange.forEach(day => this.rides[day] = []);
      appts.forEach(a => {
        const day = a.appointment.appointmentDate.toString().slice(0,10);
        this.rides[day].push(a);
      });
    });
  }

  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      this.clinics = c.reduce((map, obj) => (map[obj.id] = obj, map), {});
    });
  }

/*********************************************************************
                            Click Handlers
**********************************************************************/
  toggleRideModal(ride?: any): void {
    this.displayRideModal = !this.displayRideModal;
    ride ? this.rideModalContent = ride : this.rideModalContent = null;
  }

  toggleSettingsModal(): void {
    this.showSettingsModal = !this.showSettingsModal;
  }

  setActiveDate(date: string): void {
    this.activeDate = date;
  }

  handleChangeWeekClick(changeType: string): void {
    if(changeType === 'prev') this.setDateRange(this.addDays(this.startDate, -6));
    if(changeType === 'next') this.setDateRange(this.addDays(this.endDate, 1));
    if(changeType == 'today') this.setDateRange();
    this.getRides();
  }

/*********************************************************************
                              Utilities
**********************************************************************/
  setDateRange(date?: any): void {
    const currentDate = date || new Date();
    this.startDate = this.addDays(currentDate, -currentDate.getDay()).toISOString().slice(0,10);
    this.endDate = this.addDays(this.startDate, 6).toISOString().slice(0,10);
    this.getDatesForDateRange();
    this.activeDate = currentDate.toISOString().slice(0,10);
  }

  private addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }

  private getDatesForDateRange(): void {
    this.datesForDateRange = [];
    let dateArray = new Array();
    let currentDate = new Date(this.startDate.valueOf());
    for(let i = 0; i < 7; i++) {
      this.datesForDateRange.push((new Date(currentDate)).toISOString().slice(0,10));
      currentDate.setDate(currentDate.getDate() + 1);
    }
  }

  getStatusIcon(status: number): string {
    switch(status) {
      case 0: return "panorama_fish_eye";
      case 1: return "timelapse";
      case 2: return "check_circle";
      default: return "";
    }
  }

  getStatusText(status: number): string {
    switch(status) {
      case 0: return "Apply Now!";
      case 1: return "Pending Approval"; // TODO: Will be affected by user role
      case 2: return "Approved";
      default: return "";
    }
  }

}
