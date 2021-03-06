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
import { HttpHeaders }                                       from '@angular/common/http';

import { Observable }                                        from 'rxjs';

import { CASNAppCoreModelsAppointmentDTO } from '../model/cASNAppCoreModelsAppointmentDTO';
import { CASNAppCoreModelsBody } from '../model/cASNAppCoreModelsBody';
import { CASNAppCoreModelsBody1 } from '../model/cASNAppCoreModelsBody1';
import { CASNAppCoreModelsCaller } from '../model/cASNAppCoreModelsCaller';
import { CASNAppCoreModelsDeleteSuccessMessage } from '../model/cASNAppCoreModelsDeleteSuccessMessage';
import { CASNAppCoreModelsVolunteerDrive } from '../model/cASNAppCoreModelsVolunteerDrive';


import { Configuration }                                     from '../configuration';


export interface DispatcherApiServiceInterface {
    defaultHeaders: HttpHeaders;
    configuration: Configuration;


    /**
    * adds a new appointment
    * Adds appointment (and drives) to the system
    * @param appointmentDTO appointmentData to add
    */
    addAppointment(appointmentDTO?: CASNAppCoreModelsAppointmentDTO, extraHttpRequestParams?: any): Observable<CASNAppCoreModelsAppointmentDTO>;

    /**
    * adds a caller
    * Adds caller to the system
    * @param caller callerData to add
    */
    addCaller(caller?: CASNAppCoreModelsCaller, extraHttpRequestParams?: any): Observable<{}>;

    /**
    * approves a volunteer for a drive
    * Adds driverId to a drive
    * @param body1
    */
    addDriver(body1?: CASNAppCoreModelsBody1, extraHttpRequestParams?: any): Observable<{}>;

    /**
    *
    *
    * @param appointmentID pass an appointmentIdentifier
    */
    deleteAppointment(appointmentID: string, extraHttpRequestParams?: any): Observable<CASNAppCoreModelsDeleteSuccessMessage>;

    /**
    * gets caller by callerIdentifier
    * Search for existing callers by the dispatcher created callerIdentifier (caller ID)
    * @param callerIdentifier pass a search string for looking up callerIdentifier
    */
    getCallerByCallerIdentifier(callerIdentifier: string, extraHttpRequestParams?: any): Observable<CASNAppCoreModelsCaller>;

    /**
    * get list of applicants for a drive
    *
    * @param driveId id of drive
    */
    getVolunteerDrives(driveId: number, extraHttpRequestParams?: any): Observable<Array<CASNAppCoreModelsVolunteerDrive>>;

    /**
    * updates an existing appointment
    * Updates appointment (and corresponding drive) information
    * @param appointmentID pass an appointmentIdentifier
    * @param appointmentDTO appointmentData with updated fields
    */
    updateAppointment(appointmentID: string, appointmentDTO?: CASNAppCoreModelsAppointmentDTO, extraHttpRequestParams?: any): Observable<CASNAppCoreModelsAppointmentDTO>;

    /**
    * unapproves a volunteer for a drive
    * removes driverId from a drive and updates status as needed
    * @param body
    */
    unapproveDriver(body?: CASNAppCoreModelsBody, extraHttpRequestParams?: any): Observable<{}>;

    /**
    * denies a volunteer for a drive (who has not yet been approved)
    * removes volunteerDriverId from list of volunteers
    * @param body1
    */
    denyDriver(body1?: CASNAppCoreModelsBody1, extraHttpRequestParams?: any): Observable<{}>;

    /**
    * used to update the status of a drive
    * @param driveId
    * @param driveStatusId
    */
    updateDriveStatus(driveId: string, driveStatusId: number, extraHttpRequestParams?: any): Observable<{}>;

}
