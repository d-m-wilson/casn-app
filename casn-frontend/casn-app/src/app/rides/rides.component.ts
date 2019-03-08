import { Component, OnInit } from '@angular/core';
import { DefaultService } from '../api/api/default.service';
import { RideDetailModalComponent } from '../ride-detail-modal/ride-detail-modal.component';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.scss']
})
export class RidesComponent implements OnInit {
  objectKeys: any = Object.keys;
  startDate: string;
  endDate: string;
  activeDate: string;
  datesToDisplay: any[]; // All dates from startDate to endDate
  rides: any[];
  ridesToDisplay: any[]; // Subset of rides, may have filters applied
  clinics: any;
  apptTypes: any;

  /************** Settings Modal **************/
  showSettingsModal: boolean = false;
  showDateFilters: boolean = false;
  // For showing badges on date filter cards
  dateFilterProperties: any = {};
  // Display flags for rides. 0=open, 1=pending, 2=approved
  displayRides: boolean[] = [true, true, true];
  // Display flags for clinics
  displayClinics: any = {};

  /************** Ride Modal **************/
  displayRideModal: boolean = false;
  rideModalContent: any;
  showRideModalDriveTo: boolean; // Show driveTo or driveFrom details

  /*************** Map Modal *************/
  displayMapModal: boolean = false;

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
      this.rides = appts;
      this.ridesToDisplay = appts;
      this.updateDateFilterProperties();
    });
  }

  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      this.clinics = c.reduce((map, obj) => (map[obj.id] = obj, map), {});
      this.objectKeys(this.clinics).forEach(c => this.displayClinics[c] = true);
    });
  }

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
  toggleRideModal(ride?: any, isDriveTo?: boolean): void {
    this.displayRideModal = !this.displayRideModal;
    ride ? this.rideModalContent = ride : this.rideModalContent = null;
    this.showRideModalDriveTo = isDriveTo;
  }

  toggleSettingsModal(): void {
    this.showSettingsModal = !this.showSettingsModal;
  }

  toggleMapModal(): void {
    this.displayMapModal = !this.displayMapModal;
  }

  toggleActiveDate(date: string): void {
    if(this.activeDate === date) {
      /* This means the user tapped the currently selected activeDate, so
      we toggle off the activeDate filter and show the full week of rides. */
      this.activeDate = null;
      this.ridesToDisplay = this.rides;
    } else {
      this.activeDate = date;
      this.ridesToDisplay = this.rides.filter(r => r.appointment.appointmentDate.slice(0,10) === this.activeDate);
    }
  }

  toggleDateFilters(): void {
    this.showDateFilters = !this.showDateFilters;
    // If a date filter was applied, remove it.
    this.activeDate = null;
    this.ridesToDisplay = this.rides;
  }

  handleChangeWeekClick(changeType: string): void {
    if(changeType === 'prev') this.setDateRange(this.addDays(this.startDate, -6));
    if(changeType === 'next') this.setDateRange(this.addDays(this.endDate, 1));
    this.getRides();
  }

  onMapDriveDetailsClick(event: any): void {
    const isDriveTo = event.driveType === 'driveTo';
    this.toggleRideModal(event.ride, isDriveTo);
  }

  /*********************************************************************
                                Utilities
  **********************************************************************/
  setDateRange(date?: any): void {
    const currentDate = date || new Date();
    this.startDate = this.addDays(currentDate, -currentDate.getDay()).toISOString().slice(0,10);
    this.endDate = this.addDays(this.startDate, 6).toISOString().slice(0,10);
    this.getDatesForDateRange();
    this.activeDate = null;
  }

  private addDays(date, days) {
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }

  private getDatesForDateRange(): void {
    this.datesToDisplay = [];
    let currentDate = new Date(this.startDate.valueOf());
    for(let i = 0; i < 7; i++) {
      this.datesToDisplay.push((new Date(currentDate)).toISOString().slice(0,10));
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
      case 1: return "Pending"; // TODO: Will be affected by user role
      case 2: return "Approved";
      default: return "";
    }
  }

  updateDateFilterProperties(): void {
    this.datesToDisplay.forEach(d => {
      this.dateFilterProperties[d] = {};
      /* Find number of appts per day. This isn't very efficient,
      so we may need to optimize if the number of appts in the rides array
      becomes large. Not a concern for the foreseeable future. */
      this.dateFilterProperties[d].numApptsThisDay = this.rides.filter(r => r.appointment.appointmentDate.slice(0,10) === d).length;
    })
  }

}
