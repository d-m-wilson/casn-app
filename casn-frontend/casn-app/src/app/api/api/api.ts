export * from './default.service';
import { DefaultService } from './default.service';
export * from './default.serviceInterface'
export * from './dispatcher.service';
import { DispatcherService } from './dispatcher.service';
export * from './dispatcher.serviceInterface'
export * from './driver.service';
import { DriverService } from './driver.service';
export * from './driver.serviceInterface'
export const APIS = [DefaultService, DispatcherService, DriverService];
