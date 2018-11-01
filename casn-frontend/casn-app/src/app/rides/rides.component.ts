import { Component, OnInit } from '@angular/core';
import { DispatcherService } from '../api/api/dispatcher.service';

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
  // TODO: How to handle dispatcher vs. driver? Same endpoint?
  constructor( private ds: DispatcherService ) { }

  ngOnInit() {
    this.getRides()
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getRides(): void {
    this.ds.getAllAppointmentsForDispatcher().subscribe(data => {
      console.log("Appts are", data);
      this.rides = data;
    })
  }

}
