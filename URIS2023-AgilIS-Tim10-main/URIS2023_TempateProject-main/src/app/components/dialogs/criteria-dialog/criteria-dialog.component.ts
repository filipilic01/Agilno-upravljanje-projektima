import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AcceptanceCriteria, AcceptanceCriteriaCreation, AcceptanceCriteriaUpdate } from 'app/models/acceptance-criteria';
import { BacklogItem } from 'app/models/backog-item';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';

@Component({
  selector: 'criteria-dialog',
  templateUrl: './criteria-dialog.component.html',
  styleUrls: ['./criteria-dialog.component.scss']
})
export class CriteriaDialogComponent implements OnInit {

public flagCriteria! : number
criteria: AcceptanceCriteria = new AcceptanceCriteria()

  constructor(@Inject (MAT_DIALOG_DATA) public data: BacklogItem,
  public dialogRef: MatDialogRef<CriteriaDialogComponent>,
  private backlogItemService: BacklogItemService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    if(this.flagCriteria != 1){
      this.backlogItemService.getAcceptanceCriteria(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.criteria = res;
        }
        
      })
    }
  }

  public addCriteria(): void {
    this.backlogItemService.postAcceptanceCriteria(new AcceptanceCriteriaCreation(this.criteria.acceptanceCriteriaText, this.data.backlogItemId)).subscribe(() => {
      this.snackBar.open('Succesfully added acceptance criteria: ' + this.criteria.acceptanceCriteriaText, 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Acceptance criteria for this backlog item already exists. ', 'Close', {
          duration: 2500
        })
      };
}


public editCriteria(): void {
  console.log(this.criteria.acceptanceCriteriaId,this.criteria.acceptanceCriteriaText)
    this.backlogItemService.updateAcceptanceCriteria(new AcceptanceCriteriaUpdate(this.criteria.acceptanceCriteriaId, this.criteria.acceptanceCriteriaText)).subscribe(() => {
      this.snackBar.open('Succesfully updated acceptance criteria ', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating acceptance criteria ', 'Close', {
          duration: 2500
        })
      };

}

public deleteCriteria(): void {
    this.backlogItemService.deleteAcceptanceCriteria(this.criteria.acceptanceCriteriaId).subscribe(() => {
      this.snackBar.open('Succesfully deleted acceptance criteria ', 'OK', {
        duration: 2500
      }
      )
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during deleting acceptance criteria ', 'Close', {
          duration: 2500
        })
      };
}

  public cancel() : void{
    this.dialogRef.close();
  }

}
