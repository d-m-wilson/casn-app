import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

/**
  TODO: Edit Appointment Feature
  - Receive appointment data OR receive apptID and GET data from API.
  - Set form values from appt data
  - Ensure UX of form works correctly
**/

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.scss']
})
export class AppointmentFormComponent implements OnInit {
  /*
  NOTE:
  "callerId" is the actual database ID for communication w/ API
  "callerIdentifier" is a 4-digit ID used by CASN volunteers to refer
  to the caller without using the caller's name.
  */
  callerId: number;
  callerIdentifier: string;
  appointmentTypes: any[];
  serviceProviders: any;
  clinicServiceProviders: any;
  courthouseServiceProviders: any;
  appointmentDTO: any;
  callerNeedsPickup: boolean;
  callerNeedsDropoff: boolean;
  dropoffSameAsPickup: boolean;

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
    /* NOTE: Workaround due to angular material stepper component issue
    https://github.com/angular/components/issues/8881 */
    const stepElement = document.getElementsByClassName('mat-drawer-content')[0];
    stepElement.scrollTop = 0;
    this.getAppointmentTypes();
    this.getCaller();
    this.getServiceProviders();
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
    this.formAppt.callerId.setValue(this.callerId);
    this.formAppt.callerIdentifier.setValue(this.callerIdentifier);
  }

  getServiceProviders(): void {
    this.defaultService.getServiceProviders().subscribe(
      data => {
        console.log("Service Providers:", data)
        this.serviceProviders = data;
        this.courthouseServiceProviders = data.filter(s => s.serviceProviderTypeId === 2);
        this.clinicServiceProviders = data.filter(s => s.serviceProviderTypeId === 1);
      },
      err => {
        console.error("--Error fetching serviceProviders...", err);
        alert('An error occurred while fetching serviceProviders. Please refresh & try again.')
      }
    );
  }

  saveNewAppt(): void {
    console.log("Saving your appt!", this.appointmentDTO);
    this.ds.addAppointment(this.appointmentDTO).subscribe(
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
  /*********************************************************************
                                Forms
  **********************************************************************/
  appointmentForm = this.fb.group({
    appointmentTypeId: [3, Validators.required],
    callerId: [0],
    callerIdentifier: ['', Validators.required],
    // TODO: Get dispatcherId from localstorage
    // Need to talk to David about this -- handle on backend?
    dispatcherId: [5],
    serviceProviderId: [1, Validators.required],
    appointmentDate: ['', Validators.required],
  });

  driveToForm = this.fb.group({
    pickupAddress: ['', Validators.required],
    pickupCity: ['', Validators.required],
    pickupState: ['TX', Validators.required],
    pickupPostalCode: ['', Validators.required],
    pickupLocationVague: ['', Validators.required],
  });

  driveFromForm = this.fb.group({
    dropoffAddress: ['', Validators.required],
    dropoffCity: ['', Validators.required],
    dropoffState: ['TX', Validators.required],
    dropoffPostalCode: ['', Validators.required],
    dropoffLocationVague: ['', Validators.required],
  });

  onSubmit(): void {
    const apptInvalid = !this.appointmentForm.valid;
    const pickupInvalid = this.callerNeedsPickup && !this.driveToForm.valid;
    const dropoffInvalid = this.callerNeedsDropoff && !this.dropoffSameAsPickup && !this.driveFromForm.valid;
    const formInvalid = apptInvalid || pickupInvalid || dropoffInvalid;
    if(formInvalid) return;
    this.constructAppointmentDTO();
  }

  /*********************************************************************
                            Getters/Setters
  **********************************************************************/
  // convenience getters for easy access to form fields
  get formAppt() { return this.appointmentForm.controls; }
  get formPickup() { return this.driveToForm.controls; }
  get formDropoff() { return this.driveFromForm.controls; }

  get apptType(): string {
    return (this.appointmentTypes.find(a => a.value === this.formAppt.appointmentTypeId.value)).displayValue;
  }

  get apptServiceProvider(): string {
    return (this.serviceProviders.find(s => s.id == this.formAppt.serviceProviderId.value)).name;
  }


  /*********************************************************************
                      DTO Construction for API Calls
  **********************************************************************/
  // TODO: Confirm what I need to send to backend for one-way drives.
  // TODO: Refactor this, possibly out into a service call.
  constructAppointmentDTO(): void {
    this.appointmentDTO = {};
    this.appointmentDTO.appointment = {
      appointmentTypeId: this.formAppt.appointmentTypeId.value,
      callerId: this.formAppt.callerId.value,
      dispatcherId: this.formAppt.dispatcherId.value,
      serviceProviderId: this.formAppt.serviceProviderId.value,
      appointmentDate: this.formAppt.appointmentDate.value.toISOString(),
      pickupLocationVague: this.formPickup.pickupLocationVague.value,
      dropoffLocationVague: this.dropoffSameAsPickup ? this.formPickup.pickupLocationVague.value : this.formDropoff.dropoffLocationVague.value
    }
    if(this.callerNeedsPickup) {
      this.appointmentDTO.driveTo = {
        direction: 1,
        startAddress: this.formPickup.pickupAddress.value,
        startCity: this.formPickup.pickupCity.value,
        startState: this.formPickup.pickupState.value,
        startPostalCode: this.formPickup.pickupPostalCode.value,
        endAddress: "",
        endCity: "",
        endState: "",
        endPostalCode: "",
      }
    } else {
      this.appointmentDTO.driveTo = null;
      this.appointmentDTO.appointment.pickupLocationVague = null;
    }
    if(this.callerNeedsDropoff) {
      if(this.dropoffSameAsPickup) {
        this.appointmentDTO.driveFrom = {
          direction: 2,
          endAddress: this.formPickup.pickupAddress.value,
          endCity: this.formPickup.pickupCity.value,
          endState: this.formPickup.pickupState.value,
          endPostalCode: this.formPickup.pickupPostalCode.value,
          startAddress: "",
          startCity: "",
          startState: "",
          startPostalCode: "",
        }
      } else {
        this.appointmentDTO.driveFrom = {
          direction: 2,
          endAddress: this.formDropoff.dropoffAddress.value,
          endCity: this.formDropoff.dropoffCity.value,
          endState: this.formDropoff.dropoffState.value,
          endPostalCode: this.formDropoff.dropoffPostalCode.value,
          startAddress: "",
          startCity: "",
          startState: "",
          startPostalCode: "",
        }
      }
    } else {
      this.appointmentDTO.driveFrom = null;
      this.appointmentDTO.appointment.dropoffLocationVague = null;
    }
    console.log("Constructed Appointment DTO:", this.appointmentDTO);
    this.saveNewAppt();
  }

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
  toggleDropoffSameAsPickup(sameAsPickup: boolean): void {
    this.dropoffSameAsPickup = sameAsPickup;
    // Scroll to top of current step when form fields hide/show
    const stepElement = document.getElementsByClassName('mat-drawer-content')[0];
    stepElement.scrollTop = 0;
  }

}
