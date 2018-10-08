import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { DispatcherService } from '../api/api/dispatcher.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  existingPatient: any = {};
  /* Display flags for patient lookup feature */
  displayPatientFoundModal: boolean = false;
  displayPatientForm: boolean = false;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DispatcherService,
               private fb: FormBuilder,
               private location: Location ) { }

  ngOnInit() { }

  /*********************************************************************
                                Form
  **********************************************************************/
  languages: string[] = ['English', 'Spanish', 'French', 'Other'];
  // NOTE: Ask about number mapping here.
  contactMethods: any[] = [ {value: 1, displayValue: 'Call'},
                            {value: 2, displayValue: 'Text'},
                            {value: 3, displayValue:'Email'} ];

  patientForm = this.fb.group({
    patientIdentifier: ['', [Validators.required, Validators.minLength(4),
                        Validators.maxLength(6)]],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    phone: ['', Validators.required],
    isMinor: [false, Validators.required],
    preferredLanguage: ['', Validators.required],
    preferredContactMethod: ['', Validators.required],
  })

  // convenience getter for easy access to form fields
  get f() { return this.patientForm.controls; }

  onSubmit(): void {
    if(!this.patientForm.valid) { return; }
    // Check if patient is new or existing to make appropriate REST call.
    const isNewPatient = Object.keys(this.existingPatient).length === 0;
    if(isNewPatient) {
      this.saveNewPatient();
    } else {
      // TODO: There should be an update patient endpoint
      this.saveNewPatient();
      // this.updatePatient();
    }
  }

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
  handleYesClick(): void {
    this.displayPatientFoundModal = false;
    this.displayPatientForm = true;
    this.patientForm.setValue(this.existingPatient);
  }

  handleNoClick(): void {
    this.displayPatientFoundModal = false;
    this.existingPatient = {}
  }

  handleCancelClick(): void {
    if(confirm('Are you sure? Any unsaved changes will be lost.')) {
      this.patientForm.reset();
      this.displayPatientForm = false;
      this.goBack();
    }
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  goBack(): void {
    this.location.back();
  }

  searchPatientIdentifier(): void {
    const id = this.patientForm.value.patientIdentifier;
    this.ds.getPatientByPatientIdentifier(id).subscribe(p => {
      console.log("Get patient request returned:", p);
      if(p.patientIdentifier) {
        this.existingPatient = {
          patientIdentifier: p.patientIdentifier,
          firstName: p.firstName,
          lastName: p.lastName,
          phone: p.phone,
          isMinor: p.isMinor,
          preferredLanguage: p.preferredLanguage,
          preferredContactMethod: p.preferredContactMethod,
        };
        this.displayPatientFoundModal = true;
      } else {
        this.displayPatientForm = true;
      }
    })
  }

  saveNewPatient(): void {
    this.ds.addPatient().subscribe(p => {
      console.log("Save patient response is", p);
      alert('Success! Your patient has been saved.');
    });
  }

  // updatePatient(): void {}

}
