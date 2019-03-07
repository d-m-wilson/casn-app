import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  @Input() rides: any = {};
  @Output() closeMapModalClick = new EventEmitter<boolean>();
  @Output() seeDriveDetailsClick = new EventEmitter();
  mapCenter: any = { latitude: 29.7604, longitude: -95.3698, zoom: 11 };
  coords: any[];

  constructor() { }

  ngOnInit() {
    this.coords = [
      [29.767381, -95.349843],
      [29.783622, -95.322733],
      [29.787346, -95.355677],
      [29.759036, -95.378155],
      [29.737873, -95.416263],
      [29.703232, -95.408203]
    ];
    this.addFakeCoordsToRides();
  }

  addFakeCoordsToRides() {
    this.rides.forEach(r => {
      r.driveTo.startLatitude = this.coords[0][0];
      r.driveTo.startLongitude = this.coords[0][1];
      r.driveTo.endLatitude = this.coords[1][0];
      r.driveTo.endLongitude = this.coords[1][1];

      r.driveFrom.startLatitude = this.coords[2][0];
      r.driveFrom.startLongitude = this.coords[2][1];
      r.driveFrom.endLatitude = this.coords[3][0];
      r.driveFrom.endLongitude = this.coords[3][1];
    });
    console.log("Rides with fake coords", this.rides);
  }

/*********************************************************************
                            Click Handlers
**********************************************************************/
  handleCloseModalClick() {
    this.closeMapModalClick.emit(true);
  }

  handleSeeDriveDetailsClick(ride: any, driveType: string) {
    const msg = { ride: ride, driveType: driveType };
    this.seeDriveDetailsClick.emit(msg);
  }

/*********************************************************************
                    Utilities - Getters/Setters, etc.
**********************************************************************/
  getGoogleMapLink(ride: any, driveType: string, addressType: string) {
    const query = `${ride[driveType][addressType + 'Address']} ${ride[driveType][addressType + 'City']} ${ride[driveType][addressType + 'PostalCode']}`;
    const urlEncodedQuery = encodeURI(query);
    return `https://www.google.com/maps/search/?api=1&query=${urlEncodedQuery}`;
  }
}
