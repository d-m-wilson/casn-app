import { Component, OnInit } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { Constants } from '../app.constants';
import { DriverApiService } from '../api';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-my-drives',
  templateUrl: './my-drives.component.html',
  styleUrls: ['./my-drives.component.scss']
})
export class MyDrivesComponent implements OnInit {
  loading: boolean = false;
  objectKeys: any = Object.keys;
  userRole: string;
  startDate: string;
  startDateLong: Date;
  endDate: string;
  endDateLong: Date;
  activeDate: string;
  datesToDisplay: any[]; // All dates from startDate to endDate
  rides: any[];
  ridesToDisplay: any[]; // Subset of rides, may have filters applied
  serviceProviders: any;
  apptTypes: any;
  driveStatuses: any;

  /************** Settings Modal **************/
  showSettingsModal: boolean = false;
  showDateFilters: boolean = false;
  // For showing badges on date filter cards
  dateFilterProperties: any = {};
  // Display flags for rides. 0=open, 1=pending, 2=approved, 3=cancelled
  displayRides: boolean[] = [false, false, true, true];
  // Display flags for service providers
  displayServiceProviders: any = {};

  /************** Ride Modal **************/
  displayRideModal: boolean = false;
  rideModalContent: any;
  showRideModalDriveTo: boolean; // Show driveTo or driveFrom details

  /*************** Map Modal *************/
  displayMapModal: boolean = false;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultApiService,
               private driverService: DriverApiService,
               public constants: Constants,
               private datePipe: DatePipe ) { }

  ngOnInit() {
    this.userRole = localStorage.getItem("userRole");
    this.setDateRange();
    this.getAppointmentTypes();
    this.getServiceProviders();
    this.getRides();
    this.getDriveStatuses();

    const showDateFilters = JSON.parse(localStorage.getItem("showDateFilters"));
    if(showDateFilters) this.toggleDateFilters();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getDriveStatuses(): void {
    this.ds.getDriveStatuses().subscribe(s => {
      this.driveStatuses = s.map(i => i.name);
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

  getRides(): void {
    this.loading = true;
    this.driverService.getMyDrives(this.startDate, this.endDate).subscribe(
      appts => {
        this.loading = false
        appts = appts.sort((a,b) => new Date(a.appointment.appointmentDate).valueOf() - new Date(b.appointment.appointmentDate).valueOf());
        this.rides = appts;
        console.log("Rides")
        this.ridesToDisplay = appts;
        this.updateDateFilterProperties();
        console.log("Rides:", this.rides);
      },
      err => {
        this.loading = false;
        // TODO: Handle Error
        console.error("Error fetching rides", err);
      }
    );
  }

  getServiceProviders(): void {
    this.ds.getServiceProviders().subscribe(p => {
      this.serviceProviders = p.reduce((map, obj) => (map[obj.id] = obj, map), {});
      // NOTE: All courthouses are displayed/hidden with a single toggle.
      // The rest of the service providers are toggled on/off individually.
      this.displayServiceProviders['courthouses'] = true;
      this.objectKeys(this.serviceProviders).forEach(s => {
        if(s.serviceProviderTypeId !== 2) this.displayServiceProviders[s] = true;
      });
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
    localStorage.setItem("showDateFilters", JSON.stringify(this.showDateFilters));
    // If a date filter was applied, remove it.
    this.activeDate = null;
    this.ridesToDisplay = this.rides;
  }

  handleChangeWeekClick(changeType: string): void {
    if(changeType === 'prev') this.setDateRange(this.addDays(this.endDateLong, -7));
    if(changeType === 'next') this.setDateRange(this.addDays(this.endDateLong, 7));
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
    this.startDateLong = this.addDays(currentDate, -currentDate.getDay());
    this.startDate = this.datePipe.transform(this.startDateLong, 'yyyy-MM-dd');
    this.endDateLong = this.addDays(this.startDate, 7);
    this.endDate = this.datePipe.transform(this.endDateLong, 'yyyy-MM-dd');
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
    let currentDate = new Date(this.startDateLong);
    for(let i = 0; i < 7; i++) {
      this.datesToDisplay.push(this.datePipe.transform(currentDate, 'yyyy-MM-dd'));
      currentDate.setDate(currentDate.getDate() + 1);
    }
  }

  getStatusIcon(status: number): string {
    switch(status) {
      case 0: return "panorama_fish_eye";
      case 1: return "timelapse";
      case 2: return "check_circle";
      case 3: return "block"
      default: return "";
    }
  }

  getStatusText(status: number): string {
    switch(status) {
      case 0: return "Apply Now!";
      case 1: return "Pending";
      case 2: return "Approved";
      case 3: return "Canceled"
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

  getAppointmentEndTime(apptTime, apptType) {
    const date = new Date(apptTime);
    const minutes = this.apptTypes[apptType].estimatedDurationMinutes;
    return new Date(date.getTime() + minutes*60000);
  }

  swipe(action: string) {
    if(action === 'swiperight') this.handleChangeWeekClick('next');
    if(action === 'swipeleft') this.handleChangeWeekClick('prev');
  }

  providerIsDisplayed(providerId: string|number): boolean {
    // NOTE: All courthouses are toggled on/off with a single toggle
    // If it's a courthouse, check if courthouses are displayed
    if(this.serviceProviders[providerId].serviceProviderTypeId === 2) {
      return this.displayServiceProviders['courthouses'];
    }
    // Otherwise, just check if the individual provider is displayed
    return this.displayServiceProviders[providerId]
  }

}
