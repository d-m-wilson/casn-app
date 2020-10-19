import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DefaultApiService } from '../api/api/defaultApi.service';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';

@Component({
  selector: 'app-cancel-drive-modal',
  templateUrl: './cancel-drive-modal.component.html',
  styleUrls: ['./cancel-drive-modal.component.scss']
})
export class CancelDriveModalComponent implements OnInit {
  @Input() driveId: string;
  driveCancelReasons: any;
  cancelReason = new FormControl(1);
  @Output() closeCancelDriveModalClick = new EventEmitter<boolean>();
  @Output() closeCancelDriveModalAndUpdateClick = new EventEmitter<boolean>();

  constructor( private ds: DefaultApiService,
               private dispatcherService: DispatcherApiService ) { }

  ngOnInit() {
    this.getCancelReasons();
  }

  /*********************************************************************
                            Service Calls
  **********************************************************************/
  getCancelReasons(): void {
    this.ds.getDriveCancelReasons().subscribe(r => this.driveCancelReasons = r)
  }

  /*********************************************************************
                              Click Handlers
  **********************************************************************/
    handleCloseModalClick() {
      this.closeCancelDriveModalClick.emit(true);
    }

    submitDriveCancelRequest() {
      console.log("reason:", this.cancelReason.value)
      this.dispatcherService.cancelDrive(this.driveId, this.cancelReason.value).subscribe(
        res => {
          console.log("Success! Canceled drive.");
          this.closeCancelDriveModalAndUpdateClick.emit(true);
        },
        err => {
          // TODO: Handle error
          console.error("ERROR:", err);
        }
      );
    }


}
