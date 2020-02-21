import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';

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

  constructor( private ds: DefaultApiService ) { }

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
        switch(provider.serviceProviderTypeId) {
          case 1:
            provider.iconUrl = 'assets/img/marker_clinic.png';
            break;
          case 2:
            provider.iconUrl = 'assets/img/marker_court.png';
            break;
          case 3:
            provider.iconUrl = 'assets/img/marker_lodging.png';
            break;
          default:
            // TODO: Get a default marker?
            provider.iconUrl = 'assets/img/marker_cluster.png';
        }
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
