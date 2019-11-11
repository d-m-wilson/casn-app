import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppointmentDataService {

  private messageSource = new BehaviorSubject({});
  currentMessage = this.messageSource.asObservable();

  constructor() { }

  changeMessage(message: string) {
    console.log("Shared appt data service was updated:", message);
    this.messageSource.next(message)
  }

}
