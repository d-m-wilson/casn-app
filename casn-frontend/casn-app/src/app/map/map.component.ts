import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {
  @Input() rides: any = {};
  @Input() startDate: any;
  @Input() endDate: any;
  @Input() activeDate: any;
  @Output() closeMapModalClick = new EventEmitter<boolean>();
  @Output() seeDriveDetailsClick = new EventEmitter();
  houstonLatitude: number = 29.7604;
  houstonLongitude: number = -95.3698;
  mapCenter: any = { latitude: 29.7604, longitude: -95.3698, zoom: 9 };
  serviceProviders: any;
  userRole: string;
  // Display flags for rides. 0=open, 1=pending, 2=approved, 3=cancelled
  displayRides: boolean[] = [true, true, false, false];

  constructor( private ds: DefaultApiService,
               private constants: Constants ) { }

  ngOnInit() {
    this.getServiceProviders();
    this.userRole = localStorage.getItem('userRole');
  }

  /*********************************************************************
                          Service Calls
  **********************************************************************/
  getServiceProviders(): void {
    this.ds.getServiceProviders().subscribe(s => {
      this.serviceProviders = s.map(provider => {
        provider.iconUrl = this.constants.SERVICE_PROVIDER_MAP_MARKERS[provider.serviceProviderTypeId];
        return provider;
      })
    });
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

/*********************************************************************
                    Utilities - Getters/Setters, etc.
**********************************************************************/
  getGoogleMapLink(ride: any, driveType: string, addressType: string) {
    const query = `${ride[driveType][addressType + 'Address']} ${ride[driveType][addressType + 'City']} ${ride[driveType][addressType + 'PostalCode']}`;
    const urlEncodedQuery = encodeURI(query);
    return `https://www.google.com/maps/search/?api=1&query=${urlEncodedQuery}`;
  }

  getServiceProviderGoogleMapLink(provider: any): string {
    const query = `${provider.address} ${provider.city} ${provider.postalCode}`;
    const urlEncodedQuery = encodeURI(query);
    return `https://www.google.com/maps/search/?api=1&query=${urlEncodedQuery}`;
  }
}
