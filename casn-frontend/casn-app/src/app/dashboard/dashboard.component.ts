import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Constants } from '../app.constants';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  userRole: string;
  quoteIsDismissed: boolean = true;
  welcomeMessage: string;

  constructor( private router: Router,
               private constants: Constants ) {}

  ngOnInit() {
    this.userRole = localStorage.getItem("userRole");
    const welcome = localStorage.getItem("showWelcomeMessage");
    if(welcome === "1") {
      this.quoteIsDismissed = false;
      // Pick a random message from the array of possible messages
      this.welcomeMessage = this.constants.WELCOME_MESSAGES[Math.floor(Math.random() * this.constants.WELCOME_MESSAGES.length)];
    }

    // Auto-dismiss intro quote after 7 seconds
    setTimeout(() => {
      this.dismissQuote();
      // For drivers, redirect to the schedule page
      if(this.userRole === '2') this.redirectDriverToSchedule();
    }, 7000);
  }

  redirectDriverToSchedule(): void {
    this.router.navigate(['/view-schedule']);
  }

  dismissQuote(): void {
    localStorage.setItem("showWelcomeMessage", "0");
    this.quoteIsDismissed = true;
  }

}
