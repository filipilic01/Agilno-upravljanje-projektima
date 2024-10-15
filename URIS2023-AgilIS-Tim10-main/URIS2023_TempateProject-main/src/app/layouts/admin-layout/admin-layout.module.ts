import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import {MatButtonModule} from '@angular/material/button';
import {MatInputModule} from '@angular/material/input';
import {MatRippleModule} from '@angular/material/core';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatSelectModule} from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog'
import { TableListDialogComponent } from 'app/table-list-dialog/table-list-dialog.component';
import { RegistrationComponent } from 'app/components/registration/registration.component';
import { LogInComponent } from 'app/components/log-in/log-in.component';
import { MatOptionModule } from '@angular/material/core';
import { CreateItemDialogComponent } from 'app/components/dialogs/create-item-dialog/create-item-dialog.component';
import { DragDropModule } from '@angular/cdk/drag-drop';
import {MatIconModule} from '@angular/material/icon';
import { ItemDetailsComponent } from 'app/components/dialogs/item-details/item-details.component';
import { MatMenuModule } from '@angular/material/menu';
import { CriteriaDialogComponent } from 'app/components/dialogs/criteria-dialog/criteria-dialog.component';
import { DescriptionDialogComponent } from 'app/components/dialogs/description-dialog/description-dialog.component';
import { StoryPointDialogComponent } from 'app/components/dialogs/story-point-dialog/story-point-dialog.component';
import { RagDialogComponent } from 'app/components/dialogs/rag-dialog/rag-dialog.component';
import { StatusDialogComponent } from 'app/components/dialogs/status-dialog/status-dialog.component';
import { EditItemDialogComponent } from 'app/components/dialogs/edit-item-dialog/edit-item-dialog.component';
import { TeamComponent } from 'app/components/team/team.component';
import { MyTeamsComponent } from 'app/components/my-teams/my-teams.component';
import { MyProjectsComponent } from 'app/components/my-projects/my-projects.component';
import { ProjectComponent } from 'app/components/project/project.component';
import { SprintDialogComponent } from 'app/components/dialogs/sprint-dialog/sprint-dialog.component';
import { NgFlatpickrModule } from 'ng-flatpickr';
import { MatDatepicker, MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { ProductDialogComponent } from 'app/components/dialogs/product-dialog/product-dialog.component';
import { HomeComponent } from 'app/components/home/home.component';
import { EditTeamDialogComponent } from 'app/components/dialogs/edit-team-dialog/edit-team-dialog.component';








@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    MatDialogModule,
    MatSnackBarModule,
    MatOptionModule,
    DragDropModule,
    MatIconModule,
    MatMenuModule,
    MatDatepickerModule,
    MatNativeDateModule


  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TableListComponent,
    NotificationsComponent,
    TableListDialogComponent,
    RegistrationComponent,
    LogInComponent, 
    CreateItemDialogComponent,
    ItemDetailsComponent,
    CriteriaDialogComponent,
    DescriptionDialogComponent,
    StoryPointDialogComponent,
    RagDialogComponent,
    StatusDialogComponent,
    EditItemDialogComponent,
    TeamComponent,
    MyTeamsComponent,
    MyProjectsComponent,
    ProjectComponent,
    SprintDialogComponent,
    ProductDialogComponent,
    HomeComponent,
    EditTeamDialogComponent


  
    
  ]
})

export class AdminLayoutModule {}
