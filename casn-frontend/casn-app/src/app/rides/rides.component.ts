import { Component, OnInit } from '@angular/core';
import { DefaultService } from '../api/api/default.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css']
})
export class RidesComponent implements OnInit {
  objectKeys: any = Object.keys;
  startDate: string = "2018-11-01";
  endDate: string = "2018-11-07";
  activeDate: string = "2018-11-01"
  datesForDateRange: any[]; // All dates from startDate to endDate
  // rides: any[];
  rides: any;
  clinics: any;
  // TODO: should be fetched from API or set as app-level constant
  apptTypes: any = { 4: 'Ultrasound', 3: 'Surgical' };
  // Display flags for rides. 0=open, 1=pending, 2=approved
  displayRides: boolean[] = [true, true, true];
  displayRideModal: boolean = false;

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
  toggleRideModal(): void {
    this.displayRideModal = !this.displayRideModal;
  }

  setActiveDate(date: string): void {
    this.activeDate = date;
  }

/*********************************************************************
                              Utilities
**********************************************************************/
  setDateRange(): void {
    // TODO: Uncomment when finished testing.
    // const currentDate = new Date();
    // // First day of current week is day of month - day of week
    // const firstDay = currentDate.getDate() - currentDate.getDay();
    // // Last day of current week is first day + 6
    // const lastDay = firstDay + 6;
    // this.startDate = new Date(currentDate.setDate(firstDay)).toISOString().slice(0,10);
    // this.endDate = new Date(currentDate.setDate(lastDay)).toISOString().slice(0,10);
    this.getDatesForDateRange();
  }

  getDatesForDateRange(): void {
    this.datesForDateRange = [];
    let dateArray = new Array();
    let currentDate = new Date(this.startDate.valueOf());
    console.log("Current date is", currentDate);
    for(let i = 0; i < 7; i++) {
      this.datesForDateRange.push((new Date(currentDate)).toISOString().slice(0,10));
      console.log('Dates', this.datesForDateRange);
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
      case 0: return "Open - Apply Now";
      case 1: return "Pending Approval";
      case 2: return "Approved";
      default: return "";
    }
  }

}
