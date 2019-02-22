import { Component, OnInit } from '@angular/core';
import { Constants } from './app.constants';
import { AuthenticationService } from './auth-services/auth.service';
import { Router } from '@angular/router';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";

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
    private constants: Constants,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
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
    this.registerCustomMaterialIcons();
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

  registerCustomMaterialIcons(): void {
    this.matIconRegistry.addSvgIcon(
      `drive_to`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/drive-to.svg")
    );
    this.matIconRegistry.addSvgIcon(
      `drive_from`,
      this.domSanitizer.bypassSecurityTrustResourceUrl("../assets/icons/drive-from.svg")
    );
  }
}
