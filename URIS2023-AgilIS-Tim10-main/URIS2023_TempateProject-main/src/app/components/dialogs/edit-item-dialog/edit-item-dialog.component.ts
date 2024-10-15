import { Component, EventEmitter, OnInit, Output, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem} from 'app/models/backog-item';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';

@Component({
  selector: 'edit-item-dialog',
  templateUrl: './edit-item-dialog.component.html',
  styleUrls: ['./edit-item-dialog.component.scss']
})
export class EditItemDialogComponent implements OnInit {
  @Output() itemAdded = new EventEmitter();
  flagItem!: number
  constructor(@Inject (MAT_DIALOG_DATA) public data: BacklogItem,
  public dialogRef: MatDialogRef<EditItemDialogComponent>,
  private backlogItemService: BacklogItemService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    
  }

  editBacklogItem(){
    this.backlogItemService.updateBacklogItem(this.data).subscribe(() => {
      this.snackBar.open('Succesfully updated backlog item ', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating backlog item ', 'Close', {
          duration: 2500
        })
      };

  }

  
   deleteBacklogItem(): void {
    this.backlogItemService.deleteBacklogItem(this.data.backlogItemId).subscribe(() => {
      this.snackBar.open('Succesfully deleted backlog item ', 'OK', {
        duration: 2500
      }
    )
       this.dialogRef.close();
    }),
    (error: Error) => {
      console.log(error.name + ' ' + error.message)
      this.snackBar.open('Error during deleting backlog item', 'Close', {
        duration: 2500
      })
    };
  }

  public cancel() : void{
    this.dialogRef.close();
  }

}
