import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem } from 'app/models/backog-item';
import { Rag, RagCreation, RagUpdate } from 'app/models/rag';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';
import { RagService } from 'app/services/rag/rag.service';

@Component({
  selector: 'rag-dialog',
  templateUrl: './rag-dialog.component.html',
  styleUrls: ['./rag-dialog.component.scss']
})
export class RagDialogComponent implements OnInit {

  public flagRag! : number
  rag: Rag = new Rag()
  constructor(@Inject (MAT_DIALOG_DATA) public data: BacklogItem,
  public dialogRef: MatDialogRef<RagDialogComponent>,
  private backlogItemService: BacklogItemService,
  private ragService: RagService,
  private snackBar: MatSnackBar) { }


  
  ngOnInit(): void {
    if(this.flagRag != 1){
      this.backlogItemService.getRag(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if (res) {
          this.rag = res;
        }
      })
    }
  }

  
  
  public addRag(): void {

    this.ragService.postRag(new RagCreation(this.rag.ragValue, this.data.backlogItemId)).subscribe(() => {
      this.snackBar.open('Succesfully added rag value: ' + this.rag.ragValue, 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Rag value for this backlog item already exists. ', 'Close', {
          duration: 2500
        })
       
      };
}


public editRag(): void {
    this.ragService.updateRag(new RagUpdate(this.rag.ragId, this.rag.ragValue)).subscribe(() => {
      this.snackBar.open('Succesfully updated rag value!', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating rag value', 'Close', {
          duration: 2500
        })
      };

}

public deleteRag(): void {
    this.ragService.deleteRag(this.rag.ragId).subscribe(() => {
      this.snackBar.open('Succesfully deleted rag value ', 'OK', {
        duration: 2500
      }
      )
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during deleting rag value', 'Close', {
          duration: 2500
        })
      };
}

  public cancel() : void{
    this.dialogRef.close();
  }

}
