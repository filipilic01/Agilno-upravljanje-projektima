import { Component, EventEmitter, OnInit, Output, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem} from 'app/models/backog-item';
import { Team } from 'app/models/team';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';
import { TeamServiceService } from 'app/services/team/team-service.service';

@Component({
  selector: 'edit-team-dialog',
  templateUrl: './edit-team-dialog.component.html',
  styleUrls: ['./edit-team-dialog.component.scss']
})
export class EditTeamDialogComponent implements OnInit {
  @Output() itemAdded = new EventEmitter();
  flagTeam!: number
  constructor(@Inject (MAT_DIALOG_DATA) public data: Team,
  public dialogRef: MatDialogRef<EditTeamDialogComponent>,
  private teamService: TeamServiceService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    
  }
  editTeam(){
    this.teamService.updateTeam(this.data.timID,this.data).subscribe(() => {
      this.itemAdded.emit()
      this.snackBar.open('Succesfully updated team  ', 'OK', {
        duration: 2500
      })
      this.dialogRef.close();
    }),
      (error: Error) => {
        console.log(error.name + ' ' + error.message)
        this.snackBar.open('Error during updating team ', 'Close', {
          duration: 2500
        })
      };
  }

  deleteTeam(){
    this.teamService.deleteTeam(this.data.timID).subscribe(() => {
      this.itemAdded.emit();
      this.snackBar.open('Succesfully deleted team ', 'OK', {
        duration: 2500
      }
    )
       this.dialogRef.close();
    }),
    (error: Error) => {
      console.log(error.name + ' ' + error.message)
      this.snackBar.open('Error during deleting team', 'Close', {
        duration: 2500
      })
    };
  }

  public cancel() : void{
    this.dialogRef.close();
  }
}
