import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BacklogItem } from 'app/models/backog-item';
import { MatOptionModule } from '@angular/material/core';
import { Description } from 'app/models/description';
import { Rag } from 'app/models/rag';
import { AcceptanceCriteria } from 'app/models/acceptance-criteria';
import { StoryPoint } from 'app/models/story-point';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';
import { Status } from 'app/models/status';


@Component({
  selector: 'item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.scss']
})
export class ItemDetailsComponent implements OnInit {

  @Output() itemAdded = new EventEmitter();

  description: Description = new Description()
  rag: Rag = new Rag()
  criteria: AcceptanceCriteria = new AcceptanceCriteria()
  storyPoint: StoryPoint = new StoryPoint()
  status: Status = new Status()

  constructor( @Inject (MAT_DIALOG_DATA) public data: BacklogItem,
  public dialogRef: MatDialogRef<ItemDetailsComponent>,
  private backlogItemService: BacklogItemService) { }

  ngOnInit(): void {
      this.backlogItemService.getAcceptanceCriteria(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.criteria = res;
        }
        
      });
      this.backlogItemService.getDescription(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.description = res;
        }
        
      });
      this.backlogItemService.getRag(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if (res) {
          this.rag = res;
        }
      });
      this.backlogItemService.getStoryPoint(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.storyPoint = res;
        }
        
      });
      this.backlogItemService.getStatus(this.data.backlogItemId).subscribe(res => {
        console.log(res);
        if(res){
          this.status = res;
        }
        
      });

  }

  public cancel() : void{
    this.dialogRef.close();
  }
}
