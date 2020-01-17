import { Component, OnInit, HostListener } from '@angular/core';
import { Constants } from './app.constants';
import { AuthenticationService } from './auth-services/auth.service';
import { Router } from '@angular/router';
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { environment } from '../environments/environment';
import { trigger, transition, state, animate, style, group } from '@angular/animations';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [
    trigger('openClose', [
      state('open', style({
        opacity: 1,
        width: '100%'
      })),
      state('closed', style({
        // opacity: 0,
        display: 'none'
      })),
      transition('open => closed', [
        animate('8s')
      ]),
      transition('closed => open', [
        animate('8s')
      ]),
      transition('* => closed', [
        animate('8s')
      ]),
      transition('* => open', [
        animate('8s')
      ]),
      transition('open <=> closed', [
        animate('8s')
      ]),
      transition ('* => open', [
        animate ('8s',
          style ({ opacity: '*' }),
        ),
      ]),
      transition('* => *', [
        animate('8s')
      ]),
    ]),
  ]
})
export class AppComponent implements OnInit {
  opened: boolean;
  menuItems: any[];
  // A2HS
  deferredPrompt: any;
  showButton: boolean = true;
  isOpen: boolean = true;
  userRole: string;

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
    this.userRole = localStorage.getItem("userRole");
    this.registerCustomMaterialIcons();
    this.isOpen = false;
  }
  /*********************************************************************
                              User Login
  **********************************************************************/
  login() {
    this._authService.login();
  }

  logout() {
    this._authService.logout();
  }

  isLoggedIn() {
    var boolExpr = this._authService.isLoggedIn();
    this.userRole = localStorage.getItem("userRole");
    return boolExpr;
  }

  /*********************************************************************
                              Custom Icons
  **********************************************************************/
  registerCustomMaterialIcons(): void {
    this.matIconRegistry.addSvgIcon(
      `drive_to`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`${environment.clientRoot}assets/icons/drive-to.svg`)
    );
    this.matIconRegistry.addSvgIcon(
      `drive_from`,
      this.domSanitizer.bypassSecurityTrustResourceUrl(`${environment.clientRoot}assets/icons/drive-to.svg`)
    );
  }

  /*********************************************************************
                                    A2HS
  **********************************************************************/
  @HostListener('window:beforeinstallprompt', ['$event'])
  onbeforeinstallprompt(e) {
    console.log(e);
    // Prevent Chrome 67 and earlier from automatically showing the prompt
    e.preventDefault();
    // Stash the event so it can be triggered later.
    this.deferredPrompt = e;
    this.showButton = true;
  }

  addToHomeScreen() {
    // Hide our user interface that shows our A2HS button
    this.showButton = false;
    // Show the prompt
    this.deferredPrompt.prompt();
    // Wait for the user to respond to the prompt
    this.deferredPrompt.userChoice.then(choiceResult => {
      console.log("A2HS result:", choiceResult.outcome);
      this.deferredPrompt = null;
    });
  }

}
