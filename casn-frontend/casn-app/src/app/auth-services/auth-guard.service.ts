import { Injectable }       from '@angular/core';
import { CanActivate,
         Router,
         ActivatedRouteSnapshot,
         RouterStateSnapshot } from '@angular/router';
import { AuthenticationService }      from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  private _restrictedPages = [
    '/caller',
    '/appointment'
  ];

  constructor ( private authService: AuthenticationService,
                private router: Router ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.authService.isLoggedIn()) {
      const userRole = localStorage.getItem('userRole');
      // Drivers (role 2) are restricted from some pages
      if(userRole === "2" && this.pageIsRestricted(state.url)) {
        this.router.navigate(['/error'], { queryParams: { error: "401" }});
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
    return this._restrictedPages.includes(page);
  }
}
