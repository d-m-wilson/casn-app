import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthenticationService {
  private loggedIn = new BehaviorSubject<boolean>(false);

  get isLoggedIn() {
    if(localStorage.getItem('currentUser')) {
      this.loggedIn.next(true);
    }
    return this.loggedIn.asObservable();
  }

  constructor( private http: HttpClient ) { }

  login(username: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/login`, { username: username, password: password }).pipe(
      map(user => {
        // login successful if there's a jwt token in the response
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.loggedIn.next(true);
        }
        return user;
      })
    );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.loggedIn.next(false);
  }
}
