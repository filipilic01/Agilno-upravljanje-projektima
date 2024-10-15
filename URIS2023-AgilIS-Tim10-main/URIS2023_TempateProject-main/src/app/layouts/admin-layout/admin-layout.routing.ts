import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { TableListComponent } from '../../table-list/table-list.component';
import { NotificationsComponent } from '../../notifications/notifications.component';
import { RegistrationComponent } from 'app/components/registration/registration.component';
import { LogInComponent } from 'app/components/log-in/log-in.component';
import { TeamComponent } from 'app/components/team/team.component';
import { MyTeamsComponent } from 'app/components/my-teams/my-teams.component';
import { MyProjectsComponent } from 'app/components/my-projects/my-projects.component';
import { ProjectComponent } from 'app/components/project/project.component';
import { HomeComponent } from 'app/components/home/home.component';

export const AdminLayoutRoutes: Routes = [
    // {
    //   path: '',
    //   children: [ {
    //     path: 'dashboard',
    //     component: DashboardComponent
    // }]}, {
    // path: '',
    // children: [ {
    //   path: 'userprofile',
    //   component: UserProfileComponent
    // }]
    // }, {
    //   path: '',
    //   children: [ {
    //     path: 'icons',
    //     component: IconsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'notifications',
    //         component: NotificationsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'maps',
    //         component: MapsComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'typography',
    //         component: TypographyComponent
    //     }]
    // }, {
    //     path: '',
    //     children: [ {
    //         path: 'upgrade',
    //         component: UpgradeComponent
    //     }]
    // }
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'table-list',     component: TableListComponent },
    { path: 'notifications',  component: NotificationsComponent },
    { path: 'registration',      component: RegistrationComponent },
    { path: 'login',      component: LogInComponent },
    { path: 'team', component:TeamComponent},
    { path: 'my-teams', component:MyTeamsComponent},
    { path: 'my-projects', component:MyProjectsComponent},
    { path: 'project', component:ProjectComponent},
    { path: 'home', component:HomeComponent}
];
