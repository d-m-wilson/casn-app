import { Injectable }       from '@angular/core';
import { CanActivate,
         Router,
         ActivatedRouteSnapshot,
         RouterStateSnapshot } from '@angular/router';
import { AuthenticationService }      from './auth.service';
import { Constants } from '../app.constants';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor ( private authService: AuthenticationService,
                private router: Router,
                private constants: Constants ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.authService.isLoggedIn()) {
      const userRole = localStorage.getItem('userRole');
      // Drivers (role 2) are restricted from some pages
      if(userRole === "2" && this.pageIsRestricted(state.url)) {
        this.router.navigate(['/error'], { queryParams: { errorCode: "401" }});
        return false;
      }
      return true;
    }

    // not logged in so redirect to login page with the return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }

  pageIsRestricted(page: string): boolean {
    console.log("Page is", page);
    return this.constants.RESTRICTED_ROUTES.includes(page);
  }
}
