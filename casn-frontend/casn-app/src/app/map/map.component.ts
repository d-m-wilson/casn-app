import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit {
  @Input() rides: any = {};
  @Output() closeMapModalClick = new EventEmitter<boolean>();
  mapCenter: any = { latitude: 29.7604, longitude: -95.3698, zoom: 11 };
  coords: any[];

  constructor() { }

  ngOnInit() {
    this.coords = [
      [29.767381, -95.349843],
      [29.783622, -95.322733],
      [29.787346, -95.355677],
      [29.759036, -95.378155],
      [29.737873, -95.416263],
      [29.703232, -95.408203]
    ]
  }

/*********************************************************************
                            Click Handlers
**********************************************************************/
  handleCloseModalClick() {
    this.closeMapModalClick.emit(true);
  }

}
