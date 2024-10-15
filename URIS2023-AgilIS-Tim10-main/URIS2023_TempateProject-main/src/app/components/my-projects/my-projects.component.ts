import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'app/models/project';
import { TeamServiceService } from 'app/services/team/team-service.service';

@Component({
  selector: 'my-projects',
  templateUrl: './my-projects.component.html',
  styleUrls: ['./my-projects.component.scss']
})
export class MyProjectsComponent implements OnInit {
  projects: Project[] =[]
  id:string
  name:string
  projectBool : boolean
  
  constructor( private route: ActivatedRoute, private router:Router, private teamService: TeamServiceService, private snackBar:MatSnackBar) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      this.name = params['name']
      console.log(this.id)
      console.log(this.name)
      this.teamService.getProjectsForTeam(this.id).subscribe(res => {
        if(res){
          this.projects = res;
        }
        if(this.projects.length==0){
          this.projectBool=true
          this.snackBar.open('There is no projects for team!', 'OK', {
            duration: 6000
          })
        }
        else{
          this.projectBool=false
        }
      })
      
    })
    }

    openDashboard(id){
      this.router.navigate(['/dashboard'],  { queryParams: { id } })
    }
}
