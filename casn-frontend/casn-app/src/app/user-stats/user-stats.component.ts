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
  getBadges(): void {
    this.ds.getBadges().subscribe(badges => {
      this.badges = badges.map(b => {
        if(b.isEarned) return b;
        if(b.isHidden) b.path = `assets/badges/hidden${Math.floor(Math.random() * 3) + 1}.png`;
        return b;
      })
    });
  }

  toggleTapped(badge): void {
    badge.tapped = badge.tapped ? false : true;
  }

}
