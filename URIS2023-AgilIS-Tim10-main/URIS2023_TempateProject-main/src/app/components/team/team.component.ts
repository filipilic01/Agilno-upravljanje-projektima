import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Team, TeamMember } from 'app/models/team';
import { UserDto } from 'app/models/user';
import { TeamServiceService } from 'app/services/team/team-service.service';
import { UserServiceService } from 'app/services/user-service.service';

@Component({
  selector: 'team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.scss']
})
export class TeamComponent implements OnInit {
  team: Team = new Team('3fa85f64-5717-4562-b3fc-2c963f66afa6', '', '', localStorage.getItem('id'))
  teamUsers:string [] = []
  users: UserDto[] = []
  userArray= new FormControl([]);

  constructor(private userService: UserServiceService, private teamService: TeamServiceService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.userService.getUsers().subscribe(res => {
      this.users=res;
    })
  }

  createTeam(){
    const selectedUsers: UserDto[] = this.userArray.value;
    
    // Mapiranje samo username-ova
    const usernames: string[] = selectedUsers.map(user => user.username);

    // Dodavanje u niz
    usernames.forEach(username => {
      if (!this.teamUsers.includes(username)) {
        this.teamUsers.push(username);
      }
    });
    this.team.brojClanova=this.teamUsers.length.toString();
    console.log(this.team)
    this.teamService.postTeam(this.team).subscribe(res=>{
      console.log(res);
      this.teamUsers.forEach(teamUser =>{
        this.teamService.postTeamMember(new TeamMember('3fa85f64-5717-4562-b3fc-2c963f66afa6', teamUser, res.timID)).subscribe(result =>{
          console.log(result);
        })
      })
      this.snackBar.open('Succesfully created team! ', 'OK', {
        duration: 2500
      })
    })

    
  }

}
