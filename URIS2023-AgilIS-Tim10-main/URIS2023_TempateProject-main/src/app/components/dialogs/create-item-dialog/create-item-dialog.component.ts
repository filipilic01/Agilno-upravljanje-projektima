import { Component, ElementRef, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BacklogItem, BacklogItemDto } from 'app/models/backog-item';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';

@Component({
  selector: 'create-item-dialog',
  templateUrl: './create-item-dialog.component.html',
  styleUrls: ['./create-item-dialog.component.scss']
})
export class CreateItemDialogComponent implements OnInit {
  Item: string;
  backlogItemDto: BacklogItemDto = { backlogItemName: '', userId: '', backlogId: '' };
  backlogItemsProduct: BacklogItemDto[]; 
  id: string;
  @Output() dataLoad = new EventEmitter<BacklogItemDto[]>();

  constructor(
    @Inject(MAT_DIALOG_DATA) public dialogData: any,
    public dialogRef: MatDialogRef<CreateItemDialogComponent>,
    private snackBar: MatSnackBar,
    private backlogItemService: BacklogItemService
  ) { }

  ngOnInit(): void {
    this.backlogItemsProduct = this.dialogData.backlogItemsProduct;
    this.id = this.dialogData.id;
  }

  addBacklogItem() {
    let serviceMethod: any;
    switch (this.Item) {
      case 'Epic':
        serviceMethod = this.backlogItemService.postEpic;
        break;
      case 'Functionality':
        serviceMethod = this.backlogItemService.postFunc;
        break;
      case 'UserStory':
        serviceMethod = this.backlogItemService.postStory;
        break;
      case 'Task':
        serviceMethod = this.backlogItemService.postTask;
        break;
      default:
        break;
    }
  
    if (serviceMethod) {
      serviceMethod.call(this.backlogItemService, new BacklogItemDto(this.backlogItemDto.backlogItemName, '6cff81d6-e05d-4f6d-829f-464ce9ef911a', this.id)).subscribe(
        (response: any) => {
          const newItem: BacklogItem = response;
          this.backlogItemsProduct.push(newItem);
          this.dataLoad.emit(this.backlogItemsProduct);
          this.dialogRef.close();
          this.snackBar.open('Successfully added backlog item: ' + newItem.backlogItemName, 'OK', {
            duration: 2500
          });
        },
        error => {
          console.error("Error adding backlog item", error);
        }
      );
    }
  }
  

  public cancel(): void {
    this.dialogRef.close();
  }

}

