import { Component, OnInit } from '@angular/core';
import { Router, RoutesRecognized } from '@angular/router';
import { filter, pairwise } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  userRole: string;
  // TODO: Turn this back on once we have content for it.
  quoteIsDismissed: boolean = true;

  constructor( private router: Router ) {}

  ngOnInit() {
    this.userRole = localStorage.getItem("userRole");

    // Auto-dismiss intro quote after 7 seconds
    setTimeout(() => {
      this.quoteIsDismissed = true;
      // For drivers, redirect to the schedule page
      if(this.userRole === '2') this.redirectDriverToSchedule();
    }, 7000);
  }

  redirectDriverToSchedule(): void {
    this.router.navigate(['/view-schedule']);
  }

}
