import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Team, TeamMember } from 'app/models/team';
import { TeamServiceService } from 'app/services/team/team-service.service';
import { EditTeamDialogComponent } from '../dialogs/edit-team-dialog/edit-team-dialog.component';

@Component({
  selector: 'my-teams',
  templateUrl: './my-teams.component.html',
  styleUrls: ['./my-teams.component.scss']
})
export class MyTeamsComponent implements OnInit {

  public teamsPublic: Team[]=[]
  teamBool : boolean
  membersMap: Map<string, TeamMember []> = new Map<string, TeamMember []>();
  constructor(private teamService: TeamServiceService,
     private dialog:MatDialog,
     private router: Router,
     private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.teamService.getTeamsForUsername(localStorage.getItem('username')).subscribe(res=> {
      //console.log(res);
      this.teamsPublic=res;
      console.log(this.teamsPublic.length);
      if(this.teamsPublic.length==0){
        this.teamBool=true
        this.snackBar.open('You are not member of any team!', 'OK', {
          duration: 6000
        })
      }
      else{
        this.teamBool=false
      }
      this.teamsPublic.forEach(team => {
        this.teamService.getTeamMembersForTeam(team.timID).subscribe(result => {
          //console.log(result)
          this.membersMap.set(team.timID, result)
        })
      })

      console.log(this.membersMap)



     
    })
  }

  openEditTeamDialog(flag, team){
    const dialogRef = this.dialog.open(EditTeamDialogComponent, {
      data: team
    });
    dialogRef.componentInstance.flagTeam = flag;
    dialogRef.componentInstance.itemAdded.subscribe(() => {
      this.ngOnInit()
    });
    
  }

  openProjects(id, name){
    console.log(id)
    this.router.navigate(['/my-projects'],  { queryParams: { id, name } });
  }
}
