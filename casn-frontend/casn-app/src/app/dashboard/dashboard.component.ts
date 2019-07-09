import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  userRole: string;

  constructor( private router: Router ) { }

  ngOnInit() {
    this.userRole = localStorage.getItem("userRole");
    if(this.userRole === '2') this.redirectDriverToSchedule();
  }

  redirectDriverToSchedule(): void {
    this.router.navigate(['/view-schedule']);
  }

}
