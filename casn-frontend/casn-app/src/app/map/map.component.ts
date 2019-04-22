import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  @Input() rides: any = {};
  @Output() closeMapModalClick = new EventEmitter<boolean>();
  @Output() seeDriveDetailsClick = new EventEmitter();
  houstonLatitude: number = 29.7604;
  houstonLongitude: number = -95.3698;
  mapCenter: any = { latitude: 29.7604, longitude: -95.3698, zoom: 9 };
  clinics: any;

  constructor( private ds: DefaultApiService ) { }

  ngOnInit() {
    this.getClinics()
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

  showPolylinesForMarker(ride: any) {
    ride.showPolylines = true;
  }

  hidePolylinesForMarker(ride: any) {
    ride.showPolylines = false;
  }

  lastSelectedInfoWindow: any;
  markerClick(infoWindow: any, event?: any) {
    this.mapCenter.latitude = event.latitude;
    this.mapCenter.longitude = event.longitude;
    if (infoWindow == this.lastSelectedInfoWindow) return;
    if (this.lastSelectedInfoWindow != null) {
      try{
        this.lastSelectedInfoWindow.close();
      } catch {} //in case you reload your markers
    }
    this.lastSelectedInfoWindow = infoWindow;
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