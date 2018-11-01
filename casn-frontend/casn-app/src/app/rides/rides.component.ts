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
  // TODO: The getAllAppointmentsForDriver (single & array) endpoints
  // will disappear. The dispatcher appt services should move to the default service module. URL endpoint will be /appointments or /appointment/:id
  constructor( private ds: DispatcherService ) { }

  ngOnInit() {
    this.getRides();
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
