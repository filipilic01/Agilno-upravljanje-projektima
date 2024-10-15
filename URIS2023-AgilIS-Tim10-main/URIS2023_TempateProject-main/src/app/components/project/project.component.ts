import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Project } from 'app/models/project';
import { Team } from 'app/models/team';
import { ProjectService } from 'app/services/project/project.service';
import { TeamServiceService } from 'app/services/team/team-service.service';
import { DatePipe } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogService } from 'app/services/backlog/backlog.service';
import { ProductBacklogCreation } from 'app/models/product-backlog';
import { SprintBacklogCreation } from 'app/models/sprint-backlog';

@Component({
  selector: 'project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

 
  teams: Team = new Team ('', '', '','')
  project: Project = new Project('3fa85f64-5717-4562-b3fc-2c963f66afa6','','','','',)
  projectControl= new FormControl();
  prod : ProductBacklogCreation
  sprint: SprintBacklogCreation
  id: string

  constructor(private teamService: TeamServiceService,
     private projectService:ProjectService,
     private backlogService: BacklogService,
     private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.teamService.getTeamsForUsername(localStorage.getItem('username')).subscribe(res=> {
      console.log(res);
      this.teams=res;
      
  }
  )}


  createProject(){
    this.project.timID = this.projectControl.value;
    this.project.datumProjekta = new Date().toISOString();
    this.projectService.postProject(this.project).subscribe(res =>{
     
      this.id=res.projekatID
     this.backlogService.postProductBacklog(new ProductBacklogCreation('/','/','/','/','Product backlog', res.projekatID,localStorage.getItem('id'))).subscribe(result=> {
      var currentDate = new Date();
      currentDate.setSeconds(currentDate.getSeconds() + 20);
      var newISOString = currentDate.toISOString();
      this.sprint = new SprintBacklogCreation('/',new Date().toISOString(),newISOString, '/','Sprint',this.id, localStorage.getItem('id'));
      console.log(this.sprint)
        this.backlogService.postSprintBacklog(this.sprint).subscribe(resultat => {

        })
      })
      
      this.snackBar.open('Succesfully created project! ', 'OK', {
        duration: 2500
      })
   })
  }
}
