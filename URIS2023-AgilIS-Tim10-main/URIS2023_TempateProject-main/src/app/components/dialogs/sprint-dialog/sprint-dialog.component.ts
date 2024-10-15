import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SprintBacklog } from 'app/models/sprint-backlog';
import { BacklogService } from 'app/services/backlog/backlog.service';
import { ViewChild,ElementRef } from '@angular/core';
import { MatDatepicker } from '@angular/material/datepicker';
import { NgModel } from '@angular/forms';



@Component({
  selector: 'sprint-dialog',
  templateUrl: './sprint-dialog.component.html',
  styleUrls: ['./sprint-dialog.component.scss']
})
export class SprintDialogComponent implements OnInit {
  @ViewChild('startPicker') startPicker: MatDatepicker<Date>;
  @ViewChild('endPicker') endPicker: MatDatepicker<Date>;
  @ViewChild('titleInput') titleInput: ElementRef;
  @ViewChild('descInput') descInput: ElementRef;
  @ViewChild('goalInput') goalInput: ElementRef;
  title: string
  desc: string
  goal: string
  startDate: Date =new Date()
  endDate: Date = new Date()
  @Output() itemAdded = new EventEmitter();
  
  constructor(@Inject (MAT_DIALOG_DATA) public data: SprintBacklog,
  public dialogRef: MatDialogRef<SprintDialogComponent>,
  private backlogService: BacklogService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
   this.startDate=new Date(this.data.pocetak);
   this.endDate = new Date(this.data.kraj);
   this.title= this.data.naslov;
   this.desc = this.data.opis;
   this.goal = this.data.cilj
  }

  public cancel() : void{
    this.dialogRef.close();
  }

  saveBacklog( ){
    this.data.naslov = this.titleInput.nativeElement.value;
    this.data.opis  = this.descInput.nativeElement.value;
    this.data.cilj  = this.goalInput.nativeElement.value;
    const startDate = this.startPicker.startAt;
    const endDate = this.endPicker.startAt;
    this.data.kraj = startDate.toISOString();
    endDate.setSeconds(endDate.getSeconds() + 20);
    this.data.pocetak = startDate.toISOString();
    this.data.kraj = endDate.toISOString();
    console.log(this.data)
    this.backlogService.updateSprintBacklog(this.data).subscribe(res => {
      console.log(res);

      this.snackBar.open('Succesfully updated sprint ', 'OK', {
        duration: 2500
        
      })
      this.itemAdded.emit();
      this.dialogRef.close();
    })
  }


}
