import { Component, OnInit } from '@angular/core';
import { Constants } from './app.constants';
import { AuthenticationService } from './auth-services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  opened: boolean;
  menuItems: any[];

  constructor(
    private _authService: AuthenticationService,
    private _router: Router,
    private constants: Constants
  ) {}

  ngOnInit() {
    if (window.location.href.indexOf('?postLogout=true') > 0) {
      this._authService.signoutRedirectCallback().then(() => {
        let url: string = this._router.url.substring(
          0,
          this._router.url.indexOf('?')
        );
        this._router.navigateByUrl(url);
      });
    }
    this.menuItems = this.constants.MENU_ITEMS;
  }

  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }

  isLoggedIn() {
    var boolExpr = this._authService.isLoggedIn();
    return boolExpr;
  }
}
