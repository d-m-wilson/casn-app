import { Component, OnInit } from '@angular/core';
import { DefaultService } from '../api/api/default.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css']
})
export class RidesComponent implements OnInit {
  startDate: string = "2017-11-01";
  endDate: string = "2018-11-28";
  rides: any[];
  clinics: any;
  // TODO: should be fetched from API or set as app-level constant
  apptTypes: any = { 4: 'Ultrasound', 3: 'Surgical' };
  // Display flags for rides. 0=open, 1=pending, 2=approved
  displayRides: boolean[] = [true, true, true];

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
    // TODO: Make start/end dates dynamic
    this.ds.getAllAppointments(this.startDate, this.endDate).subscribe(appts => {
      console.log("Appts are:", appts);
      // Sort by date asc
      this.rides = appts.sort((a, b) => new Date(a.appointment.appointmentDate).valueOf() - new Date(b.appointment.appointmentDate).valueOf());
    })
  }

  getClinics(): void {
    this.ds.getClinics().subscribe(c => {
      console.table(c);
      this.clinics = c.reduce((map, obj) => (map[obj.id] = obj, map), {});
    });
  }

/*********************************************************************
                              Utilities
**********************************************************************/
  setDateRange(): void {
    const currentDate = new Date();
    // First day of current week is day of month - day of week
    const firstDay = currentDate.getDate() - currentDate.getDay();
    // Last day of current week is first day + 6
    const lastDay = firstDay + 6;
    this.startDate = new Date(currentDate.setDate(firstDay)).toUTCString();
    this.endDate = new Date(currentDate.setDate(lastDay)).toUTCString();
    console.log("Current Date:", currentDate);
    console.log("First Date:", this.startDate);
    console.log("Last Date:", this.endDate);
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
