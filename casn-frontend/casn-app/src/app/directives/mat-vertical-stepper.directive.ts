/* NOTE: This directive is included to overwrite some undesirable
behavior of the mat-stepper component as explained in:
https://github.com/angular/components/issues/8881
*/

import { Directive, HostListener } from '@angular/core';
import { MatStepper } from '@angular/material/stepper';
import { StepperSelectionEvent } from '@angular/cdk/stepper';

@Directive({
  selector: 'mat-vertical-stepper'
})
export class MatVerticalStepperScrollerDirective {

  constructor( private stepper: MatStepper ) {}

  @HostListener('selectionChange', ['$event'])
  selectionChanged(selection: StepperSelectionEvent) {
    const stepId = this.stepper._getStepLabelId(selection.selectedIndex);
    const stepElement = document.getElementById(stepId);
    if (stepElement) {
      setTimeout(() => {
        stepElement.scrollIntoView({ block: 'start', inline: 'nearest', behavior: 'smooth' });
      }, 250);
    }
  }
}
