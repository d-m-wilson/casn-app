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
    const editing = !!this.route.snapshot.paramMap.get('callerIdentifier');
    console.log("User is editing?", editing);
    if(!editing) return of(null);

    // If user is editing a caller, throw a form validation error if they
    // change the callerIdentifier to one that is already in use.
    return this.ds.getCallerByCallerIdentifier(callerIdentifier.value).pipe(
      tap(res => console.log("callerIdentifier validator got response:", res)),
      map(res => res.callerIdentifier ? { callerIdentifierExists: true } : null),
      catchError(() => of(null))
    );
  }
}
