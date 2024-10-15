import { Component, OnInit } from '@angular/core';
declare var $: any;
@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

 
  constructor() { }

  ngOnInit() {
    console.log("success")
  }

  

showNotification(from, align) {
  const type = ['danger'];

  const color = Math.floor((Math.random() * 4) + 1);

  $.notify({
      icon: "notifications",
      message: ` <div class="notification-description">
      <p>
          <em class="fa fa-info fa-fw fa-2x"></em>
          <strong>Info!</strong> <p>This software tool is a project at the Faculty of Technical Sciences and was developed by the following IT students:</p>
          <p>Dajana Isaku</p>
          <p>Filip Ilić</p>
          <p>Marko Petrovački</p>
          <p>Ljiljana Đurić.</p>
  </div>`

  },{
      type: type[color],
      timer: 4000,
      placement: {
          from: from,
          align: align
      }
      
  });
}
}
