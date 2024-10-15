import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/services/auth/auth.service';

declare const $: any;
declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}

export const ROUTES: RouteInfo[] = [
  { path: '/user-profile', title: 'Application users', icon: 'person', class: '' },
  { path: '/table-list', title: 'FAQ section', icon: 'content_paste', class: '' },
  { path: '/team', title: 'Create new team', icon: 'add', class: '' },
  { path: '/my-teams', title: 'My teams', icon: 'group', class: '' },
  { path: '/project', title: 'Create new project', icon: 'add', class: '' }
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];

  constructor(private cdr: ChangeDetectorRef, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.loadCurrentUser();
    this.menuItems = ROUTES.filter(menuItem => menuItem);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };

 

  redirectToHome(){
    this.router.navigateByUrl('/home');
  }
  loadCurrentUser(){
    this.authService.loadCurrentUser(localStorage.getItem('token'));
  }
}