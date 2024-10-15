import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Contributor } from 'app/models/contributor';
import { Task } from 'app/models/task';
import { User } from 'app/models/user';

import { TasksService } from 'app/services/tasks/tasks.service';
import { UserServiceService } from 'app/services/user-service.service';
import { TableListDialogComponent } from 'app/table-list-dialog/table-list-dialog.component';

@Component({
  selector: 'app-table-list',
  templateUrl: './table-list.component.html',
  styleUrls: ['./table-list.component.css']
})

// Komponenta TableListComponent je deo koda koji cemo na vezbama menjati
export class TableListComponent implements OnInit {


  // Kroz konstruktor uvek zelimo da injektujemo sve potrebne servise, koje kasnije mozemo slobodno koristiti u kodu.
  constructor(private userService: UserServiceService,
              private dialogModel: MatDialog) { }

 
  ngOnInit() {
    
  }

}
