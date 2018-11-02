import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
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
  patientIdentifier: string;
  patientId: any;
  apptDTO: any;
  // Hide dropoff inputs when user checks 'same as pickup location'
  showDropoffLocationInputs: boolean = true;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DispatcherService,
               private defaultService: DefaultService,
               private fb: FormBuilder,
               private route: ActivatedRoute,
               private location: Location,
               private router: Router ) { }

  ngOnInit() {
    this.getPatient();
    this.getClinics();
  }

  /*********************************************************************
                                Form
  **********************************************************************/
  clinics: any[] = [];
  // TODO: These should be fetched from API endpoint
  apptTypes: any[] = [ {value: 4, displayValue: 'Ultrasound'},
                       {value: 3, displayValue: 'Surgical'},
                       // {value: 1, displayValue: 'Lam To Complete'}
                     ];

  apptForm = this.fb.group({
    appointmentTypeId: [3, Validators.required],
    patientId: [5],
    patientIdentifier: ['', Validators.required],
    dispatcherId: [5],
    clinicId: ['', Validators.required],
    appointmentDate: ['', Validators.required],
    pickupAddress: ['', Validators.required],
    pickupCity: ['', Validators.required],
    pickupState: ['TX', Validators.required],
    pickupZipCode: ['', Validators.required],
    dropoffAddress: ['', Validators.required],
    dropoffCity: ['', Validators.required],
    dropoffState: ['', Validators.required],
    dropoffZipCode: ['', Validators.required],
    pickupLocationVague: ['', Validators.required],
    dropoffLocationVague: ['', Validators.required],
  });

  // convenience getter for easy access to form fields
  get f() { return this.apptForm.controls; }

  onSubmit(): void {
    if(!this.apptForm.valid) { return; }
    console.log("--Submitting Appt Form...", this.apptForm.value);
    this.constructApptDTO();
  }

  setDropoffLocation(sameAsPickup: boolean): void {
    if(sameAsPickup) {
      this.showDropoffLocationInputs = false;
      this.f.dropoffAddress.setValue(this.f.pickupAddress.value);
      this.f.dropoffCity.setValue(this.f.pickupCity.value);
      this.f.dropoffState.setValue(this.f.pickupState.value);
      this.f.dropoffZipCode.setValue(this.f.pickupZipCode.value);
      this.f.dropoffLocationVague.setValue(this.f.pickupLocationVague.value);
    } else {
      this.showDropoffLocationInputs = true;
      this.f.dropoffAddress.reset();
      this.f.dropoffCity.reset();
      this.f.dropoffState.reset()
      this.f.dropoffZipCode.reset();
      this.f.dropoffLocationVague.reset();
    }
  }

  // TODO: Refactor this eventually.
  constructApptDTO(): void {
    this.apptDTO = {};
    this.apptDTO.appointment = {
      appointmentTypeId: this.f.appointmentTypeId.value,
      patientId: this.f.patientId.value,
      dispatcherId: this.f.dispatcherId.value,
      clinicId: this.f.clinicId.value,
      appointmentDate: this.f.appointmentDate.value.toISOString(),
      pickupLocationVague: this.f.pickupLocationVague.value,
      dropoffLocationVague: this.f.dropoffLocationVague.value
    }
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
    }
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
    }
    console.log("Appt DTO is:", this.apptDTO);
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

  getPatient(): void {
    this.patientId = parseInt(this.route.snapshot.paramMap.get('patientId'));
    this.patientIdentifier = this.route.snapshot.paramMap.get('patientIdentifier');
    this.f.patientId.setValue(this.patientId);
    this.f.patientIdentifier.setValue(this.patientIdentifier);
  }

  getClinics(): void {
    this.defaultService.getClinics().subscribe(data => {
      this.clinics = data;
    })
  }

  saveNewAppt(): void {
    this.ds.addAppointment(this.apptDTO).subscribe(
    data => {
      console.log("Save appt response is", data);
      alert('Success! Your appointment has been saved.');
      this.router.navigate(['']);
    },
    err => {
      console.log("--Error saving appt data...", err);
      alert('An error occurred, and your appointment was not saved.');
    });
  }

}
