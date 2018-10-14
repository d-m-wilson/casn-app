import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
// import { map, startWith, catchError } from 'rxjs/operators';
import { DispatcherService } from '../api/api/dispatcher.service';
import { DefaultService } from '../api/api/default.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DispatcherService,
               private defaultService: DefaultService,
               private fb: FormBuilder,
               private location: Location,
               private router: Router ) { }

  ngOnInit() {
    // TODO: Get patientIdentifier
    this.getClinics();
  }

  /*********************************************************************
                                Form
  **********************************************************************/
  clinics: any[] = [];

  apptForm = this.fb.group({
    // TODO: Figure out what is appointmentTypeId
    // appointmentTypeId: [''],
    patientIdentifier: ['', [Validators.required, Validators.minLength(4),
                        Validators.maxLength(6)]],
    dispatcherId: [9876, Validators.required],
    clinicId: ['', Validators.required],
    appointmentDate: ['', Validators.required],
    appointmentTime: ['', Validators.required],
    pickupLocationExact: ['', Validators.required],
    // TODO: Auto-populate this w/ clinic address
    dropoffLocationExact: ['', Validators.required],
    pickupLocationVague: ['', Validators.required],
    dropoffLocationVague: ['', Validators.required],
  })

  // convenience getter for easy access to form fields
  get f() { return this.apptForm.controls; }

  onSubmit(): void {
    if(!this.apptForm.valid) { return; }
    console.log("--Submitting Appt Form...", this.apptForm);
    this.saveNewAppt();
  }

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
  handleCancelClick(): void {
    if(confirm('Are you sure? Any unsaved changes will be lost.')) {
      this.apptForm.reset();
      this.goBack();
    }
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  goBack(): void {
    this.location.back();
  }

  getClinics(): void {
    this.defaultService.getClinics().subscribe(data => {
      console.log("Clinics are", data);
      this.clinics = data;
    })
  }

  saveNewAppt(): void {
    // this.ds.addPatient(this.apptForm.value).subscribe(data => {
    //   console.log("Save appt response is", data);
    //   alert('Success! Your appointment has been saved.');
    //   this.router.navigate(['']);
    // });
  }

}
