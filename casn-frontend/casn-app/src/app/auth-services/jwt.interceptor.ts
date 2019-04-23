import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../auth-services/auth.service';
import { environment } from '../../environments/environment';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private _authService: AuthenticationService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(req.url.startsWith(environment.apiUrl)) {
            var accessToken = this._authService.getAccessToken();
            const headers = req.headers
                .set('Authorization', `Bearer ${accessToken}`);
            const authReq = req.clone({ headers });
            return next.handle(authReq);
        } else {
            return next.handle(req);
        }
    }
}
