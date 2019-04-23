import { Injectable, Inject } from '@angular/core';
import { MapsAPILoader,
         LAZY_MAPS_API_CONFIG,
         LazyMapsAPILoaderConfigLiteral,
         GoogleMapsScriptProtocol } from '@agm/core';
import { DocumentRef, WindowRef } from '@agm/core/utils/browser-globals';
import { AppConfigService } from './appconfig.service';

@Injectable()
export class GoogleMapConfigService extends MapsAPILoader {
  private _scriptLoadingPromise: Promise<void>;
  private _config: LazyMapsAPILoaderConfigLiteral;
  private _windowRef: WindowRef;
  private _documentRef: DocumentRef;

  constructor( @Inject(LAZY_MAPS_API_CONFIG) config: any,
               w: WindowRef,
               d: DocumentRef,
               private appConfigService: AppConfigService ) {
    super();
    this._config = config || {};
    this._windowRef = w;
    this._documentRef = d;
  }

  load(): Promise<void> {
    if (this._scriptLoadingPromise) return this._scriptLoadingPromise;

    const script = this._documentRef.getNativeDocument().createElement('script');
    script.type = 'text/javascript';
    script.async = true;
    script.defer = true;
    const callbackName: string = `angular2GoogleMapsLazyMapsAPILoader`;

    this._config.apiKey = this.appConfigService.getConfig().googleApiKey;
    script.src = this._getScriptSrc(callbackName);
    this._documentRef.getNativeDocument().body.appendChild(script);


    this._scriptLoadingPromise = new Promise<void>((resolve: Function, reject: Function) => {
      (<any>this._windowRef.getNativeWindow())[callbackName] = () => { console.log("loaded"); resolve(); };

      script.onerror = (error: Event) => { reject(error); };
    });

    return this._scriptLoadingPromise;
  }

  private _getScriptSrc(callbackName: string): string {
    let protocolType: GoogleMapsScriptProtocol = (this._config && this._config.protocol) || GoogleMapsScriptProtocol.HTTPS;
    let protocol: string;

    switch (protocolType) {
      case GoogleMapsScriptProtocol.AUTO:
        protocol = '';
        break;
      case GoogleMapsScriptProtocol.HTTP:
        protocol = 'http:';
        break;
      case GoogleMapsScriptProtocol.HTTPS:
        protocol = 'https:';
        break;
    }

    const hostAndPath: string = this._config.hostAndPath || 'maps.googleapis.com/maps/api/js';
    const queryParams: { [key: string]: string | Array<string> } = {
      v: this._config.apiVersion || '3',
      callback: callbackName,
      key: this._config.apiKey,
      client: this._config.clientId,
      channel: this._config.channel,
      libraries: this._config.libraries,
      region: this._config.region,
      language: this._config.language
    };
    const params: string = Object.keys(queryParams)
      .filter((k: string) => queryParams[k] != null)
      .filter((k: string) => {
        // remove empty arrays
        return !Array.isArray(queryParams[k]) ||
          (Array.isArray(queryParams[k]) && queryParams[k].length > 0);
      })
      .map((k: string) => {
        // join arrays as comma seperated strings
        let i = queryParams[k];
        if (Array.isArray(i)) {
          return { key: k, value: i.join(',') };
        }
        return { key: k, value: queryParams[k] };
      })
      .map((entry: { key: string, value: string }) => { return `${entry.key}=${entry.value}`; })
      .join('&');
    return `${protocol}//${hostAndPath}?${params}`;
  }
}
