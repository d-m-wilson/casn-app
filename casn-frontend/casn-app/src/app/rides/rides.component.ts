import { Component, OnInit, OnDestroy } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { Constants } from '../app.constants';
import { DatePipe } from '@angular/common';
import { DateUtilityService } from '../shared/utils/date-utility.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.scss']
})
export class RidesComponent implements OnInit, OnDestroy {
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
  // Display flags for rides. 0=open, 1=pending, 2=approved, 3=cancelled, 4=rideshare
  displayRides: boolean[] = [true, true, true, true, true];
  activeTab: string = "all";
  // Display flags for service providers
  displayServiceProviders: any = {};

  /************** Ride Modal **************/
  displayRideModal: boolean = false;
  rideModalContent: any;
  showRideModalDriveTo: boolean; // Show driveTo or driveFrom details

  /*************** Map Modal *************/
  displayMapModal: boolean = false;

  dateUtilSubscription;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultApiService,
               public constants: Constants,
               private datePipe: DatePipe,
               private dateUtils: DateUtilityService ) { }

  ngOnInit() {
    this.userRole = localStorage.getItem("userRole");
    this.getDateConfig();
    this.getAppointmentTypes();
    this.getServiceProviders();
    this.getDriveStatuses();

    const showDateFilters = JSON.parse(localStorage.getItem("showDateFilters"));
    if(showDateFilters) this.toggleDateFilters();
  }

  ngOnDestroy() {
    this.dateUtilSubscription.unsubscribe();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getDateConfig(): void {
    this.dateUtils.setDateRange();
    // TODO: Put this in a config object & type-check it
    this.dateUtilSubscription = this.dateUtils.dateConfig.subscribe(d => {
      this.startDate = d.startDate;
      this.startDateLong = d.startDateLong;
      this.endDate = d.endDate;
      this.endDateLong = d.endDateLong;
      this.datesToDisplay = d.datesToDisplay;
      this.activeDate = d.activeDate;
      this.getRides();
    });
  }

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
    this.ds.getAppointments(this.startDate, this.endDate).subscribe(
      appts => {
        this.loading = false
        appts = appts.sort((a,b) => new Date(a.appointment.appointmentDate).valueOf() - new Date(b.appointment.appointmentDate).valueOf());
        this.rides = appts;
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
  toggleRideModal(isDriveTo?: boolean, ride?: any): void {
    console.log("event", isDriveTo);
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
    if(changeType === 'prev') this.dateUtils.setDateRange(this.dateUtils.addDays(this.endDateLong, -7));
    if(changeType === 'next') this.dateUtils.setDateRange(this.dateUtils.addDays(this.endDateLong, 7));
  }

  onMapDriveDetailsClick(event: any): void {
    const isDriveTo = event.driveType === 'driveTo';
    this.toggleRideModal(event.ride, isDriveTo);
  }

  setActiveTab(tabName: string) {
    this.activeTab = tabName;
    /* Display flags for rides. 0=open/unstaffed, 1=pending, 2=approved, 3=cancelled
       Though we have a 3 or "cancelled" status, there is no tab to
       display only cancelled drives. They'll only be shown under "All" */
    // TODO: Possibly refactor, array may not be most appropriate data structure anymore
    switch(tabName) {
      case 'unstaffed':
        this.displayRides = [true, false, false, false, false];
        break;
      case 'pending':
        this.displayRides = [false, true, false, false, false];
        break;
      case 'approved':
        this.displayRides = [false, false, true, false, false];
        break;
      default:
       this.displayRides = [true, true, true, true, true];
    }
  }

  /*********************************************************************
                                Utilities
  **********************************************************************/
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
