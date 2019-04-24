import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {
  callerIdentifier: string;
  callerId: number;
  apptDTO: any;
  appointmentTypes: any[];
  clinics: any[] = [];
  // Hide dropoff inputs when user checks 'same as pickup location'
  showDropoffLocationInputs: boolean = true;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DispatcherApiService,
               private defaultService: DefaultApiService,
               private fb: FormBuilder,
               private route: ActivatedRoute,
               private location: Location,
               private router: Router ) { }

  ngOnInit() {
    this.getAppointmentTypes();
    this.getCaller();
    this.getClinics();
  }

  /*********************************************************************
                                Form
  **********************************************************************/
  apptForm = this.fb.group({
    appointmentTypeId: [3, Validators.required],
    callerId: [0],
    callerIdentifier: ['', Validators.required],
    // TODO: Get dispatcherId from localstorage
    // Need to talk to David about this -- handle on backend?
    dispatcherId: [5],
    clinicId: ['', Validators.required],
    appointmentDate: ['', Validators.required],
    pickupAddress: ['', Validators.required],
    pickupCity: ['', Validators.required],
    pickupState: ['TX', Validators.required],
    pickupZipCode: ['', Validators.required],
    dropoffAddress: ['', Validators.required],
    dropoffCity: ['', Validators.required],
    dropoffState: ['TX', Validators.required],
    dropoffZipCode: ['', Validators.required],
    pickupLocationVague: ['', Validators.required],
    dropoffLocationVague: ['', Validators.required],
  });

  // convenience getter for easy access to form fields
  get f() { return this.apptForm.controls; }

  onSubmit(): void {
    /* If pickup has been edited since "Same as Pickup" checkbox was first
    clicked, update dropoff accordingly. */
    if(!this.showDropoffLocationInputs) this.setDropoffLocation(true);
    if (!this.apptForm.valid) return;
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
      callerId: this.f.callerId.value,
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

  getAppointmentTypes(): void {
    this.defaultService.getAppointmentTypes().subscribe(a => {
      this.appointmentTypes = a.map(i => {
        return { value: i.id, displayValue: i.title };
      })
    })
  }

  getCaller(): void {
    this.callerId = parseInt(this.route.snapshot.paramMap.get('callerId'));
    this.callerIdentifier = this.route.snapshot.paramMap.get('callerIdentifier');
    this.f.callerId.setValue(this.callerId);
    this.f.callerIdentifier.setValue(this.callerIdentifier);
  }

  getClinics(): void {
    this.defaultService.getClinics().subscribe(
      data => {
        this.clinics = data;
      },
      err => {
        console.error("--Error fetching clinics...", err);
        alert('An error occurred while fetching clinics. Please refresh & try again.')
      }
    );
  }

  saveNewAppt(): void {
    this.ds.addAppointment(this.apptDTO).subscribe(
      data => {
        console.log("Save appt response is", data);
        alert('Success! Your appointment has been saved.');
        this.router.navigate(['']);
      },
      err => {
        console.error("--Error saving appt data...", err);
        alert('An error occurred, and your appointment was not saved.');
      }
    );
  }

}
