import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';
import { AppointmentDataService } from "../appointment-data.service";
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-callers',
  templateUrl: './callers.component.html',
  styleUrls: ['./callers.component.scss']
})
export class CallersComponent implements OnInit {
  existingCaller: any = {};
  existingCallerId: Number;
  editingCaller: boolean = false;
  /* Display flags for caller lookup feature */
  displayCallerFoundModal: boolean = false;
  displayCallerForm: boolean = false;

  /*********************************************************************
                      Constructor, Lifecycle Hooks
  **********************************************************************/
  constructor( private ds: DispatcherApiService,
               private sharedApptDataService: AppointmentDataService,
               private fb: FormBuilder,
               private location: Location,
               private route: ActivatedRoute,
               private router: Router ) { }

  ngOnInit() {
    // Set up filtering for preferredLanguage autocomplete
    this.filteredLanguages = this.f.preferredLanguage.valueChanges.pipe(
      startWith(''),
      map(value => this.filterLanguage(value))
    );

    // If caller already selected, search callerIdentifier
    const callerIdentifier = this.route.snapshot.paramMap.get('callerIdentifier');
    if(callerIdentifier) {
      this.editingCaller = true;
      this.callerIdentifierSearch.setValue(callerIdentifier);
      this.searchCallerIdentifier();
    }
  }

  /*********************************************************************
                                Form
  **********************************************************************/
  languages: string[] = ['English', 'Spanish', 'French', 'Vietnamese'];
  filteredLanguages: Observable<string[]>;
  contactMethods: any[] = [ {value: 1, displayValue: 'Text'},
                            {value: 2, displayValue: 'Call'} ];

  callerIdentifierSearch = new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(6)])

  callerForm = this.fb.group({
    callerIdentifier: ['', [Validators.required, Validators.minLength(4),
                        Validators.maxLength(6)]],
    firstName: ['', Validators.required],
    lastName: [''],
    phone: ['', Validators.pattern("^((\\+91-?)|0)?[0-9]{10}$")],
    isMinor: [false, Validators.required],
    preferredLanguage: ['English', Validators.required],
    preferredContactMethod: [1, Validators.required],
    note: ['', Validators.maxLength(500)]
  })

  // convenience getter for easy access to form fields
  get f() { return this.callerForm.controls; }

  onSubmit(): void {
    if(!this.callerForm.valid) { return; }
    // Check if caller is new or existing to make appropriate REST call.
    const isNewCaller = Object.keys(this.existingCaller).length === 0;
    if(isNewCaller) {
      this.saveNewCaller();
    } else {
      this.updateCaller();
    }
  }

  filterLanguage(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.languages.filter(l => l.toLowerCase().indexOf(filterValue) === 0);
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
    this.f.callerIdentifier.setValue(this.callerIdentifierSearch.value);
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

  searchCallerIdentifier(): void {
    const id = this.callerIdentifierSearch.value;
    this.ds.getCallerByCallerIdentifier(id).subscribe(
      p => {
        if(p.callerIdentifier) {
          // TODO: Refactor
          this.existingCallerId = p.id;
          this.existingCaller = {
            callerIdentifier: p.callerIdentifier,
            firstName: p.firstName,
            lastName: p.lastName,
            phone: p.phone,
            isMinor: p.isMinor,
            preferredLanguage: p.preferredLanguage,
            preferredContactMethod: p.preferredContactMethod,
            note: p.note,
          };
          this.displayCallerFoundModal = true;
          // If editing caller, assume user auto-confirms the callerIdentifier is correct
          if(this.editingCaller) this.handleYesClick();
        } else {
          this.displayCallerForm = true;
          this.f.callerIdentifier.setValue(this.callerIdentifierSearch.value);

        }
      },
      err => {
        console.log("404 - No existing caller was found");
        this.displayCallerForm = true;
        this.f.callerIdentifier.setValue(this.callerIdentifierSearch.value);
      }
    );
  }

  saveNewCaller(): void {
    this.ds.addCaller(this.callerForm.value).subscribe(p => {
      this.router.navigate(['/appointment', { callerIdentifier: p.callerIdentifier, callerId: p.id }]);
    });
  }

  updateCaller(): void {
    this.sharedApptDataService.changeMessage(this.callerForm.value);
    this.router.navigate(['/appointment', { callerIdentifier: this.f.callerIdentifier.value, callerId: this.existingCallerId }]);
  }

}
