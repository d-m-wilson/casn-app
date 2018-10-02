import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { DispatcherService } from '../api/api/dispatcher.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  languages = ['English', 'Spanish', 'Other'];
  contactMethods = ['Call', 'Text', 'Email'];
  // TODO: This may become a simple text input + search rather than autocomplete
  patientOptions: string[] = ['1234', '5225', '8274'];

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private dispatcherService: DispatcherService ) { }

  ngOnInit() {
    this.getAppts();
    this.filteredPatientOptions = this.patientSearchControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  /*********************************************************************
                      Form Methods
  **********************************************************************/
  // PatientId - last 4 digits of phone number, add 5th digit on duplicate
  patientForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    phoneNumber: new FormControl(''),
    preferredLanguage: new FormControl(''),
    preferredContactMethod: new FormControl(''),
  });

  patientSearchControl = new FormControl();
  filteredPatientOptions: Observable<string[]>;

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.patientOptions.filter(option => option.toLowerCase().includes(filterValue));
  }

  /*********************************************************************
                      Service Calls
  **********************************************************************/
  getAppts() {
    this.dispatcherService.getAllAppointmentsForDispatcher().subscribe(a => {
      console.log("Appointments are:", a);
    })
  }

}
