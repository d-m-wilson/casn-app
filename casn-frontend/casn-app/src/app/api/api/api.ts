export * from './defaultApi.service';
import { DefaultApiService } from './defaultApi.service';
export * from './defaultApi.serviceInterface'
export * from './dispatcherApi.service';
import { DispatcherApiService } from './dispatcherApi.service';
export * from './dispatcherApi.serviceInterface'
export * from './driverApi.service';
import { DriverApiService } from './driverApi.service';
export * from './driverApi.serviceInterface'
export const APIS = [DefaultApiService, DispatcherApiService, DriverApiService];
