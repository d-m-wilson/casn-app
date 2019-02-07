import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
// import { map, catchError } from 'rxjs/operators';
import { DispatcherService } from '../api/api/dispatcher.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  existingCaller: any = {};
  existingCallerId: Number;
  /* Display flags for patient lookup feature */
  displayCallerFoundModal: boolean = false;
  displayCallerForm: boolean = false;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DispatcherService,
               private fb: FormBuilder,
               private location: Location,
               private router: Router ) { }

  ngOnInit() { }

  /*********************************************************************
                                Form
  **********************************************************************/
  // TODO: Remove French? Was used bc dummy data includes it.
  languages: string[] = ['English', 'Spanish', 'French', 'Other'];
  contactMethods: any[] = [ {value: 1, displayValue: 'Text'},
                            {value: 2, displayValue: 'Call'},
                            {value: 3, displayValue: 'Email'} ];

  patientIdentifierSearch = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(6)])

  callerForm = this.fb.group({
    patientIdentifier: ['', [Validators.required, Validators.minLength(4),
                        Validators.maxLength(6)]],
    firstName: ['', Validators.required],
    lastName: [''],
    phone: ['', Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")], // TODO: Require either phone OR email
    // email: [''],
    isMinor: [false, Validators.required],
    preferredLanguage: ['English', Validators.required],
    preferredContactMethod: [1, Validators.required],
  })

  // convenience getter for easy access to form fields
  get f() { return this.callerForm.controls; }

  onSubmit(): void {
    if(!this.callerForm.valid) { return; }
    // Check if patient is new or existing to make appropriate REST call.
    const isNewPatient = Object.keys(this.existingCaller).length === 0;
    if(isNewPatient) {
      this.saveNewPatient();
    } else {
      // TODO: There should be an update patient endpoint
      this.router.navigate(['/appointment', { patientIdentifier: this.f.patientIdentifier.value, patientId: this.existingCallerId }]);
    }
  }

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
  handleYesClick(): void {
    this.displayCallerFoundModal = false;
    this.displayCallerForm = true;
    this.callerForm.setValue(this.existingCaller);
  }

  handleNoClick(): void {
    this.displayCallerFoundModal = false;
    this.f.patientIdentifier.setValue(this.patientIdentifierSearch.value);
  }

  handleCancelClick(): void {
    if(confirm('Are you sure? Any unsaved changes will be lost.')) {
      this.callerForm.reset();
      this.displayCallerForm = false;
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
    const id = this.patientIdentifierSearch.value;
    this.ds.getPatientByPatientIdentifier(id).subscribe(
      p => {
        if(p.patientIdentifier) {
          // TODO: Refactor
          this.existingCallerId = p.id;
          this.existingCaller = {
            patientIdentifier: p.patientIdentifier,
            firstName: p.firstName,
            lastName: p.lastName,
            phone: p.phone,
            isMinor: p.isMinor,
            preferredLanguage: p.preferredLanguage,
            preferredContactMethod: p.preferredContactMethod,
          };
          this.displayCallerFoundModal = true;
        } else {
          this.displayCallerForm = true;
          this.f.patientIdentifier.setValue(this.patientIdentifierSearch.value);

        }
      },
      err => {
        console.log("404 - No existing patient was found");
        this.displayCallerForm = true;
        this.f.patientIdentifier.setValue(this.patientIdentifierSearch.value);

      }
    );
  }

  saveNewPatient(): void {
    this.ds.addPatient(this.callerForm.value).subscribe(p => {
      this.router.navigate(['/appointment', { patientIdentifier: p.patientIdentifier, patientId: p.id }]);
    });
  }

}
