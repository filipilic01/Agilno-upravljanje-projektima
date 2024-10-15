import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Contributor } from 'app/models/contributor';


@Component({
  selector: 'table-list-dialog',
  templateUrl: './table-list-dialog.component.html',
  styleUrls: ['./table-list-dialog.component.scss']
})
export class TableListDialogComponent implements OnInit {


  constructor(
    public dialogRef: MatDialogRef<TableListDialogComponent>) { }

  ngOnInit(): void {
  }

  onClick(): void {
    this.dialogRef.close();
  }

}
