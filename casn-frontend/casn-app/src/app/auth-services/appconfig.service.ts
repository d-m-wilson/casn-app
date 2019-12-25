import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class AppConfigService {
    private appConfig;

    constructor(private http: HttpClient) { console.log('AppConfigService ctor'); }

    loadAppConfig() {
        console.log('AppConfigService loadAppConfig()');
        return this.http.get(environment.customConfigUrl)
            .toPromise()
            .then(data => {
                this.appConfig = data;
                console.log('AppConfigService: appConfig loaded:');
                console.log(JSON.stringify(this.appConfig));
            });
    }

    getConfig() {
        console.log('AppConfigService getConfig()');
        return this.appConfig;
    }

}

export const appConfigInitializerFn = (appConfig: AppConfigService) => {
    console.log('appConfigInitializerFn()');
    return () => {
        return appConfig.loadAppConfig();
    }
};