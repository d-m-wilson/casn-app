import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-ride-detail-modal',
  templateUrl: './ride-detail-modal.component.html',
  styleUrls: ['./ride-detail-modal.component.css']
})
export class RideDetailModalComponent implements OnInit {
  @Input() ride: any = {};
  @Output() closeModalClick = new EventEmitter<boolean>();

  constructor() { }

  ngOnInit() { }

  handleCloseModalClick() {
    this.closeModalClick.emit(true);
  }
}
