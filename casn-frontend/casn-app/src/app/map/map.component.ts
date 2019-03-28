import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultService } from '../api/api/default.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  @Input() rides: any = {};
  @Output() closeMapModalClick = new EventEmitter<boolean>();
  @Output() seeDriveDetailsClick = new EventEmitter();
  mapCenter: any = { latitude: 29.7604, longitude: -95.3698, zoom: 9 };
  clinics: any;

  constructor( private ds: DefaultService ) { }

  ngOnInit() {
    this.getClinics()
    console.log("Rides", this.rides);
  }

  /*********************************************************************
                          Service Calls
  **********************************************************************/
  getClinics(): void {
    this.ds.getClinics().subscribe(c => this.clinics = c);
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

  getClinicGoogleMapLink(clinic: any): string {
    const query = `${clinic.address} ${clinic.city} ${clinic.postalCode}`;
    const urlEncodedQuery = encodeURI(query);
    return `https://www.google.com/maps/search/?api=1&query=${urlEncodedQuery}`;
  }
}
