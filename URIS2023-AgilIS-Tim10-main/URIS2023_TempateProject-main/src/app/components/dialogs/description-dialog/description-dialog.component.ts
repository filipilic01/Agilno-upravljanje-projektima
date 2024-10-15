import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem } from 'app/models/backog-item';
import { Description, DescriptionCreation, DescriptionUpdate } from 'app/models/description';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';

@Component({
  selector: 'description-dialog',
  templateUrl: './description-dialog.component.html',
  styleUrls: ['./description-dialog.component.scss']
})
export class DescriptionDialogComponent implements OnInit {

  public flagDescription! : number
  description: Description = new Description()
  
    constructor(@Inject (MAT_DIALOG_DATA) public data: BacklogItem,
    public dialogRef: MatDialogRef<DescriptionDialogComponent>,
    private backlogItemService: BacklogItemService,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    if(this.flagDescription != 1){
      this.backlogItemService.getDescription(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.description = res;
        }
        
      })
    }
  }

  
  public addDescription(): void {
    this.backlogItemService.postDescription(new DescriptionCreation(this.description.descriptionText, this.data.backlogItemId)).subscribe(() => {
      this.snackBar.open('Succesfully added description: ' + this.description.descriptionText, 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Description for this backlog item already exists. ', 'Close', {
          duration: 2500
        })
        this.dialogRef.close();
      };
}


public editDescription(): void {
  console.log(this.description.descriptionId,this.description.descriptionText)
    this.backlogItemService.updateDescription(new DescriptionUpdate(this.description.descriptionId, this.description.descriptionText)).subscribe(() => {
      this.snackBar.open('Succesfully updated description!', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating description ', 'Close', {
          duration: 2500
        })
        this.dialogRef.close();
      };

}

public deleteDescription(): void {
    this.backlogItemService.deleteDescription(this.description.descriptionId).subscribe(() => {
      this.snackBar.open('Succesfully deleted description ', 'OK', {
        duration: 2500
      }
      )
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during deleting description', 'Close', {
          duration: 2500
        })
      };
}

  public cancel() : void{
    this.dialogRef.close();
  }

}
