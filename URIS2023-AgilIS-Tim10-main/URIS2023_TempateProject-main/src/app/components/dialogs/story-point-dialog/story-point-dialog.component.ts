import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem } from 'app/models/backog-item';
import { StoryPoint, StoryPointCreation, StoryPointUpdate } from 'app/models/story-point';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';

@Component({
  selector: 'story-point-dialog',
  templateUrl: './story-point-dialog.component.html',
  styleUrls: ['./story-point-dialog.component.scss']
})
export class StoryPointDialogComponent implements OnInit {

  public flagPoint! : number
  storyPoint: StoryPoint = new StoryPoint()
  constructor(@Inject (MAT_DIALOG_DATA) public data: BacklogItem,
  public dialogRef: MatDialogRef<StoryPointDialogComponent>,
  private backlogItemService: BacklogItemService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    if(this.flagPoint != 1){
      this.backlogItemService.getStoryPoint(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.storyPoint = res;
        }
        
      })
    }
  }

  
  public addStoryPoint(): void {
    console.log(this.storyPoint.storyPointValue, this.data.backlogItemId)
    console.log(typeof(this.storyPoint.storyPointValue))

    this.backlogItemService.postStoryPoint(new StoryPointCreation(this.storyPoint.storyPointValue, this.data.backlogItemId)).subscribe(() => {
      this.snackBar.open('Succesfully added story point: ' + this.storyPoint.storyPointValue, 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Story point for this backlog item already exists. ', 'Close', {
          duration: 2500
        })
      };
}


public editStoryPoint(): void {
  console.log(this.storyPoint.storyPointId,this.storyPoint.storyPointValue)
    this.backlogItemService.updateStoryPoint(new StoryPointUpdate(this.storyPoint.storyPointId, this.storyPoint.storyPointValue)).subscribe(() => {
      this.snackBar.open('Succesfully updated story point!', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating story point ', 'Close', {
          duration: 2500
        })
      };

}

public deleteStoryPoint(): void {
    this.backlogItemService.deleteStoryPoint(this.storyPoint.storyPointId).subscribe(() => {
      this.snackBar.open('Succesfully deleted story point ', 'OK', {
        duration: 2500
      }
      )
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during deleting story point', 'Close', {
          duration: 2500
        })
      };
}

  public cancel() : void{
    this.dialogRef.close();
  }

}
