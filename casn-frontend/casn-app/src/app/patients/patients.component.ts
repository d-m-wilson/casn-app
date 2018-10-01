import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import {Observable} from 'rxjs';
import {map, startWith} from 'rxjs/operators';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  languages = ['english', 'spanish', 'vietnamese'];
  contactMethods = ['phone call', 'text', 'email'];

  // PatientId - last 4 digits of phone number, add 5th digit on duplicate
  patientForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    phoneNumber: new FormControl(''),
    preferredLanguage: new FormControl(''),
    preferredContactMethod: new FormControl(''),
  });

  patientSearchControl = new FormControl();
  patientOptions: string[] = ['One', 'Two', 'Three'];
  filteredPatientOptions: Observable<string[]>;

  constructor() { }

  ngOnInit() {
    this.filteredPatientOptions = this.patientSearchControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.patientOptions.filter(option => option.toLowerCase().includes(filterValue));
  }

}
