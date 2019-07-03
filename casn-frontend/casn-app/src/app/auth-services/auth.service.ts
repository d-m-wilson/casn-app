import { Injectable } from '@angular/core';
import { UserManager, User, WebStorageStateStore } from 'oidc-client';
import { environment } from '../../environments/environment';
import { AppConfigService } from './appconfig.service';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';

@Injectable()
export class AuthenticationService {
  private _userManager: UserManager;
  private _user: User;
  private _appConfigService: AppConfigService;
  private _appConfigData;
  private _initialized: Boolean;

  constructor( private appConfigService: AppConfigService,
               private ds: DispatcherApiService ) {
    console.log('AuthenticationService ctor');
    this._initialized = false;
    this._appConfigService = appConfigService;
    this._appConfigData = this._appConfigService.getConfig();
    if (!this._appConfigData || !this._appConfigData.stsAuthority) {
      console.log('App config data not loaded yet. Will try again later.');
    } else {
      this.initOidcClient();
    }
  }

  private initOidcClient() {
    console.log('AuthenticationService initOidcClient()');
    if (!this._appConfigData) {
      console.log('Trying to get app config data again...');
      this._appConfigData = this._appConfigService.getConfig();
      if (!this._appConfigData) {
        console.error('AuthenticationService initOidcClient() failed to get app config data!');
        return;
      } else {
        console.log('App config data received!');
      }
    }

    var stsAuthority = this._appConfigData.stsAuthority;
    var clientId = this._appConfigData.stsClientId;
    var apiScope = this._appConfigData.apiScope;

    var config = {
      authority: stsAuthority,
      client_id: clientId,
      redirect_uri: `${environment.clientRoot}assets/oidc-login-redirect.html`,
      scope: `openid profile email ${apiScope}`,
      response_type: 'id_token token',
      post_logout_redirect_uri: environment.logoutRedirectUrl,
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      metadata: {
        authorization_endpoint: `${stsAuthority}authorize?audience=${apiScope}`,
        issuer: `${stsAuthority}`,
        jwks_uri: `${stsAuthority}.well-known/jwks.json`,
        end_session_endpoint: `${stsAuthority}v2/logout?client_id=${clientId}&returnTo=${environment.clientRoot}?postLogout=true`
      }
    };

    this._userManager = new UserManager(config);

    this._initialized = true;

    this._userManager.getUser().then(user => {
      if (user && !user.expired) {
        this._user = user;
        this.checkPermissions();
      }
    })
  }

  login(): Promise<any> {
    if (!this._initialized) {
      this.initOidcClient();
    }
    return this._userManager.signinRedirect();
  }

  logout(): Promise<any> {
    if (!this._initialized) {
      this.initOidcClient();
    }
    return this._userManager.signoutRedirect();
  }

  signoutRedirectCallback(): Promise<any> {
    if (!this._initialized) {
      this.initOidcClient();
    }
    return this._userManager.signoutRedirectCallback();
  }

  isLoggedIn(): boolean {
    if (!this._initialized) {
      this.initOidcClient();
    }
    if (!this._initialized) {
      return false;
    }
    var boolExpr = this._user && this._user.access_token && !this._user.expired;
    return boolExpr;
  }

  getAccessToken(): string {
    return this._user ? this._user.access_token : '';
  }

  /*
    NOTE: We send a request to a dispatcher-only endpoint to check permissions
    1 = dispatcher
    2 = driver
  */
  checkPermissions(): void {
    this.ds.getCallerByCallerIdentifier("null").subscribe(
      data => {
        console.error("Huh? This should never happen", data);
      },
      err => {
        console.log("Error", err);
        // 404 - no caller found indicates dispatcher-level permissions
        if(err.status === 404) {
          localStorage.setItem('userRole', '1');
        }
        // 403 - forbidden indicates driver-level permissions
        if(err.status === 403) {
          localStorage.setItem('userRole', '2');
        }
      }
    )
  }
}
