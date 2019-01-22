import { Component, OnInit } from '@angular/core';
import { Constants } from './app.constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  opened: boolean;
  menuItems: any[];

  constructor( private constants: Constants ) { }

  ngOnInit() {
    this.menuItems = this.constants.MENU_ITEMS;
  }
}
