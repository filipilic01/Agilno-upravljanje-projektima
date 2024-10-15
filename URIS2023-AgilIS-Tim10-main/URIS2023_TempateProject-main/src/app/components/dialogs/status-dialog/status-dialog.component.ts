import { Component, OnInit, Inject  } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem } from 'app/models/backog-item';
import { Status, StatusCreation, StatusUpdate } from 'app/models/status';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';
import { StatusService } from 'app/services/status/status.service';

@Component({
  selector: 'status-dialog',
  templateUrl: './status-dialog.component.html',
  styleUrls: ['./status-dialog.component.scss']
})
export class StatusDialogComponent implements OnInit {

  public flagStatus! : number
  status: Status = new Status()
  constructor(@Inject (MAT_DIALOG_DATA) public data: BacklogItem,
  public dialogRef: MatDialogRef<StatusDialogComponent>,
  private backlogItemService: BacklogItemService,
  private statusService: StatusService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    if(this.flagStatus != 1){
      this.backlogItemService.getStatus(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if (res) {
          this.status = res;
        }
      })
    }
  }

  
  public addStatus(): void {

    this.statusService.postStatus(new StatusCreation(this.status.vrednostStatusa, this.data.backlogItemId)).subscribe(() => {
      this.snackBar.open('Succesfully added status value: ' + this.status.vrednostStatusa, 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Status value for this backlog item already exists. ', 'Close', {
          duration: 2500
        })
       
      };
}


public editStatus(): void {
    this.statusService.updateStatus(new StatusUpdate(this.status.idStatusa, this.status.vrednostStatusa)).subscribe(() => {
      this.snackBar.open('Succesfully updated status value!', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating status value', 'Close', {
          duration: 2500
        })
      };

}

public deleteRag(): void {
    this.statusService.deleteStatus(this.status.idStatusa).subscribe(() => {
      this.snackBar.open('Succesfully deleted status value ', 'OK', {
        duration: 2500
      }
      )
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during deleting status', 'Close', {
          duration: 2500
        })
      };
}

  public cancel() : void{
    this.dialogRef.close();
  }


}
