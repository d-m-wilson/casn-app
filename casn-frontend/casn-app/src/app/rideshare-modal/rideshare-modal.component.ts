import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DispatcherApiService } from '../api/api/dispatcherApi.service';

@Component({
  selector: 'app-rideshare-modal',
  templateUrl: './rideshare-modal.component.html',
  styleUrls: ['./rideshare-modal.component.scss']
})
export class RideshareModalComponent implements OnInit {
  @Input() driveId: string;
  @Output() closeRideshareModalClick = new EventEmitter<boolean>();
  @Output() closeRideshareModalAndUpdateClick = new EventEmitter<boolean>();

  constructor( private dispatcherService: DispatcherApiService ) { }

  ngOnInit() { }

  handleCloseModalClick() {
    this.closeRideshareModalClick.emit(true);
  }

  markDriveAsRideshare() {
    this.dispatcherService.updateDriveStatus(this.driveId, 4).subscribe(
      res => {
        console.log("Success! Updated drive status to rideshare.");
        this.closeRideshareModalAndUpdateClick.emit(true);
      },
      err => {
        console.error("ERROR:", err);
      }
    );
  }

}
