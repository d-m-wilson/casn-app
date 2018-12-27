(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./src/app/api/api.module.ts":
/*!***********************************!*\
  !*** ./src/app/api/api.module.ts ***!
  \***********************************/
/*! exports provided: ApiModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ApiModule", function() { return ApiModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _configuration__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./configuration */ "./src/app/api/configuration.ts");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _api_default_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./api/default.service */ "./src/app/api/api/default.service.ts");
/* harmony import */ var _api_dispatcher_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./api/dispatcher.service */ "./src/app/api/api/dispatcher.service.ts");
/* harmony import */ var _api_driver_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./api/driver.service */ "./src/app/api/api/driver.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};






var ApiModule = /** @class */ (function () {
    function ApiModule(parentModule, http) {
        if (parentModule) {
            throw new Error('ApiModule is already loaded. Import in your base AppModule only.');
        }
        if (!http) {
            throw new Error('You need to import the HttpClientModule in your AppModule! \n' +
                'See also https://github.com/angular/angular/issues/20575');
        }
    }
    ApiModule_1 = ApiModule;
    ApiModule.forRoot = function (configurationFactory) {
        return {
            ngModule: ApiModule_1,
            providers: [{ provide: _configuration__WEBPACK_IMPORTED_MODULE_1__["Configuration"], useFactory: configurationFactory }]
        };
    };
    var ApiModule_1;
    ApiModule = ApiModule_1 = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [],
            declarations: [],
            exports: [],
            providers: [
                _api_default_service__WEBPACK_IMPORTED_MODULE_3__["DefaultService"],
                _api_dispatcher_service__WEBPACK_IMPORTED_MODULE_4__["DispatcherService"],
                _api_driver_service__WEBPACK_IMPORTED_MODULE_5__["DriverService"]
            ]
        }),
        __param(0, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()), __param(0, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["SkipSelf"])()),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()),
        __metadata("design:paramtypes", [ApiModule,
            _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"]])
    ], ApiModule);
    return ApiModule;
}());



/***/ }),

/***/ "./src/app/api/api/default.service.ts":
/*!********************************************!*\
  !*** ./src/app/api/api/default.service.ts ***!
  \********************************************/
/*! exports provided: DefaultService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DefaultService", function() { return DefaultService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _encoder__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../encoder */ "./src/app/api/encoder.ts");
/* harmony import */ var _variables__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../variables */ "./src/app/api/variables.ts");
/* harmony import */ var _configuration__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../configuration */ "./src/app/api/configuration.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../environments/environment */ "./src/environments/environment.ts");
/**
 * CASN API
 * This is a test CASN API
 *
 * OpenAPI spec version: 1.0.0
 * Contact: katie@clinicaccess.org
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
/* tslint:disable:no-unused-variable member-ordering */
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};






var DefaultService = /** @class */ (function () {
    function DefaultService(httpClient, basePath, configuration) {
        this.httpClient = httpClient;
        this.basePath = _environments_environment__WEBPACK_IMPORTED_MODULE_5__["environment"].apiUrl;
        this.defaultHeaders = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]();
        this.configuration = new _configuration__WEBPACK_IMPORTED_MODULE_4__["Configuration"]();
        if (configuration) {
            this.configuration = configuration;
            this.configuration.basePath = configuration.basePath || basePath || this.basePath;
        }
        else {
            this.configuration.basePath = basePath || this.basePath;
        }
    }
    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    DefaultService.prototype.canConsumeForm = function (consumes) {
        var form = 'multipart/form-data';
        for (var _i = 0, consumes_1 = consumes; _i < consumes_1.length; _i++) {
            var consume = consumes_1[_i];
            if (form === consume) {
                return true;
            }
        }
        return false;
    };
    DefaultService.prototype.getClinics = function (observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/clinic", {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DefaultService.prototype.getAllAppointments = function (startDate, endDate, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var queryParameters = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]({ encoder: new _encoder__WEBPACK_IMPORTED_MODULE_2__["CustomHttpUrlEncodingCodec"]() });
        if (startDate !== undefined && startDate !== null) {
            queryParameters = queryParameters.set('startDate', startDate);
        }
        if (endDate !== undefined && endDate !== null) {
            queryParameters = queryParameters.set('endDate', endDate);
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/appointments", {
            params: queryParameters,
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DefaultService.prototype.getAppointmentByID = function (appointmentID, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (appointmentID === null || appointmentID === undefined) {
            throw new Error('Required parameter appointmentID was null or undefined when calling getAppointmentForDispatcherByID.');
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/appointments/" + encodeURIComponent(String(appointmentID)), {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DefaultService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()), __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_variables__WEBPACK_IMPORTED_MODULE_3__["BASE_PATH"])), __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"], String, _configuration__WEBPACK_IMPORTED_MODULE_4__["Configuration"]])
    ], DefaultService);
    return DefaultService;
}());



/***/ }),

/***/ "./src/app/api/api/dispatcher.service.ts":
/*!***********************************************!*\
  !*** ./src/app/api/api/dispatcher.service.ts ***!
  \***********************************************/
/*! exports provided: DispatcherService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DispatcherService", function() { return DispatcherService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _encoder__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../encoder */ "./src/app/api/encoder.ts");
/* harmony import */ var _variables__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../variables */ "./src/app/api/variables.ts");
/* harmony import */ var _configuration__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../configuration */ "./src/app/api/configuration.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../environments/environment */ "./src/environments/environment.ts");
/**
 * CASN API
 * This is a test CASN API
 *
 * OpenAPI spec version: 1.0.0
 * Contact: katie@clinicaccess.org
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
/* tslint:disable:no-unused-variable member-ordering */
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};






var DispatcherService = /** @class */ (function () {
    function DispatcherService(httpClient, basePath, configuration) {
        this.httpClient = httpClient;
        this.basePath = _environments_environment__WEBPACK_IMPORTED_MODULE_5__["environment"].apiUrl;
        this.defaultHeaders = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]();
        this.configuration = new _configuration__WEBPACK_IMPORTED_MODULE_4__["Configuration"]();
        if (configuration) {
            this.configuration = configuration;
            this.configuration.basePath = configuration.basePath || basePath || this.basePath;
        }
        else {
            this.configuration.basePath = basePath || this.basePath;
        }
    }
    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    DispatcherService.prototype.canConsumeForm = function (consumes) {
        var form = 'multipart/form-data';
        for (var _i = 0, consumes_1 = consumes; _i < consumes_1.length; _i++) {
            var consume = consumes_1[_i];
            if (form === consume) {
                return true;
            }
        }
        return false;
    };
    DispatcherService.prototype.addAppointment = function (appointmentDTO, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [
            'application/json'
        ];
        var httpContentTypeSelected = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }
        return this.httpClient.post(this.configuration.basePath + "/dispatcher/appointments", appointmentDTO, {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.addDriver = function (body1, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [
            'application/json'
        ];
        var httpContentTypeSelected = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }
        return this.httpClient.post(this.configuration.basePath + "/drives/approve", body1, {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.addPatient = function (patient, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [
            'application/json'
        ];
        var httpContentTypeSelected = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }
        return this.httpClient.post(this.configuration.basePath + "/patient", patient, {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.dispatcherAppointmentsAppointmentIDDelete = function (appointmentID, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (appointmentID === null || appointmentID === undefined) {
            throw new Error('Required parameter appointmentID was null or undefined when calling dispatcherAppointmentsAppointmentIDDelete.');
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            '*/*'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.delete(this.configuration.basePath + "/dispatcher/appointments/" + encodeURIComponent(String(appointmentID)), {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.getAllAppointmentsForDispatcher = function (startDate, endDate, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var queryParameters = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]({ encoder: new _encoder__WEBPACK_IMPORTED_MODULE_2__["CustomHttpUrlEncodingCodec"]() });
        if (startDate !== undefined && startDate !== null) {
            queryParameters = queryParameters.set('startDate', startDate);
        }
        if (endDate !== undefined && endDate !== null) {
            queryParameters = queryParameters.set('endDate', endDate);
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/appointments", {
            params: queryParameters,
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.getAppointmentForDispatcherByID = function (appointmentID, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (appointmentID === null || appointmentID === undefined) {
            throw new Error('Required parameter appointmentID was null or undefined when calling getAppointmentForDispatcherByID.');
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/appointments/" + encodeURIComponent(String(appointmentID)), {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.getPatientByPatientIdentifier = function (patientIdentifier, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (patientIdentifier === null || patientIdentifier === undefined) {
            throw new Error('Required parameter patientIdentifier was null or undefined when calling getPatientByPatientIdentifier.');
        }
        var queryParameters = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]({ encoder: new _encoder__WEBPACK_IMPORTED_MODULE_2__["CustomHttpUrlEncodingCodec"]() });
        if (patientIdentifier !== undefined && patientIdentifier !== null) {
            queryParameters = queryParameters.set('patientIdentifier', patientIdentifier);
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/patient", {
            params: queryParameters,
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.getVolunteerDrives = function (driveId, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (driveId === null || driveId === undefined) {
            throw new Error('Required parameter driveId was null or undefined when calling getVolunteerDrives.');
        }
        var queryParameters = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]({ encoder: new _encoder__WEBPACK_IMPORTED_MODULE_2__["CustomHttpUrlEncodingCodec"]() });
        if (driveId !== undefined && driveId !== null) {
            queryParameters = queryParameters.set('driveId', driveId);
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/volunteerDrive", {
            params: queryParameters,
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService.prototype.updateAppointment = function (appointmentID, appointmentDTO, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (appointmentID === null || appointmentID === undefined) {
            throw new Error('Required parameter appointmentID was null or undefined when calling updateAppointment.');
        }
        if (appointmentDTO === null || appointmentDTO === undefined) {
            throw new Error('Required parameter appointmentDTO was null or undefined when calling updateAppointment.');
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [
            'application/json'
        ];
        var httpContentTypeSelected = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }
        return this.httpClient.put(this.configuration.basePath + "/dispatcher/appointments/" + encodeURIComponent(String(appointmentID)), appointmentDTO, {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DispatcherService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()), __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_variables__WEBPACK_IMPORTED_MODULE_3__["BASE_PATH"])), __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"], String, _configuration__WEBPACK_IMPORTED_MODULE_4__["Configuration"]])
    ], DispatcherService);
    return DispatcherService;
}());



/***/ }),

/***/ "./src/app/api/api/driver.service.ts":
/*!*******************************************!*\
  !*** ./src/app/api/api/driver.service.ts ***!
  \*******************************************/
/*! exports provided: DriverService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DriverService", function() { return DriverService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _encoder__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../encoder */ "./src/app/api/encoder.ts");
/* harmony import */ var _variables__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../variables */ "./src/app/api/variables.ts");
/* harmony import */ var _configuration__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../configuration */ "./src/app/api/configuration.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../../environments/environment */ "./src/environments/environment.ts");
/**
 * CASN API
 * This is a test CASN API
 *
 * OpenAPI spec version: 1.0.0
 * Contact: katie@clinicaccess.org
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
/* tslint:disable:no-unused-variable member-ordering */
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};






var DriverService = /** @class */ (function () {
    function DriverService(httpClient, basePath, configuration) {
        this.httpClient = httpClient;
        this.basePath = _environments_environment__WEBPACK_IMPORTED_MODULE_5__["environment"].apiUrl;
        this.defaultHeaders = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpHeaders"]();
        this.configuration = new _configuration__WEBPACK_IMPORTED_MODULE_4__["Configuration"]();
        if (configuration) {
            this.configuration = configuration;
            this.configuration.basePath = configuration.basePath || basePath || this.basePath;
        }
        else {
            this.configuration.basePath = basePath || this.basePath;
        }
    }
    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    DriverService.prototype.canConsumeForm = function (consumes) {
        var form = 'multipart/form-data';
        for (var _i = 0, consumes_1 = consumes; _i < consumes_1.length; _i++) {
            var consume = consumes_1[_i];
            if (form === consume) {
                return true;
            }
        }
        return false;
    };
    DriverService.prototype.addDriveApplicant = function (body, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [
            'application/json'
        ];
        var httpContentTypeSelected = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected !== undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }
        return this.httpClient.post(this.configuration.basePath + "/drives/apply", body, {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DriverService.prototype.getAllAppointmentsForDriver = function (startDate, endDate, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var queryParameters = new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]({ encoder: new _encoder__WEBPACK_IMPORTED_MODULE_2__["CustomHttpUrlEncodingCodec"]() });
        if (startDate !== undefined && startDate !== null) {
            queryParameters = queryParameters.set('startDate', startDate);
        }
        if (endDate !== undefined && endDate !== null) {
            queryParameters = queryParameters.set('endDate', endDate);
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/driver/appointments", {
            params: queryParameters,
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DriverService.prototype.getAppointmentForDriverByID = function (appointmentID, observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        if (appointmentID === null || appointmentID === undefined) {
            throw new Error('Required parameter appointmentID was null or undefined when calling getAppointmentForDriverByID.');
        }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/driver/appointments/" + encodeURIComponent(String(appointmentID)), {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DriverService.prototype.getMyDrives = function (observe, reportProgress) {
        if (observe === void 0) { observe = 'body'; }
        if (reportProgress === void 0) { reportProgress = false; }
        var headers = this.defaultHeaders;
        // authentication (BearerAuth) required
        if (this.configuration.username || this.configuration.password) {
            headers = headers.set('Authorization', 'Basic ' + btoa(this.configuration.username + ':' + this.configuration.password));
        }
        // to determine the Accept header
        var httpHeaderAccepts = [
            'application/json'
        ];
        var httpHeaderAcceptSelected = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected !== undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }
        // to determine the Content-Type header
        var consumes = [];
        return this.httpClient.get(this.configuration.basePath + "/driver/myDrives", {
            withCredentials: this.configuration.withCredentials,
            headers: headers,
            observe: observe,
            reportProgress: reportProgress
        });
    };
    DriverService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()), __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_variables__WEBPACK_IMPORTED_MODULE_3__["BASE_PATH"])), __param(2, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Optional"])()),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"], String, _configuration__WEBPACK_IMPORTED_MODULE_4__["Configuration"]])
    ], DriverService);
    return DriverService;
}());



/***/ }),

/***/ "./src/app/api/configuration.ts":
/*!**************************************!*\
  !*** ./src/app/api/configuration.ts ***!
  \**************************************/
/*! exports provided: Configuration */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "Configuration", function() { return Configuration; });
var Configuration = /** @class */ (function () {
    function Configuration(configurationParameters) {
        if (configurationParameters === void 0) { configurationParameters = {}; }
        this.apiKeys = configurationParameters.apiKeys;
        this.username = configurationParameters.username;
        this.password = configurationParameters.password;
        this.accessToken = configurationParameters.accessToken;
        this.basePath = configurationParameters.basePath;
        this.withCredentials = configurationParameters.withCredentials;
    }
    /**
     * Select the correct content-type to use for a request.
     * Uses {@link Configuration#isJsonMime} to determine the correct content-type.
     * If no content type is found return the first found type if the contentTypes is not empty
     * @param contentTypes - the array of content types that are available for selection
     * @returns the selected content-type or <code>undefined</code> if no selection could be made.
     */
    Configuration.prototype.selectHeaderContentType = function (contentTypes) {
        var _this = this;
        if (contentTypes.length === 0) {
            return undefined;
        }
        var type = contentTypes.find(function (x) { return _this.isJsonMime(x); });
        if (type === undefined) {
            return contentTypes[0];
        }
        return type;
    };
    /**
     * Select the correct accept content-type to use for a request.
     * Uses {@link Configuration#isJsonMime} to determine the correct accept content-type.
     * If no content type is found return the first found type if the contentTypes is not empty
     * @param accepts - the array of content types that are available for selection.
     * @returns the selected content-type or <code>undefined</code> if no selection could be made.
     */
    Configuration.prototype.selectHeaderAccept = function (accepts) {
        var _this = this;
        if (accepts.length === 0) {
            return undefined;
        }
        var type = accepts.find(function (x) { return _this.isJsonMime(x); });
        if (type === undefined) {
            return accepts[0];
        }
        return type;
    };
    /**
     * Check if the given MIME is a JSON MIME.
     * JSON MIME examples:
     *   application/json
     *   application/json; charset=UTF8
     *   APPLICATION/JSON
     *   application/vnd.company+json
     * @param mime - MIME (Multipurpose Internet Mail Extensions)
     * @return True if the given MIME is JSON, false otherwise.
     */
    Configuration.prototype.isJsonMime = function (mime) {
        var jsonMime = new RegExp('^(application\/json|[^;/ \t]+\/[^;/ \t]+[+]json)[ \t]*(;.*)?$', 'i');
        return mime !== null && (jsonMime.test(mime) || mime.toLowerCase() === 'application/json-patch+json');
    };
    return Configuration;
}());



/***/ }),

/***/ "./src/app/api/encoder.ts":
/*!********************************!*\
  !*** ./src/app/api/encoder.ts ***!
  \********************************/
/*! exports provided: CustomHttpUrlEncodingCodec */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CustomHttpUrlEncodingCodec", function() { return CustomHttpUrlEncodingCodec; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();

/**
* CustomHttpUrlEncodingCodec
* Fix plus sign (+) not encoding, so sent as blank space
* See: https://github.com/angular/angular/issues/11058#issuecomment-247367318
*/
var CustomHttpUrlEncodingCodec = /** @class */ (function (_super) {
    __extends(CustomHttpUrlEncodingCodec, _super);
    function CustomHttpUrlEncodingCodec() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    CustomHttpUrlEncodingCodec.prototype.encodeKey = function (k) {
        k = _super.prototype.encodeKey.call(this, k);
        return k.replace(/\+/gi, '%2B');
    };
    CustomHttpUrlEncodingCodec.prototype.encodeValue = function (v) {
        v = _super.prototype.encodeValue.call(this, v);
        return v.replace(/\+/gi, '%2B');
    };
    return CustomHttpUrlEncodingCodec;
}(_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpUrlEncodingCodec"]));



/***/ }),

/***/ "./src/app/api/variables.ts":
/*!**********************************!*\
  !*** ./src/app/api/variables.ts ***!
  \**********************************/
/*! exports provided: BASE_PATH, COLLECTION_FORMATS */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BASE_PATH", function() { return BASE_PATH; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "COLLECTION_FORMATS", function() { return COLLECTION_FORMATS; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");

var BASE_PATH = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["InjectionToken"]('basePath');
var COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
};


/***/ }),

/***/ "./src/app/app-routing.module.ts":
/*!***************************************!*\
  !*** ./src/app/app-routing.module.ts ***!
  \***************************************/
/*! exports provided: AppRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppRoutingModule", function() { return AppRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./auth-services/auth-guard.service */ "./src/app/auth-services/auth-guard.service.ts");
/* harmony import */ var _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./dashboard/dashboard.component */ "./src/app/dashboard/dashboard.component.ts");
/* harmony import */ var _rides_rides_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./rides/rides.component */ "./src/app/rides/rides.component.ts");
/* harmony import */ var _patients_patients_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./patients/patients.component */ "./src/app/patients/patients.component.ts");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./login/login.component */ "./src/app/login/login.component.ts");
/* harmony import */ var _appointments_appointments_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./appointments/appointments.component */ "./src/app/appointments/appointments.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








var routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full', canActivate: [_auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]] },
    { path: 'login', component: _login_login_component__WEBPACK_IMPORTED_MODULE_6__["LoginComponent"] },
    { path: 'dashboard', component: _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_3__["DashboardComponent"], canActivate: [_auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]] },
    { path: 'patient', component: _patients_patients_component__WEBPACK_IMPORTED_MODULE_5__["PatientsComponent"], canActivate: [_auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]] },
    { path: 'appointment', component: _appointments_appointments_component__WEBPACK_IMPORTED_MODULE_7__["AppointmentsComponent"], canActivate: [_auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]] },
    { path: 'view-schedule', component: _rides_rides_component__WEBPACK_IMPORTED_MODULE_4__["RidesComponent"], canActivate: [_auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]] },
    { path: '**', component: _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_3__["DashboardComponent"], canActivate: [_auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_2__["AuthGuard"]] }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forRoot(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());



/***/ }),

/***/ "./src/app/app.component.css":
/*!***********************************!*\
  !*** ./src/app/app.component.css ***!
  \***********************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".app-container {\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  left: 0;\n  right: 0;\n}\n\n.fill-remaining-space {\n  flex: 1 1 auto;\n}\n\n.logo {\n  height: 100%;\n}\n\n.side-menu {\n  display: flex;\n  flex-direction: column;\n}\n"

/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<mat-sidenav-container class=\"app-container\">\n<!--********************************************************************\n                          Sidenav Menu\n**********************************************************************-->\n  <mat-sidenav class=\"side-menu\"\n               #sidenav\n               mode=\"over\"\n               [(opened)]=\"opened\">\n    <button mat-raised-button\n            color=\"primary\"\n            routerLink=\"/\"\n            (click)=\"sidenav.close()\">My Dashboard</button>\n    <button mat-raised-button\n            color=\"primary\"\n            routerLink=\"/patient\" (click)=\"sidenav.close()\">Schedule a Ride</button>\n    <button mat-raised-button\n            color=\"primary\"\n            routerLink=\"/view-schedule\"\n            (click)=\"sidenav.close()\">View Schedule</button>\n    <button mat-raised-button\n            color=\"accent\"\n            routerLink=\"/login\"\n            (click)=\"sidenav.close()\">Log Out</button>\n  </mat-sidenav>\n<!--********************************************************************\n                          Main App Content\n**********************************************************************-->\n  <mat-sidenav-content>\n    <mat-toolbar color=\"primary\">\n      <img class=\"logo\"\n           src=\"assets/img/casn-logo.png\"\n           alt=\"CASN logo\"\n           routerLink=\"/\">\n      <span class=\"fill-remaining-space\"></span>\n      <mat-icon class=\"clicky\" (click)=\"sidenav.toggle()\">menu</mat-icon>\n    </mat-toolbar>\n    <main>\n      <router-outlet></router-outlet>\n    </main>\n  </mat-sidenav-content>\n</mat-sidenav-container>\n"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var AppComponent = /** @class */ (function () {
    function AppComponent() {
        this.title = 'casn-app';
    }
    AppComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-root',
            template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html"),
            styles: [__webpack_require__(/*! ./app.component.css */ "./src/app/app.component.css")]
        })
    ], AppComponent);
    return AppComponent;
}());



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/platform-browser/animations */ "./node_modules/@angular/platform-browser/fesm5/animations.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var ngx_mask__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ngx-mask */ "./node_modules/ngx-mask/fesm5/ngx-mask.js");
/* harmony import */ var ng_pick_datetime__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ng-pick-datetime */ "./node_modules/ng-pick-datetime/picker.js");
/* harmony import */ var _angular_material__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! @angular/material */ "./node_modules/@angular/material/esm5/material.es5.js");
/* harmony import */ var _app_routing_module__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./app-routing.module */ "./src/app/app-routing.module.ts");
/* harmony import */ var _auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./auth-services/auth-guard.service */ "./src/app/auth-services/auth-guard.service.ts");
/* harmony import */ var _auth_services_auth_service__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./auth-services/auth.service */ "./src/app/auth-services/auth.service.ts");
/* harmony import */ var _auth_services_fake_backend__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./auth-services/fake-backend */ "./src/app/auth-services/fake-backend.ts");
/* harmony import */ var _auth_services_jwt_interceptor__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./auth-services/jwt.interceptor */ "./src/app/auth-services/jwt.interceptor.ts");
/* harmony import */ var _auth_services_error_interceptor__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./auth-services/error.interceptor */ "./src/app/auth-services/error.interceptor.ts");
/* harmony import */ var _auth_services_user_service__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./auth-services/user.service */ "./src/app/auth-services/user.service.ts");
/* harmony import */ var _api_api_module__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./api/api.module */ "./src/app/api/api.module.ts");
/* harmony import */ var _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./dashboard/dashboard.component */ "./src/app/dashboard/dashboard.component.ts");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./login/login.component */ "./src/app/login/login.component.ts");
/* harmony import */ var _patients_patients_component__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./patients/patients.component */ "./src/app/patients/patients.component.ts");
/* harmony import */ var _rides_rides_component__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ./rides/rides.component */ "./src/app/rides/rides.component.ts");
/* harmony import */ var _appointments_appointments_component__WEBPACK_IMPORTED_MODULE_21__ = __webpack_require__(/*! ./appointments/appointments.component */ "./src/app/appointments/appointments.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};








/* Angular Material Components */

/* Routing */

/* Custom Services & HTTP Interceptors */







/* Custom Components */





var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"],
                _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_17__["DashboardComponent"],
                _patients_patients_component__WEBPACK_IMPORTED_MODULE_19__["PatientsComponent"],
                _rides_rides_component__WEBPACK_IMPORTED_MODULE_20__["RidesComponent"],
                _login_login_component__WEBPACK_IMPORTED_MODULE_18__["LoginComponent"],
                _appointments_appointments_component__WEBPACK_IMPORTED_MODULE_21__["AppointmentsComponent"]
            ],
            imports: [
                ngx_mask__WEBPACK_IMPORTED_MODULE_6__["NgxMaskModule"].forRoot(),
                ng_pick_datetime__WEBPACK_IMPORTED_MODULE_7__["OwlDateTimeModule"],
                ng_pick_datetime__WEBPACK_IMPORTED_MODULE_7__["OwlNativeDateTimeModule"],
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__["BrowserModule"],
                _angular_platform_browser_animations__WEBPACK_IMPORTED_MODULE_4__["BrowserAnimationsModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_5__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClientModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_5__["ReactiveFormsModule"],
                _app_routing_module__WEBPACK_IMPORTED_MODULE_9__["AppRoutingModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatAutocompleteModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatButtonModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatButtonToggleModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatCardModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatCheckboxModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatDatepickerModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatFormFieldModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatIconModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatInputModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatMenuModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatNativeDateModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatSelectModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatSidenavModule"],
                _angular_material__WEBPACK_IMPORTED_MODULE_8__["MatToolbarModule"],
            ],
            providers: [
                _api_api_module__WEBPACK_IMPORTED_MODULE_16__["ApiModule"],
                _auth_services_auth_guard_service__WEBPACK_IMPORTED_MODULE_10__["AuthGuard"],
                _auth_services_auth_service__WEBPACK_IMPORTED_MODULE_11__["AuthenticationService"],
                _auth_services_user_service__WEBPACK_IMPORTED_MODULE_15__["UserService"],
                { provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HTTP_INTERCEPTORS"], useClass: _auth_services_jwt_interceptor__WEBPACK_IMPORTED_MODULE_13__["JwtInterceptor"], multi: true },
                { provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HTTP_INTERCEPTORS"], useClass: _auth_services_error_interceptor__WEBPACK_IMPORTED_MODULE_14__["ErrorInterceptor"], multi: true },
                // provider used to create fake backend for dev
                _auth_services_fake_backend__WEBPACK_IMPORTED_MODULE_12__["fakeBackendProvider"]
            ],
            bootstrap: [_app_component__WEBPACK_IMPORTED_MODULE_3__["AppComponent"]]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/app/appointments/appointments.component.css":
/*!*********************************************************!*\
  !*** ./src/app/appointments/appointments.component.css ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "main {\n  display: flex;\n  flex-direction: column;\n  align-items: center;\n}\n\nform {\n  display: flex;\n  flex-direction: column;\n  margin: 10px;\n}\n\n.patient-form-group {\n  display: flex;\n  flex-direction: column;\n  margin: 10px;\n}\n\n.mat-form-field, .mat-checkbox {\n  margin: 5px 0;\n}\n\n.form-btn {\n  margin: 10px 5px;\n}\n\n.hide {\n  display: none;\n}\n"

/***/ }),

/***/ "./src/app/appointments/appointments.component.html":
/*!**********************************************************!*\
  !*** ./src/app/appointments/appointments.component.html ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<main>\n<!--********************************************************************\n                              Appt Form\n**********************************************************************-->\n  <h2>Create Appointment</h2>\n  <form [formGroup]=\"apptForm\" (ngSubmit)=\"onSubmit()\">\n    <div class=\"patient-form-group\">\n      <h3>Patient Identifier: {{ f.patientIdentifier.value }}</h3>\n      <mat-form-field class=\"example-full-width hide\"\n                      appearance=\"outline\"\n                      disabled>\n        <mat-label>Patient ID</mat-label>\n        <input type=\"text\"\n        placeholder=\"1234\"\n        aria-label=\"Patient ID\"\n        matInput\n        formControlName=\"patientIdentifier\">\n        <mat-error *ngIf=\"f.patientIdentifier.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n      <mat-form-field class=\"example-full-width hide\" appearance=\"outline\">\n        <mat-label>Dispatcher ID</mat-label>\n        <input type=\"number\"\n        placeholder=\"4321\"\n        aria-label=\"Dispatcher Identifier\"\n        matInput\n        formControlName=\"dispatcherId\">\n        <mat-error *ngIf=\"f.dispatcherId.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n    </div>\n\n  <div class=\"patient-form-group\">\n    <mat-form-field appearance=\"outline\">\n      <mat-label>Appointment Type</mat-label>\n      <mat-select placeholder=\"Appt Type\"\n                  aria-label=\"Appointment Type\"\n                  formControlName=\"appointmentTypeId\">\n        <mat-option *ngFor=\"let a of apptTypes\" [value]=\"a.value\">\n          {{ a.displayValue }}\n        </mat-option>\n      </mat-select>\n      <mat-error *ngIf=\"f.appointmentTypeId.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>Clinic</mat-label>\n      <mat-select placeholder=\"Clinic\"\n                  aria-label=\"Clinic\"\n                  formControlName=\"clinicId\">\n        <mat-option *ngFor=\"let c of clinics\" [value]=\"c.id\">\n          {{ c.name }}\n        </mat-option>\n      </mat-select>\n      <mat-error *ngIf=\"f.clinicId.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>Appointment Time</mat-label>\n      <input [owlDateTimeTrigger]=\"dt5\"\n             [owlDateTime]=\"dt5\"\n             matInput\n             placeholder=\"Choose a date\"\n             formControlName=\"appointmentDate\">\n      <owl-date-time [pickerMode]=\"'dialog'\" #dt5></owl-date-time>\n    </mat-form-field>\n\n    <!-- Pick Up Address -->\n    <h3>Patient Pick-Up Address</h3>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>Street Address</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"pickupAddress\"\n            aria-label=\"Street Address\"\n            placeholder=\"345 Main St, Apt F\">\n      <mat-error *ngIf=\"f.pickupAddress.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>City</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"pickupCity\"\n            aria-label=\"Pickup City\"\n            placeholder=\"Cypress\">\n      <mat-error *ngIf=\"f.pickupCity.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>State</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"pickupState\"\n            aria-label=\"Pickup State\"\n            placeholder=\"TX\">\n      <mat-error *ngIf=\"f.pickupState.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>Zip Code</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"pickupZipCode\"\n            aria-label=\"Pickup Zip Code\"\n            placeholder=\"77777\">\n      <mat-error *ngIf=\"f.pickupZipCode.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\">\n      <mat-label>Vague Pickup Location</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"pickupLocationVague\"\n            aria-label=\"Vague Pickup Location\"\n            placeholder=\"23rd and Main St.\">\n      <mat-error *ngIf=\"f.pickupLocationVague.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n\n    <!-- Drop Off Address -->\n    <h3>Patient Drop-Off Address</h3>\n    <mat-checkbox (change)=\"setDropoffLocation($event.checked)\">\n      Same as pickup address?\n    </mat-checkbox>\n    <mat-form-field appearance=\"outline\"\n                    [class.hide]=\"!showDropoffLocationInputs\">\n      <mat-label>Street Address</mat-label>\n      <input matInput\n             type=\"text\"\n             formControlName=\"dropoffAddress\"\n             aria-label=\"Street Address\"\n             placeholder=\"21 Park Dr. Ste 200\">\n      <mat-error *ngIf=\"f.dropoffAddress.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\"\n                    [class.hide]=\"!showDropoffLocationInputs\">\n      <mat-label>City</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"dropoffCity\"\n            aria-label=\"Dropoff City\"\n            placeholder=\"Cypress\">\n      <mat-error *ngIf=\"f.dropoffCity.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\"\n                    [class.hide]=\"!showDropoffLocationInputs\">\n      <mat-label>State</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"dropoffState\"\n            aria-label=\"Dropoff State\"\n            placeholder=\"TX\">\n      <mat-error *ngIf=\"f.dropoffState.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\"\n                    [class.hide]=\"!showDropoffLocationInputs\">\n      <mat-label>Zip Code</mat-label>\n      <input matInput\n            type=\"text\"\n            formControlName=\"dropoffZipCode\"\n            aria-label=\"Dropoff Zip Code\"\n            placeholder=\"77777\">\n      <mat-error *ngIf=\"f.dropoffZipCode.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n    <mat-form-field appearance=\"outline\"\n                    [class.hide]=\"!showDropoffLocationInputs\">\n      <mat-label>Vague Dropoff Location</mat-label>\n      <input matInput\n             type=\"text\"\n             formControlName=\"dropoffLocationVague\"\n             aria-label=\"Vague Dropoff Location\"\n             placeholder=\"Yale St. off I-45\">\n      <mat-error *ngIf=\"f.dropoffLocationVague.errors?.required\">This field is required.</mat-error>\n    </mat-form-field>\n\n\n    <button class=\"form-btn\"\n            mat-raised-button\n            color=\"primary\"\n            type=\"submit\"\n            [disabled]=\"!apptForm.valid\">Submit</button>\n    <button mat-raised-button\n            type=\"button\"\n            color=\"warn\"\n            class=\"form-btn\"\n            (click)=\"handleCancelClick()\">Cancel</button>\n    </div>\n  </form>\n  <!-- TODO: Replace with something less hacky -->\n  <br><br><br><br><br><br><br><Br><br><br><br><br>\n  <br><br><br><br><br><br><br><Br><br><br><br><br>\n  <br><br><br><br><br><br><br><Br><br><br><br><br>\n\n  <!--********************************************************************\n                              Form Debug\n  **********************************************************************-->\n  <div class=\"debug\">\n    <p>Form Status: {{ apptForm.status }}</p>\n    <p>Form Value: {{ apptForm.value | json }}</p>\n  </div>\n</main>\n"

/***/ }),

/***/ "./src/app/appointments/appointments.component.ts":
/*!********************************************************!*\
  !*** ./src/app/appointments/appointments.component.ts ***!
  \********************************************************/
/*! exports provided: AppointmentsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppointmentsComponent", function() { return AppointmentsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _api_api_dispatcher_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../api/api/dispatcher.service */ "./src/app/api/api/dispatcher.service.ts");
/* harmony import */ var _api_api_default_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../api/api/default.service */ "./src/app/api/api/default.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var AppointmentsComponent = /** @class */ (function () {
    /*********************************************************************
                        Constructor, Lifecycle Hooks
    **********************************************************************/
    function AppointmentsComponent(ds, defaultService, fb, route, location, router) {
        this.ds = ds;
        this.defaultService = defaultService;
        this.fb = fb;
        this.route = route;
        this.location = location;
        this.router = router;
        // Hide dropoff inputs when user checks 'same as pickup location'
        this.showDropoffLocationInputs = true;
        /*********************************************************************
                                      Form
        **********************************************************************/
        this.clinics = [];
        // TODO: These should be fetched from API endpoint or set as app-level constants
        this.apptTypes = [{ value: 4, displayValue: 'Ultrasound' },
            { value: 3, displayValue: 'Surgical' },
        ];
        this.apptForm = this.fb.group({
            appointmentTypeId: [3, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            patientId: [0],
            patientIdentifier: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            // TODO: Get dispatcherId from localstorage
            dispatcherId: [5],
            clinicId: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            appointmentDate: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            pickupAddress: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            pickupCity: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            pickupState: ['TX', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            pickupZipCode: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            dropoffAddress: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            dropoffCity: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            dropoffState: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            dropoffZipCode: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            pickupLocationVague: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            dropoffLocationVague: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
        });
    }
    AppointmentsComponent.prototype.ngOnInit = function () {
        this.getPatient();
        this.getClinics();
    };
    Object.defineProperty(AppointmentsComponent.prototype, "f", {
        // convenience getter for easy access to form fields
        get: function () { return this.apptForm.controls; },
        enumerable: true,
        configurable: true
    });
    AppointmentsComponent.prototype.onSubmit = function () {
        if (!this.apptForm.valid)
            return;
        this.constructApptDTO();
    };
    AppointmentsComponent.prototype.setDropoffLocation = function (sameAsPickup) {
        if (sameAsPickup) {
            this.showDropoffLocationInputs = false;
            this.f.dropoffAddress.setValue(this.f.pickupAddress.value);
            this.f.dropoffCity.setValue(this.f.pickupCity.value);
            this.f.dropoffState.setValue(this.f.pickupState.value);
            this.f.dropoffZipCode.setValue(this.f.pickupZipCode.value);
            this.f.dropoffLocationVague.setValue(this.f.pickupLocationVague.value);
        }
        else {
            this.showDropoffLocationInputs = true;
            this.f.dropoffAddress.reset();
            this.f.dropoffCity.reset();
            this.f.dropoffState.reset();
            this.f.dropoffZipCode.reset();
            this.f.dropoffLocationVague.reset();
        }
    };
    // TODO: Refactor this eventually.
    AppointmentsComponent.prototype.constructApptDTO = function () {
        this.apptDTO = {};
        this.apptDTO.appointment = {
            appointmentTypeId: this.f.appointmentTypeId.value,
            patientId: this.f.patientId.value,
            dispatcherId: this.f.dispatcherId.value,
            clinicId: this.f.clinicId.value,
            appointmentDate: this.f.appointmentDate.value.toISOString(),
            pickupLocationVague: this.f.pickupLocationVague.value,
            dropoffLocationVague: this.f.dropoffLocationVague.value
        };
        this.apptDTO.driveTo = {
            direction: 1,
            startAddress: this.f.pickupAddress.value,
            startCity: this.f.pickupCity.value,
            startState: this.f.pickupState.value,
            startPostalCode: this.f.pickupZipCode.value,
            endAddress: "",
            endCity: "",
            endState: "",
            endPostalCode: "",
        };
        this.apptDTO.driveFrom = {
            direction: 2,
            endAddress: this.f.dropoffAddress.value,
            endCity: this.f.dropoffCity.value,
            endState: this.f.dropoffState.value,
            endPostalCode: this.f.dropoffZipCode.value,
            startAddress: "",
            startCity: "",
            startState: "",
            startPostalCode: "",
        };
        this.saveNewAppt();
    };
    /*********************************************************************
                                Click Handlers
    **********************************************************************/
    AppointmentsComponent.prototype.handleCancelClick = function () {
        if (confirm('Are you sure? Any unsaved changes will be lost.')) {
            this.apptForm.reset();
            this.goBack();
        }
    };
    /*********************************************************************
                              Service Calls
    **********************************************************************/
    AppointmentsComponent.prototype.goBack = function () {
        this.location.back();
    };
    AppointmentsComponent.prototype.getPatient = function () {
        this.patientId = parseInt(this.route.snapshot.paramMap.get('patientId'));
        this.patientIdentifier = this.route.snapshot.paramMap.get('patientIdentifier');
        this.f.patientId.setValue(this.patientId);
        this.f.patientIdentifier.setValue(this.patientIdentifier);
    };
    AppointmentsComponent.prototype.getClinics = function () {
        var _this = this;
        this.defaultService.getClinics().subscribe(function (data) {
            _this.clinics = data;
        }, function (err) {
            console.error("--Error fetching clinics...", err);
            alert('An error occurred while fetching clinics. Please refresh & try again.');
        });
    };
    AppointmentsComponent.prototype.saveNewAppt = function () {
        var _this = this;
        this.ds.addAppointment(this.apptDTO).subscribe(function (data) {
            console.log("Save appt response is", data);
            alert('Success! Your appointment has been saved.');
            _this.router.navigate(['']);
        }, function (err) {
            console.error("--Error saving appt data...", err);
            alert('An error occurred, and your appointment was not saved.');
        });
    };
    AppointmentsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-appointments',
            template: __webpack_require__(/*! ./appointments.component.html */ "./src/app/appointments/appointments.component.html"),
            styles: [__webpack_require__(/*! ./appointments.component.css */ "./src/app/appointments/appointments.component.css")]
        }),
        __metadata("design:paramtypes", [_api_api_dispatcher_service__WEBPACK_IMPORTED_MODULE_2__["DispatcherService"],
            _api_api_default_service__WEBPACK_IMPORTED_MODULE_3__["DefaultService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            _angular_router__WEBPACK_IMPORTED_MODULE_4__["ActivatedRoute"],
            _angular_common__WEBPACK_IMPORTED_MODULE_5__["Location"],
            _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"]])
    ], AppointmentsComponent);
    return AppointmentsComponent;
}());



/***/ }),

/***/ "./src/app/auth-services/auth-guard.service.ts":
/*!*****************************************************!*\
  !*** ./src/app/auth-services/auth-guard.service.ts ***!
  \*****************************************************/
/*! exports provided: AuthGuard */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthGuard", function() { return AuthGuard; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth-services/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AuthGuard = /** @class */ (function () {
    function AuthGuard(authService, router) {
        this.authService = authService;
        this.router = router;
    }
    AuthGuard.prototype.canActivate = function (route, state) {
        if (localStorage.getItem('currentUser')) {
            // logged in so return true
            return true;
        }
        // not logged in so redirect to login page with the return url
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
    };
    AuthGuard = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [_auth_service__WEBPACK_IMPORTED_MODULE_2__["AuthenticationService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], AuthGuard);
    return AuthGuard;
}());



/***/ }),

/***/ "./src/app/auth-services/auth.service.ts":
/*!***********************************************!*\
  !*** ./src/app/auth-services/auth.service.ts ***!
  \***********************************************/
/*! exports provided: AuthenticationService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthenticationService", function() { return AuthenticationService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../environments/environment */ "./src/environments/environment.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var AuthenticationService = /** @class */ (function () {
    function AuthenticationService(http) {
        this.http = http;
        this.loggedIn = new rxjs__WEBPACK_IMPORTED_MODULE_2__["BehaviorSubject"](false);
    }
    Object.defineProperty(AuthenticationService.prototype, "isLoggedIn", {
        get: function () {
            if (localStorage.getItem('currentUser')) {
                this.loggedIn.next(true);
            }
            return this.loggedIn.asObservable();
        },
        enumerable: true,
        configurable: true
    });
    AuthenticationService.prototype.login = function (username, password) {
        var _this = this;
        return this.http.post(_environments_environment__WEBPACK_IMPORTED_MODULE_4__["environment"].apiUrl + "/login", { username: username, password: password }).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["map"])(function (user) {
            // login successful if there's a jwt token in the response
            if (user && user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                _this.loggedIn.next(true);
            }
            return user;
        }));
    };
    AuthenticationService.prototype.logout = function () {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.loggedIn.next(false);
    };
    AuthenticationService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
    ], AuthenticationService);
    return AuthenticationService;
}());



/***/ }),

/***/ "./src/app/auth-services/error.interceptor.ts":
/*!****************************************************!*\
  !*** ./src/app/auth-services/error.interceptor.ts ***!
  \****************************************************/
/*! exports provided: ErrorInterceptor */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ErrorInterceptor", function() { return ErrorInterceptor; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _auth_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./auth.service */ "./src/app/auth-services/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var ErrorInterceptor = /** @class */ (function () {
    function ErrorInterceptor(authenticationService) {
        this.authenticationService = authenticationService;
    }
    ErrorInterceptor.prototype.intercept = function (request, next) {
        var _this = this;
        return next.handle(request).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_2__["catchError"])(function (err) {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                _this.authenticationService.logout();
                location.reload(true);
            }
            var error = err.error.message || err.statusText;
            return Object(rxjs__WEBPACK_IMPORTED_MODULE_1__["throwError"])(error);
        }));
    };
    ErrorInterceptor = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [_auth_service__WEBPACK_IMPORTED_MODULE_3__["AuthenticationService"]])
    ], ErrorInterceptor);
    return ErrorInterceptor;
}());



/***/ }),

/***/ "./src/app/auth-services/fake-backend.ts":
/*!***********************************************!*\
  !*** ./src/app/auth-services/fake-backend.ts ***!
  \***********************************************/
/*! exports provided: FakeBackendInterceptor, fakeBackendProvider */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FakeBackendInterceptor", function() { return FakeBackendInterceptor; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "fakeBackendProvider", function() { return fakeBackendProvider; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var rxjs__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! rxjs */ "./node_modules/rxjs/_esm5/index.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var FakeBackendInterceptor = /** @class */ (function () {
    function FakeBackendInterceptor() {
    }
    FakeBackendInterceptor.prototype.intercept = function (request, next) {
        // array in local storage for registered users
        var users = JSON.parse(localStorage.getItem('users')) || [{ username: 'test', password: 'test' }];
        // wrap in delayed observable to simulate server api call
        return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(null).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["mergeMap"])(function () {
            // authenticate
            if (request.url.endsWith('/login') && request.method === 'POST') {
                // find if any user matches login credentials
                var filteredUsers = users.filter(function (user) {
                    return user.username === request.body.username && user.password === request.body.password;
                });
                if (filteredUsers.length) {
                    // if login details are valid return 200 OK with user details and fake jwt token
                    var user = filteredUsers[0];
                    var body = {
                        id: user.id,
                        username: user.username,
                        firstName: user.firstName,
                        lastName: user.lastName,
                        token: 'fake-jwt-token'
                    };
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpResponse"]({ status: 200, body: body }));
                }
                else {
                    // else return 400 bad request
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])({ error: { message: 'Username or password is incorrect' } });
                }
            }
            // get users
            if (request.url.endsWith('/users') && request.method === 'GET') {
                // check for fake auth token in header and return users if valid, this security is implemented server side in a real application
                if (request.headers.get('Authorization') === 'Bearer fake-jwt-token') {
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpResponse"]({ status: 200, body: users }));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])({ error: { message: 'Unauthorised' } });
                }
            }
            // get user by id
            if (request.url.match(/\/users\/\d+$/) && request.method === 'GET') {
                // check for fake auth token in header and return user if valid, this security is implemented server side in a real application
                if (request.headers.get('Authorization') === 'Bearer fake-jwt-token') {
                    // find user by id in users array
                    var urlParts = request.url.split('/');
                    var id_1 = parseInt(urlParts[urlParts.length - 1]);
                    var matchedUsers = users.filter(function (user) { return user.id === id_1; });
                    var user = matchedUsers.length ? matchedUsers[0] : null;
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpResponse"]({ status: 200, body: user }));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])({ error: { message: 'Unauthorised' } });
                }
            }
            // register user
            if (request.url.endsWith('/users/register') && request.method === 'POST') {
                // get new user object from post body
                var newUser_1 = request.body;
                // validation
                var duplicateUser = users.filter(function (user) { return user.username === newUser_1.username; }).length;
                if (duplicateUser) {
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])({ error: { message: 'Username "' + newUser_1.username + '" is already taken' } });
                }
                // save new user
                newUser_1.id = users.length + 1;
                users.push(newUser_1);
                localStorage.setItem('users', JSON.stringify(users));
                // respond 200 OK
                return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpResponse"]({ status: 200 }));
            }
            // delete user
            if (request.url.match(/\/users\/\d+$/) && request.method === 'DELETE') {
                // check for fake auth token in header and return user if valid, this security is implemented server side in a real application
                if (request.headers.get('Authorization') === 'Bearer fake-jwt-token') {
                    // find user by id in users array
                    var urlParts = request.url.split('/');
                    var id = parseInt(urlParts[urlParts.length - 1]);
                    for (var i = 0; i < users.length; i++) {
                        var user = users[i];
                        if (user.id === id) {
                            // delete user
                            users.splice(i, 1);
                            localStorage.setItem('users', JSON.stringify(users));
                            break;
                        }
                    }
                    // respond 200 OK
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["of"])(new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpResponse"]({ status: 200 }));
                }
                else {
                    // return 401 not authorised if token is null or invalid
                    return Object(rxjs__WEBPACK_IMPORTED_MODULE_2__["throwError"])({ error: { message: 'Unauthorised' } });
                }
            }
            // pass through any requests not handled above
            return next.handle(request);
        }))
            // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["materialize"])())
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["delay"])(500))
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["dematerialize"])());
    };
    FakeBackendInterceptor = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [])
    ], FakeBackendInterceptor);
    return FakeBackendInterceptor;
}());

var fakeBackendProvider = {
    // use fake backend in place of Http service for backend-less development
    provide: _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HTTP_INTERCEPTORS"],
    useClass: FakeBackendInterceptor,
    multi: true
};


/***/ }),

/***/ "./src/app/auth-services/jwt.interceptor.ts":
/*!**************************************************!*\
  !*** ./src/app/auth-services/jwt.interceptor.ts ***!
  \**************************************************/
/*! exports provided: JwtInterceptor */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "JwtInterceptor", function() { return JwtInterceptor; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var JwtInterceptor = /** @class */ (function () {
    function JwtInterceptor() {
    }
    JwtInterceptor.prototype.intercept = function (request, next) {
        // add authorization header with jwt token if available
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            request = request.clone({
                setHeaders: {
                    Authorization: "Bearer " + currentUser.token
                }
            });
        }
        return next.handle(request);
    };
    JwtInterceptor = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])()
    ], JwtInterceptor);
    return JwtInterceptor;
}());



/***/ }),

/***/ "./src/app/auth-services/user.service.ts":
/*!***********************************************!*\
  !*** ./src/app/auth-services/user.service.ts ***!
  \***********************************************/
/*! exports provided: UserService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserService", function() { return UserService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../environments/environment */ "./src/environments/environment.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var UserService = /** @class */ (function () {
    function UserService(http) {
        this.http = http;
    }
    UserService.prototype.getAll = function () {
        return this.http.get(_environments_environment__WEBPACK_IMPORTED_MODULE_2__["environment"].apiUrl + "/users");
    };
    UserService.prototype.getById = function (id) {
        return this.http.get(_environments_environment__WEBPACK_IMPORTED_MODULE_2__["environment"].apiUrl + "/users/" + id);
    };
    UserService.prototype.register = function (user) {
        return this.http.post(_environments_environment__WEBPACK_IMPORTED_MODULE_2__["environment"].apiUrl + "/users/register", user);
    };
    UserService.prototype.update = function (user) {
        return this.http.put(_environments_environment__WEBPACK_IMPORTED_MODULE_2__["environment"].apiUrl + "/users/" + user.id, user);
    };
    UserService.prototype.delete = function (id) {
        return this.http.delete(_environments_environment__WEBPACK_IMPORTED_MODULE_2__["environment"].apiUrl + "/users/" + id);
    };
    UserService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])(),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
    ], UserService);
    return UserService;
}());



/***/ }),

/***/ "./src/app/dashboard/dashboard.component.css":
/*!***************************************************!*\
  !*** ./src/app/dashboard/dashboard.component.css ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "main {\n  display: flex;\n  flex-direction: column;\n  min-height: 90vh;\n  align-items: center;\n  justify-content: space-around;\n}\n\n.btn-dash {\n  width: 250px;\n  height: 75px;\n  border-radius: 50px;\n}\n"

/***/ }),

/***/ "./src/app/dashboard/dashboard.component.html":
/*!****************************************************!*\
  !*** ./src/app/dashboard/dashboard.component.html ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<main>\n  <button class=\"btn-dash\"\n          mat-raised-button\n          color=\"primary\"\n          routerLink=\"/patient\">\n          <mat-icon>departure_board</mat-icon> Schedule a Ride\n  </button>\n  <button class=\"btn-dash\"\n          mat-raised-button\n          color=\"primary\"\n          routerLink=\"/view-schedule\">\n          <mat-icon>event_note</mat-icon> View Schedule\n  </button>\n</main>\n"

/***/ }),

/***/ "./src/app/dashboard/dashboard.component.ts":
/*!**************************************************!*\
  !*** ./src/app/dashboard/dashboard.component.ts ***!
  \**************************************************/
/*! exports provided: DashboardComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DashboardComponent", function() { return DashboardComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var DashboardComponent = /** @class */ (function () {
    function DashboardComponent() {
    }
    DashboardComponent.prototype.ngOnInit = function () {
    };
    DashboardComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-dashboard',
            template: __webpack_require__(/*! ./dashboard.component.html */ "./src/app/dashboard/dashboard.component.html"),
            styles: [__webpack_require__(/*! ./dashboard.component.css */ "./src/app/dashboard/dashboard.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], DashboardComponent);
    return DashboardComponent;
}());



/***/ }),

/***/ "./src/app/login/login.component.css":
/*!*******************************************!*\
  !*** ./src/app/login/login.component.css ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/login/login.component.html":
/*!********************************************!*\
  !*** ./src/app/login/login.component.html ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<main>\n  <mat-card style=\"min-height:100vh\">\n    <!-- <h2>Please log in to proceed</h2> -->\n    <div *ngIf=\"errorMsg\">\n      <small>{{ errorMsg }}</small>\n    </div>\n    <form [formGroup]=\"loginForm\" (ngSubmit)=\"onSubmit()\">\n      <div class=\"form-group\">\n        <mat-form-field appearance=\"outline\">\n          <mat-label for=\"username\">Username</mat-label>\n          <input matInput type=\"text\" formControlName=\"username\" class=\"form-control\" [ngClass]=\"{ 'is-invalid': submitted && f.username.errors }\" />\n          <mat-hint *ngIf=\"submitted && f.username.errors\" class=\"invalid-feedback\">\n            <small *ngIf=\"f.username.errors.required\">Username is required</small>\n          </mat-hint>\n        </mat-form-field>\n      </div>\n      <div class=\"form-group\">\n        <mat-form-field appearance=\"outline\">\n          <mat-label for=\"password\">Password</mat-label>\n          <input matInput type=\"password\" formControlName=\"password\" class=\"form-control\" [ngClass]=\"{ 'is-invalid': submitted && f.password.errors }\" />\n          <mat-hint *ngIf=\"submitted && f.password.errors\" class=\"invalid-feedback\">\n            <small *ngIf=\"f.password.errors.required\">Password is required</small>\n          </mat-hint>\n        </mat-form-field>\n      </div>\n      <div class=\"form-group\">\n        <button mat-raised-button color=\"primary\" [disabled]=\"loading\" class=\"btn btn-primary\">Log In</button>\n        <img *ngIf=\"loading\" src=\"data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==\" />\n      </div>\n    </form>\n  </mat-card>\n</main>\n"

/***/ }),

/***/ "./src/app/login/login.component.ts":
/*!******************************************!*\
  !*** ./src/app/login/login.component.ts ***!
  \******************************************/
/*! exports provided: LoginComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "LoginComponent", function() { return LoginComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var rxjs_operators__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! rxjs/operators */ "./node_modules/rxjs/_esm5/operators/index.js");
/* harmony import */ var _auth_services_auth_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../auth-services/auth.service */ "./src/app/auth-services/auth.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var LoginComponent = /** @class */ (function () {
    function LoginComponent(formBuilder, route, router, authenticationService) {
        this.formBuilder = formBuilder;
        this.route = route;
        this.router = router;
        this.authenticationService = authenticationService;
        this.loading = false;
        this.submitted = false;
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.loginForm = this.formBuilder.group({
            username: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required],
            password: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_2__["Validators"].required]
        });
        // reset login status
        this.authenticationService.logout();
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    };
    Object.defineProperty(LoginComponent.prototype, "f", {
        // convenience getter for easy access to form fields
        get: function () { return this.loginForm.controls; },
        enumerable: true,
        configurable: true
    });
    LoginComponent.prototype.onSubmit = function () {
        var _this = this;
        this.submitted = true;
        // stop here if form is invalid
        if (this.loginForm.invalid) {
            return;
        }
        this.loading = true;
        this.authenticationService.login(this.f.username.value, this.f.password.value)
            .pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_3__["first"])())
            .subscribe(function (data) {
            _this.router.navigate([_this.returnUrl]);
        }, function (error) {
            // TODO: Add some error handling/user msgs
            _this.errorMsg = "Incorrect username/password. Please try again.";
            _this.loading = false;
        });
    };
    LoginComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-login',
            template: __webpack_require__(/*! ./login.component.html */ "./src/app/login/login.component.html"),
            styles: [__webpack_require__(/*! ./login.component.css */ "./src/app/login/login.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _auth_services_auth_service__WEBPACK_IMPORTED_MODULE_4__["AuthenticationService"]])
    ], LoginComponent);
    return LoginComponent;
}());



/***/ }),

/***/ "./src/app/patients/patients.component.css":
/*!*************************************************!*\
  !*** ./src/app/patients/patients.component.css ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "main {\n  display: flex;\n  flex-direction: column;\n  align-items: center;\n}\n\nform {\n  display: flex;\n  flex-direction: column;\n  margin: 10px;\n}\n\n.patient-form-group {\n  display: flex;\n  flex-direction: column;\n  margin: 10px;\n}\n\n.mat-form-field, .mat-checkbox {\n  margin: 5px 0;\n}\n\n.form-btn {\n  margin: 10px 5px;\n}\n\n.hide {\n  display: none;\n}\n"

/***/ }),

/***/ "./src/app/patients/patients.component.html":
/*!**************************************************!*\
  !*** ./src/app/patients/patients.component.html ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<main>\n<!--********************************************************************\n                        Patient Search Field\n**********************************************************************-->\n  <h2>Schedule A Ride</h2>\n  <h3>Add or Lookup Patient</h3>\n  <div class=\"patient-form-group\" *ngIf=\"!displayPatientForm\">\n    <mat-form-field class=\"example-full-width\" appearance=\"outline\">\n      <mat-label>Patient Identifier</mat-label>\n      <input type=\"text\"\n      placeholder=\"1234\"\n      aria-label=\"Patient Identifier\"\n      matInput\n      [formControl]=\"patientIdentifierSearch\">\n      <mat-error *ngIf=\"patientIdentifierSearch.errors?.required\">This field is required.</mat-error>\n      <mat-error *ngIf=\"patientIdentifierSearch.errors?.minlength || f.patientIdentifier.errors?.maxlength\">ID should be 4-5 digits.</mat-error>\n    </mat-form-field>\n\n    <button class=\"form-btn\"\n            mat-raised-button\n            color=\"primary\"\n            [disabled]=\"patientIdentifierSearch.invalid\"\n            *ngIf=\"!displayPatientForm\"\n            (click)=\"searchPatientIdentifier()\">Next</button>\n  </div>\n\n<!--********************************************************************\n                            Patient Detail Form\n**********************************************************************-->\n  <form [formGroup]=\"patientForm\"\n        (ngSubmit)=\"onSubmit()\"\n        *ngIf=\"displayPatientForm\">\n    <div class=\"patient-form-group\">\n      <mat-form-field class=\"example-full-width\" appearance=\"outline\">\n        <mat-label>Patient Identifier</mat-label>\n        <input type=\"text\"\n        placeholder=\"1234\"\n        aria-label=\"Patient Identifier\"\n        matInput\n        formControlName=\"patientIdentifier\">\n        <mat-error *ngIf=\"f.patientIdentifier.errors?.required\">This field is required.</mat-error>\n        <mat-error *ngIf=\"f.patientIdentifier.errors?.minlength || f.patientIdentifier.errors?.maxlength\">ID should be 4-5 digits.</mat-error>\n      </mat-form-field>\n    </div>\n\n  <div class=\"patient-form-group\">\n      <mat-form-field appearance=\"outline\">\n        <mat-label>First Name</mat-label>\n        <input matInput\n              type=\"text\"\n              formControlName=\"firstName\"\n              aria-label=\"First Name\"\n              placeholder=\"Jane\">\n        <mat-error *ngIf=\"submitted && f.firstName.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n      <mat-form-field appearance=\"outline\">\n        <mat-label>Last Name</mat-label>\n        <input matInput\n               type=\"text\"\n               formControlName=\"lastName\"\n               aria-label=\"Last Name\"\n               placeholder=\"Doe\">\n        <mat-error *ngIf=\"f.lastName.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n      <mat-form-field appearance=\"outline\">\n        <mat-label>Phone Number</mat-label>\n        <input matInput\n               type=\"text\"\n               formControlName=\"phone\"\n               mask=\"000-000-0000\"\n               aria-label=\"Phone Number\"\n               placeholder=\"Phone Number\">\n        <mat-error *ngIf=\"f.phone.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n      <mat-checkbox formControlName=\"isMinor\"\n                    aria-label=\"Minor\">Minor?</mat-checkbox>\n      <mat-form-field appearance=\"outline\">\n        <mat-label>Preferred Language</mat-label>\n        <mat-select placeholder=\"Language\"\n                    aria-label=\"Preferred Language\"\n                    formControlName=\"preferredLanguage\">\n          <mat-option *ngFor=\"let l of languages\" [value]=\"l\">\n            {{ l }}\n          </mat-option>\n        </mat-select>\n        <mat-error *ngIf=\"f.preferredLanguage.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n      <mat-form-field appearance=\"outline\">\n        <mat-label>Preferred Contact Method</mat-label>\n        <mat-select placeholder=\"Contact Method\"\n                    aria-label=\"Preferred Contact Method\"\n                    formControlName=\"preferredContactMethod\">\n          <mat-option *ngFor=\"let c of contactMethods\" [value]=\"c.value\">\n            {{ c.displayValue }}\n          </mat-option>\n        </mat-select>\n        <mat-error *ngIf=\"f.preferredContactMethod.errors?.required\">This field is required.</mat-error>\n      </mat-form-field>\n      <button class=\"form-btn\"\n              mat-raised-button\n              color=\"primary\"\n              type=\"submit\"\n              [disabled]=\"!patientForm.valid\">Next</button>\n      <button mat-raised-button\n              color=\"warn\"\n              class=\"form-btn\"\n              (click)=\"handleCancelClick()\">Cancel</button>\n    </div>\n  </form>\n\n  <!--********************************************************************\n                              Form Debug\n  **********************************************************************-->\n  <div class=\"debug\">\n    <p>Form Status: {{ patientForm.status }}</p>\n    <p>Form Value: {{ patientForm.value | json }}</p>\n  </div>\n</main>\n\n<!--********************************************************************\n                            Patient Modal\n**********************************************************************-->\n<div *ngIf=\"displayPatientFoundModal\" class=\"modal\">\n  <div class=\"modal-content\">\n    <h2>Found Matching Patient: {{ existingPatient.firstName }} {{ existingPatient.lastName }}</h2>\n    <p>Is this your patient?</p>\n    <div class=\"modal-ctrl-btns\">\n      <button mat-raised-button\n              color=\"primary\"\n              (click)=\"handleYesClick()\">Yes</button>\n      <button mat-raised-button\n              color=\"accent\"\n              (click)=\"handleNoClick()\">No, I'll create a different ID.</button>\n    </div>\n  </div>\n</div>\n"

/***/ }),

/***/ "./src/app/patients/patients.component.ts":
/*!************************************************!*\
  !*** ./src/app/patients/patients.component.ts ***!
  \************************************************/
/*! exports provided: PatientsComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PatientsComponent", function() { return PatientsComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _api_api_dispatcher_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../api/api/dispatcher.service */ "./src/app/api/api/dispatcher.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


// import { map, catchError } from 'rxjs/operators';



var PatientsComponent = /** @class */ (function () {
    /*********************************************************************
                        Constructor, Lifecycle Hooks
    **********************************************************************/
    function PatientsComponent(ds, fb, location, router) {
        this.ds = ds;
        this.fb = fb;
        this.location = location;
        this.router = router;
        this.existingPatient = {};
        /* Display flags for patient lookup feature */
        this.displayPatientFoundModal = false;
        this.displayPatientForm = false;
        /*********************************************************************
                                      Form
        **********************************************************************/
        // TODO: Remove French? Was used bc dummy data includes it.
        this.languages = ['English', 'Spanish', 'French', 'Other'];
        this.contactMethods = [{ value: 1, displayValue: 'Text' },
            { value: 2, displayValue: 'Call' },
            { value: 3, displayValue: 'Email' }];
        this.patientIdentifierSearch = new _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormControl"]('', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].minLength(4), _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(6)]);
        this.patientForm = this.fb.group({
            patientIdentifier: ['', [_angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].minLength(4),
                    _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].maxLength(6)]],
            firstName: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            lastName: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            phone: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].pattern("^((\\+91-?)|0)?[0-9]{10}$")],
            // email: [''],
            isMinor: [false, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            preferredLanguage: ['English', _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
            preferredContactMethod: [1, _angular_forms__WEBPACK_IMPORTED_MODULE_1__["Validators"].required],
        });
    }
    PatientsComponent.prototype.ngOnInit = function () { };
    Object.defineProperty(PatientsComponent.prototype, "f", {
        // convenience getter for easy access to form fields
        get: function () { return this.patientForm.controls; },
        enumerable: true,
        configurable: true
    });
    PatientsComponent.prototype.onSubmit = function () {
        if (!this.patientForm.valid) {
            return;
        }
        // Check if patient is new or existing to make appropriate REST call.
        var isNewPatient = Object.keys(this.existingPatient).length === 0;
        if (isNewPatient) {
            this.saveNewPatient();
        }
        else {
            // TODO: There should be an update patient endpoint
            this.router.navigate(['/appointment', { patientIdentifier: this.f.patientIdentifier.value, patientId: this.existingPatientId }]);
        }
    };
    /*********************************************************************
                                Click Handlers
    **********************************************************************/
    PatientsComponent.prototype.handleYesClick = function () {
        this.displayPatientFoundModal = false;
        this.displayPatientForm = true;
        this.patientForm.setValue(this.existingPatient);
    };
    PatientsComponent.prototype.handleNoClick = function () {
        this.displayPatientFoundModal = false;
        this.f.patientIdentifier.setValue(this.patientIdentifierSearch.value);
    };
    PatientsComponent.prototype.handleCancelClick = function () {
        if (confirm('Are you sure? Any unsaved changes will be lost.')) {
            this.patientForm.reset();
            this.displayPatientForm = false;
            this.goBack();
        }
    };
    /*********************************************************************
                              Service Calls
    **********************************************************************/
    PatientsComponent.prototype.goBack = function () {
        this.location.back();
    };
    PatientsComponent.prototype.searchPatientIdentifier = function () {
        var _this = this;
        var id = this.patientIdentifierSearch.value;
        this.ds.getPatientByPatientIdentifier(id).subscribe(function (p) {
            console.log("Get patient request returned:", p);
            if (p.patientIdentifier) {
                // TODO: Refactor
                _this.existingPatientId = p.id;
                _this.existingPatient = {
                    patientIdentifier: p.patientIdentifier,
                    firstName: p.firstName,
                    lastName: p.lastName,
                    phone: p.phone,
                    isMinor: p.isMinor,
                    preferredLanguage: p.preferredLanguage,
                    preferredContactMethod: p.preferredContactMethod,
                };
                _this.displayPatientFoundModal = true;
            }
            else {
                _this.displayPatientForm = true;
                _this.f.patientIdentifier.setValue(_this.patientIdentifierSearch.value);
            }
        }, function (err) {
            console.log("404 - No existing patient was found");
            _this.displayPatientForm = true;
            _this.f.patientIdentifier.setValue(_this.patientIdentifierSearch.value);
        });
    };
    PatientsComponent.prototype.saveNewPatient = function () {
        var _this = this;
        this.ds.addPatient(this.patientForm.value).subscribe(function (p) {
            console.log("Saved patient:", p);
            _this.router.navigate(['/appointment', { patientIdentifier: p.patientIdentifier, patientId: p.id }]);
        });
    };
    PatientsComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-patients',
            template: __webpack_require__(/*! ./patients.component.html */ "./src/app/patients/patients.component.html"),
            styles: [__webpack_require__(/*! ./patients.component.css */ "./src/app/patients/patients.component.css")]
        }),
        __metadata("design:paramtypes", [_api_api_dispatcher_service__WEBPACK_IMPORTED_MODULE_2__["DispatcherService"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_1__["FormBuilder"],
            _angular_common__WEBPACK_IMPORTED_MODULE_4__["Location"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]])
    ], PatientsComponent);
    return PatientsComponent;
}());



/***/ }),

/***/ "./src/app/rides/rides.component.css":
/*!*******************************************!*\
  !*** ./src/app/rides/rides.component.css ***!
  \*******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "p {\n  margin: 5px;\n}\n\n.day {\n  font-size: 1.5rem;\n  font-weight: bold;\n}\n\n.fixed-footer {\n  display: flex;\n  align-items: center;\n  justify-content: center;\n  width: 100%;\n  position: fixed;\n  left: 0;\n  bottom: 0;\n  z-index: 20;\n  background-color: #9575cd;\n  padding: 5px;\n}\n\n.open {\n  /* background-color: #ef5350; */\n  /* color: white; */\n  background-color: #ffebee;\n}\n\n.pending {\n  /* background-color: #ffca28; */\n  /* color: #333; */\n  background-color: #fff8e1;\n}\n\n.approved {\n  /* background-color: #66bb6a; */\n  /* color: white; */\n  background-color: #e0f2f1;\n}\n\n.drive-container {\n  display: flex;\n}\n\n.appointment-card {\n  text-align: center;\n}\n\n.drive-card {\n  width: 100%;\n  display: flex;\n  flex-direction: column;\n  padding: 5px 0px 8px 5px;\n}\n\n.drive-card-header {\n  display: flex;\n}\n\n.bolder {\n  font-weight: 600;\n}\n\n.bold {\n  font-weight: 500;\n}\n\n/* .date-container {\n  background-color: pink;\n  border: 1px solid black;\n  display: flex;\n  flex-direction: column;\n  z-index: 20;\n  position: fixed;\n  width: 50px;\n  align-items: center;\n} */\n"

/***/ }),

/***/ "./src/app/rides/rides.component.html":
/*!********************************************!*\
  !*** ./src/app/rides/rides.component.html ***!
  \********************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<ng-container *ngIf=\"rides && clinics\">\n  <!--********************************************************************\n                            Ride Display Toggles\n  **********************************************************************-->\n  <div class=\"fixed-footer\">\n    <!-- <mat-button-toggle-group name=\"fontStyle\" aria-label=\"Font Style\" multiple>\n      <mat-button-toggle value=\"open\" [checked]=\"displayRides[0]\" (change)=\"displayRides[0] = $event.source.checked\">Open</mat-button-toggle>\n      <mat-button-toggle value=\"pending\" [checked]=\"displayRides[1]\" (change)=\"displayRides[1] = $event.source.checked\">Pending</mat-button-toggle>\n      <mat-button-toggle value=\"approved\" [checked]=\"displayRides[2]\" (change)=\"displayRides[2] = $event.source.checked\">Approved</mat-button-toggle>\n    </mat-button-toggle-group> -->\n    <!-- TODO: Switch button toggles to chip-list? -->\n    <!-- <mat-chip-list>\n      <mat-chip>One fish</mat-chip>\n      <mat-chip>Two fish</mat-chip>\n      <mat-chip color=\"primary\" selected>Primary fish</mat-chip>\n      <mat-chip color=\"accent\" selected>Accent fish</mat-chip>\n    </mat-chip-list> -->\n    <!-- TODO: Insert date range switcher -->\n    &lt; {{ this.startDate | date:'MMM d' }} &mdash; {{ this.endDate | date:'MMM d' }} &gt;\n  </div>\n  <!--********************************************************************\n                                Ride Cards\n  **********************************************************************-->\n  <div *ngFor=\"let r of rides\">\n    <ng-container *ngIf=\"displayRides[r.driveTo?.status] || displayRides[r.driveFrom?.status]\">\n      <div class=\"date-container\">\n        <!-- TODO: Display these on left-hand menu -->\n        {{ r.appointment?.appointmentDate | date:'EEE' }}\n        <span class=\"day\">{{ r.appointment?.appointmentDate | date:'d' }}</span>\n        {{ r.appointment?.appointmentDate | date:'MMM' }}\n      </div>\n      <mat-card class=\"appointment-card\">\n        <span class=\"bolder\">{{ apptTypes[r.appointment?.appointmentTypeId] }} Appointment</span> <br>\n        <span class=\"bold\">{{ r.appointment?.appointmentDate | date:'shortTime'}} at {{ clinics[r.appointment?.clinicId].name }}</span>\n      </mat-card>\n      <div class=\"drive-container\">\n        <mat-card *ngIf=\"displayRides[r.driveTo?.status]\"\n                  class=\"drive-card\"\n                  [ngClass]=\"{ 'open': r.driveTo.status === 0,\n                               'pending': r.driveTo.status === 1,\n                               'approved': r.driveTo.status === 2 }\">\n          <div class=\"drive-card-header\">\n            <mat-icon>{{ getStatusIcon(r.driveTo.status) }}</mat-icon>\n            <span class=\"fill-remaining-space\"></span>\n            <mat-icon class=\"menu-icon\">more_vert</mat-icon>\n          </div>\n          <p class=\"bold\">Drive To Clinic</p>\n          <!-- TODO: Conditional display based on user role -->\n          <!-- <p>Pick up at {{ r.appointment?.pickupLocationVague }}.</p> -->\n          <p>Pick up at {{ r.driveTo.startAddress }}, {{ r.driveTo.startCity }}, {{ r.driveTo.startState }} {{ r.driveTo.startPostalCode }}.</p>\n        </mat-card>\n        <mat-card *ngIf=\"displayRides[r.driveFrom?.status]\"\n                  class=\"drive-card\"\n                  [ngClass]=\"{ 'open': r.driveFrom.status === 0,\n                               'pending': r.driveFrom.status === 1,\n                               'approved': r.driveFrom.status === 2 }\">\n          <div class=\"drive-card-header\">\n            <mat-icon>{{ getStatusIcon(r.driveFrom.status) }}</mat-icon>\n            <span class=\"fill-remaining-space\"></span>\n            <mat-icon class=\"menu-icon\">more_vert</mat-icon>\n          </div>\n          <p class=\"bold\">Drive From Clinic</p>\n          <!-- TODO: Conditional display based on user role -->\n          <!-- <p>Drop off at {{ r.appointment?.dropoffLocationVague }}.</p> -->\n          <p>Drop off at {{ r.driveFrom.endAddress }}, {{ r.driveFrom.endCity }}, {{ r.driveFrom.endState }} {{ r.driveFrom.endPostalCode }}.</p>\n        </mat-card>\n      </div>\n    </ng-container>\n  </div>\n</ng-container>\n\n<!--********************************************************************\n                          Ride Detail Modal\n**********************************************************************-->\n<!-- <span class=\"status-text\"><mat-icon>{{ getStatusIcon(r.driveFrom.status) }}</mat-icon> {{ getStatusText(r.driveFrom.status) }}</span> -->\n\n<pre *ngFor=\"let r of rides\">\n  {{ r | json }}\n</pre>\n"

/***/ }),

/***/ "./src/app/rides/rides.component.ts":
/*!******************************************!*\
  !*** ./src/app/rides/rides.component.ts ***!
  \******************************************/
/*! exports provided: RidesComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RidesComponent", function() { return RidesComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _api_api_default_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../api/api/default.service */ "./src/app/api/api/default.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var RidesComponent = /** @class */ (function () {
    /*********************************************************************
                        Constructor, Lifecycle Hooks
    **********************************************************************/
    function RidesComponent(ds) {
        this.ds = ds;
        this.startDate = "2017-11-01";
        this.endDate = "2018-11-28";
        // TODO: should be fetched from API or set as app-level constant
        this.apptTypes = { 4: 'Ultrasound', 3: 'Surgical' };
        // Display flags for rides. 0=open, 1=pending, 2=approved
        this.displayRides = [true, true, true];
    }
    RidesComponent.prototype.ngOnInit = function () {
        this.setDateRange();
        this.getClinics();
        this.getRides();
    };
    /*********************************************************************
                              Service Calls
    **********************************************************************/
    RidesComponent.prototype.getRides = function () {
        var _this = this;
        // TODO: Make start/end dates dynamic
        this.ds.getAllAppointments(this.startDate, this.endDate).subscribe(function (appts) {
            console.log("Appts are:", appts);
            // Sort by date asc
            _this.rides = appts.sort(function (a, b) { return new Date(a.appointment.appointmentDate).valueOf() - new Date(b.appointment.appointmentDate).valueOf(); });
        });
    };
    RidesComponent.prototype.getClinics = function () {
        var _this = this;
        this.ds.getClinics().subscribe(function (c) {
            console.table(c);
            _this.clinics = c.reduce(function (map, obj) { return (map[obj.id] = obj, map); }, {});
        });
    };
    /*********************************************************************
                                  Utilities
    **********************************************************************/
    RidesComponent.prototype.setDateRange = function () {
        var currentDate = new Date();
        // First day of current week is day of month - day of week
        var firstDay = currentDate.getDate() - currentDate.getDay();
        // Last day of current week is first day + 6
        var lastDay = firstDay + 6;
        this.startDate = new Date(currentDate.setDate(firstDay)).toUTCString();
        this.endDate = new Date(currentDate.setDate(lastDay)).toUTCString();
        console.log("Current Date:", currentDate);
        console.log("First Date:", this.startDate);
        console.log("Last Date:", this.endDate);
    };
    RidesComponent.prototype.getStatusIcon = function (status) {
        switch (status) {
            case 0: return "panorama_fish_eye";
            case 1: return "timelapse";
            case 2: return "check_circle";
            default: return "";
        }
    };
    RidesComponent.prototype.getStatusText = function (status) {
        switch (status) {
            case 0: return "Open - Apply Now";
            case 1: return "Pending Approval";
            case 2: return "Approved";
            default: return "";
        }
    };
    RidesComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-rides',
            template: __webpack_require__(/*! ./rides.component.html */ "./src/app/rides/rides.component.html"),
            styles: [__webpack_require__(/*! ./rides.component.css */ "./src/app/rides/rides.component.css")]
        }),
        __metadata("design:paramtypes", [_api_api_default_service__WEBPACK_IMPORTED_MODULE_1__["DefaultService"]])
    ], RidesComponent);
    return RidesComponent;
}());



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
var environment = {
    production: false,
    apiUrl: 'https://casnapptest.dmwilson.info/api'
};
/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.error(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! /Users/aspen/web-dev/casn-app-backend/casn-frontend/casn-app/src/main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map