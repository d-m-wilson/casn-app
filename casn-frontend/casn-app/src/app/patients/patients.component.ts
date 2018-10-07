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
  patient: any;
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
  languages: string[] = ['English', 'Spanish', 'Other'];
  contactMethods: string[] = ['Call', 'Text', 'Email'];

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

  onSubmit(): void {}

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
  handleYesClick() {
    this.displayPatientFoundModal = false;
    this.displayPatientForm = true;
    this.patientForm.setValue(this.patient);
  }

  handleNoClick() {
    this.displayPatientFoundModal = false;
    this.patient = {}
  }

  handleCancelClick() {
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
      console.log("The patient is:", p);
      this.patient = {
        patientIdentifier: p.patientIdentifier,
        firstName: p.firstName,
        lastName: p.lastName,
        phone: p.phone,
        isMinor: p.isMinor,
        preferredLanguage: p.preferredLanguage,
        preferredContactMethod: p.preferredContactMethod,
      };
      this.displayPatientFoundModal = true;
    })
  }

}
