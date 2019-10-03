import { Component, OnInit } from '@angular/core';
import { DefaultApiService } from '../api/api/defaultApi.service';

@Component({
  selector: 'app-user-stats',
  templateUrl: './user-stats.component.html',
  styleUrls: ['./user-stats.component.scss']
})
export class UserStatsComponent implements OnInit {
  badges: any[];

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DefaultApiService ) {}

  ngOnInit() {
    this.getBadges()
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  // TODO: Replace with API call
  getBadges(): void {
    this.ds.getBadges().subscribe(b => {
      console.log("BADGEZZZZ", b);
      this.badges = b;
    })
    // this.badges = tempBadges;
  }

  toggleTapped(badge) {
    badge.tapped = badge.tapped ? false : true;
  }

}
