import { Component, OnInit } from '@angular/core';
import { DefaultService } from '../api/api/default.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css']
})
export class RidesComponent implements OnInit {
  objectKeys: any = Object.keys;
  startDate: string = "2018-12-24";
  endDate: string = "2018-12-30";
  activeDate: string = "2018-12-24"
  datesForDateRange: any[]; // All dates from startDate to endDate
  // rides: any[];
  rides: any;
  clinics: any;
  // TODO: should be fetched from API or set as app-level constant
  apptTypes: any = { 4: 'Ultrasound', 3: 'Surgical' };
  // Display flags for rides. 0=open, 1=pending, 2=approved
  displayRides: boolean[] = [true, true, true];
  displayRideModal: boolean = false;
  rideModalContent: any;
  showDisplayFiltersModal: boolean = false;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultService ) { }

  ngOnInit() {
    this.setDateRange();
    this.getClinics();
    this.getRides();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getRides(): void {
    this.ds.getAllAppointments(this.startDate, this.endDate).subscribe(appts => {
      console.log("Appts are:", appts);
      appts = appts.sort((a,b) => new Date(a.appointment.appointmentDate).valueOf() - new Date(b.appointment.appointmentDate).valueOf());
      this.rides = {};
      this.datesForDateRange.forEach(day => this.rides[day] = []);
      console.log("Rides", this.rides)
      appts.forEach(a => {
        const day = a.appointment.appointmentDate.toString().slice(0,10);
        this.rides[day].push(a);
      });
    });
  }

  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      console.table(c);
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

  toggleDisplayFiltersModal(): void {
    this.showDisplayFiltersModal = !this.showDisplayFiltersModal;
  }

  setActiveDate(date: string): void {
    this.activeDate = date;
  }

  handleChangeWeekClick(changeType: string): void {
    if(changeType === 'prev') {
      console.log("Going to previous week....")
      this.setDateRange(this.addDays(this.startDate, -6));
    }
    if(changeType === 'next') {
      console.log("Going to next week....")
      this.setDateRange(this.addDays(this.endDate, 1))
    }
    this.getRides();
  }

/*********************************************************************
                              Utilities
**********************************************************************/
  setDateRange(date?: any): void {
    console.log("Date is", date);
    const currentDate = date || new Date();
    this.startDate = this.addDays(currentDate, -currentDate.getDay()).toISOString().slice(0,10);
    this.endDate = this.addDays(this.startDate, 6).toISOString().slice(0,10);
    this.getDatesForDateRange();
    this.activeDate = this.startDate;
    console.log("startDate", this.startDate);
    console.log("endDate", this.endDate);
  }

  private addDays(date, days) {
    console.log("Date in addDays is", date);
    var result = new Date(date);
    result.setDate(result.getDate() + days);
    return result;
  }

  getDatesForDateRange(): void {
    this.datesForDateRange = [];
    let dateArray = new Array();
    let currentDate = new Date(this.startDate.valueOf());
    for(let i = 0; i < 7; i++) {
      this.datesForDateRange.push((new Date(currentDate)).toISOString().slice(0,10));
      currentDate.setDate(currentDate.getDate() + 1);;
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
      case 1: return "Pending Approval";
      case 2: return "Approved";
      default: return "";
    }
  }

}
