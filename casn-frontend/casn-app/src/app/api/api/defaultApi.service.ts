/**
 * CASN API
 * CASN API (ASP.NET Core 2.1)
 *
 * OpenAPI spec version: 2.0
 * Contact: katie@clinicaccess.org
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
/* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { CASNAppCoreModelsAppointmentDTO } from '../model/cASNAppCoreModelsAppointmentDTO';
import { CASNAppCoreModelsAppointmentType } from '../model/cASNAppCoreModelsAppointmentType';
import { CASNAppCoreModelsServiceProvider } from '../model/cASNAppCoreModelsServiceProvider';
import { CASNAppCoreModelsDriveCancelReason } from '../model/cASNAppCoreModelsDriveCancelReason';
import { CASNAppCoreModelsDriveStatus } from '../model/cASNAppCoreModelsDriveStatus';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';
import { DefaultApiServiceInterface }                            from './defaultApi.serviceInterface';


@Injectable({
  providedIn: 'root'
})
export class DefaultApiService implements DefaultApiServiceInterface {

    protected basePath = BASE_PATH || 'https://localhost/api';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * gets appointment by appointmentID
     * Search for existing appointment by appointmentIdentifier, return dispatcher-level details
     * @param appointmentID pass an appointmentIdentifier
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getAppointmentByID(appointmentID: string, observe?: 'body', reportProgress?: boolean): Observable<CASNAppCoreModelsAppointmentDTO>;
    public getAppointmentByID(appointmentID: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<CASNAppCoreModelsAppointmentDTO>>;
    public getAppointmentByID(appointmentID: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<CASNAppCoreModelsAppointmentDTO>>;
    public getAppointmentByID(appointmentID: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (appointmentID === null || appointmentID === undefined) {
            throw new Error('Required parameter appointmentID was null or undefined when calling getAppointmentByID.');
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<CASNAppCoreModelsAppointmentDTO>(`${this.basePath}/appointments/${encodeURIComponent(String(appointmentID))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * gets list of appointment types
     *
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getAppointmentTypes(observe?: 'body', reportProgress?: boolean): Observable<Array<CASNAppCoreModelsAppointmentType>>;
    public getAppointmentTypes(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<CASNAppCoreModelsAppointmentType>>>;
    public getAppointmentTypes(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<CASNAppCoreModelsAppointmentType>>>;
    public getAppointmentTypes(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<CASNAppCoreModelsAppointmentType>>(`${this.basePath}/appointmentType`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * gets appointments with dispatcher-level details
     * Get all appointments within a default date range (possibly adjustable w/ query params). Appointments include details, e.g. exact location, available only to dispatchers.
     * @param startDate pass a startDate by which to filter
     * @param endDate pass an endDate by which to filter
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getAppointments(startDate?: string, endDate?: string, observe?: 'body', reportProgress?: boolean): Observable<Array<CASNAppCoreModelsAppointmentDTO>>;
    public getAppointments(startDate?: string, endDate?: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<CASNAppCoreModelsAppointmentDTO>>>;
    public getAppointments(startDate?: string, endDate?: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<CASNAppCoreModelsAppointmentDTO>>>;
    public getAppointments(startDate?: string, endDate?: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {



        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (startDate !== undefined && startDate !== null) {
            queryParameters = queryParameters.set('startDate', <any>startDate);
        }
        if (endDate !== undefined && endDate !== null) {
            queryParameters = queryParameters.set('endDate', <any>endDate);
        }

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<CASNAppCoreModelsAppointmentDTO>>(`${this.basePath}/appointments`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * gets list of clinics
     *
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getServiceProviders(observe?: 'body', reportProgress?: boolean): Observable<Array<CASNAppCoreModelsServiceProvider>>;
    public getServiceProviders(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<CASNAppCoreModelsServiceProvider>>>;
    public getServiceProviders(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<CASNAppCoreModelsServiceProvider>>>;
    public getServiceProviders(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<CASNAppCoreModelsClinic>>(`${this.basePath}/serviceprovider`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * gets list of drive cancel reasons
     *
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getDriveCancelReasons(observe?: 'body', reportProgress?: boolean): Observable<Array<CASNAppCoreModelsDriveCancelReason>>;
    public getDriveCancelReasons(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<CASNAppCoreModelsDriveCancelReason>>>;
    public getDriveCancelReasons(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<CASNAppCoreModelsDriveCancelReason>>>;
    public getDriveCancelReasons(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<CASNAppCoreModelsDriveCancelReason>>(`${this.basePath}/driveCancelReason`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * gets list of drive statuses
     *
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getDriveStatuses(observe?: 'body', reportProgress?: boolean): Observable<Array<CASNAppCoreModelsDriveStatus>>;
    public getDriveStatuses(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<CASNAppCoreModelsDriveStatus>>>;
    public getDriveStatuses(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<CASNAppCoreModelsDriveStatus>>>;
    public getDriveStatuses(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<CASNAppCoreModelsDriveStatus>>(`${this.basePath}/driveStatus`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * gets list of badges combined with badges earned for the current user
     *
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public getBadges(observe?: 'body', reportProgress?: boolean): Observable<Array<any>>;
    public getBadges(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<any>>>;
    public getBadges(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<any>>>;
    public getBadges(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // authentication (Bearer) required
        if (this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]) {
            headers = headers.set('Authorization', this.configuration.apiKeys && this.configuration.apiKeys["Authorization"]);
        }

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.get<Array<CASNAppCoreModelsDriveStatus>>(`${this.basePath}/badge`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
