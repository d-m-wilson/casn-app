import { Injectable } from '@angular/core';
import { FormControl, ValidationErrors, AsyncValidator } from '@angular/forms';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Injectable({ providedIn: 'root'})
export class CallerIdentifierValidator implements AsyncValidator {
  constructor ( private ds: DispatcherApiService,
                private route: ActivatedRoute ) {}

  validate(callerIdentifier: FormControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> {
    // This validator only matters if the user is editing an existing caller.
    const editing = !!this.route.snapshot.paramMap.get('callerIdentifier');
    if(!editing) return of(null);

    // If the user hasn't changed the callerIdentifier, we don't need
    // to perform a network request to check if it's taken.
    const callerIdentifierToEdit = this.route.snapshot.paramMap.get('callerIdentifier');
    if(callerIdentifierToEdit == callerIdentifier.value ) return of(null);

    // If user is editing a caller, throw a form validation error if they
    // change the callerIdentifier to one that is already in use.
    return this.ds.getCallerByCallerIdentifier(callerIdentifier.value).pipe(
      tap(res => console.log("callerIdentifier validator got response:", res)),
      map(res => res.callerIdentifier ? { callerIdentifierExists: true } : null),
      catchError(() => of(null))
    );
  }
}
