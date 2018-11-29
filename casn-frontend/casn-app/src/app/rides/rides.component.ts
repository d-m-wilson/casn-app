import { Component, OnInit } from '@angular/core';
import { DefaultService } from '../api/api/default.service';

@Component({
  selector: 'app-rides',
  templateUrl: './rides.component.html',
  styleUrls: ['./rides.component.css']
})
export class RidesComponent implements OnInit {
  rides: any[];

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultService ) { }

  ngOnInit() {
    this.getRides();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getRides(): void {
    // TODO: Make start/end dates dynamic
    this.ds.getAllAppointments("2017-11-01", "2018-11-28").subscribe(data => {
      console.log("Appts are", data);
      this.rides = data;
    })
    console.log("Rides are:", this.rides);
  }

}
