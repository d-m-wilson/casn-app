import { Component, OnInit } from '@angular/core';
import { DefaultService } from '../api/api/default.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css']
})
export class RidesComponent implements OnInit {
  rides: any[];
  clinics: any;
  // Display flags for rides. 0=open, 1=pending, 2=approved
  displayRides: boolean[] = [true, true, true];

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultService ) { }

  ngOnInit() {
    this.getRides();
    this.getClinics();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getRides(): void {
    // TODO: Make start/end dates dynamic
    this.ds.getAllAppointments("2017-11-01", "2018-11-28").subscribe(appts => {
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

}
