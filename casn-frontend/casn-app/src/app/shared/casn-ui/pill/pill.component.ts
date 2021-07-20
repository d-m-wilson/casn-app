import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pill',
  templateUrl: './pill.component.html',
  styleUrls: ['./pill.component.scss']
})
export class PillComponent implements OnInit {
  @Input() value: string;
  @Input() label: string;

  constructor() { }

  ngOnInit(): void {
  }

}
