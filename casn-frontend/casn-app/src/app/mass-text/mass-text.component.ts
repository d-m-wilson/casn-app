import { Component, OnInit } from '@angular/core';
import { SelectionModel } from '@angular/cdk/collections';
import { MatTableDataSource } from '@angular/material/table';

export interface UserContact {
  name: string;
  position: number;
}

const PERSON_DATA: UserContact[] = [
  {position: 1, name: 'Aspen'},
  {position: 2, name: 'Brian'},
  {position: 3, name: 'David'},
  {position: 4, name: 'Katie'},
  {position: 5, name: 'Naima'},
  {position: 6, name: 'Aspen'},
  {position: 7, name: 'Brian'},
  {position: 8, name: 'David'},
  {position: 9, name: 'Katie'},
  {position: 10, name: 'Naima'},
];

@Component({
  selector: 'app-mass-text',
  templateUrl: './mass-text.component.html',
  styleUrls: ['./mass-text.component.scss']
})
export class MassTextComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  displayedColumns: string[] = ['select', 'name'];
  dataSource = new MatTableDataSource<UserContact>(PERSON_DATA);
  selection = new SelectionModel<UserContact>(true, []);

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
        this.selection.clear() :
        this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(row?: UserContact): string {
    if (!row) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.position + 1}`;
  }

}
