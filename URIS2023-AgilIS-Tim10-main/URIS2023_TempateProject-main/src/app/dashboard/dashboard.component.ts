import { Component, ElementRef, OnInit, ViewChild, ChangeDetectorRef, EventEmitter, Output  } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Backlog } from 'app/models/backlog';
import { BacklogItem } from 'app/models/backog-item';
import { BacklogService } from 'app/services/backlog/backlog.service';
import { Observable, Subscription, of } from 'rxjs';
import { SprintBacklog } from 'app/models/sprint-backlog';
import { ProductBacklog } from 'app/models/product-backlog';
import { CreateItemDialogComponent } from 'app/components/dialogs/create-item-dialog/create-item-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { BacklogItemService } from 'app/services/backlogItems/backlog-item.service';
import { Location } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ItemDetailsComponent } from 'app/components/dialogs/item-details/item-details.component';
import { CriteriaDialogComponent } from 'app/components/dialogs/criteria-dialog/criteria-dialog.component';
import { DescriptionDialogComponent } from 'app/components/dialogs/description-dialog/description-dialog.component';
import { StoryPointDialogComponent } from 'app/components/dialogs/story-point-dialog/story-point-dialog.component';
import { switchMap, tap } from 'rxjs/operators';
import { RagDialogComponent } from 'app/components/dialogs/rag-dialog/rag-dialog.component';
import { StatusDialogComponent } from 'app/components/dialogs/status-dialog/status-dialog.component';
import { EditItemDialogComponent } from 'app/components/dialogs/edit-item-dialog/edit-item-dialog.component';
import { SprintDialogComponent } from 'app/components/dialogs/sprint-dialog/sprint-dialog.component';
import { ProductDialogComponent } from 'app/components/dialogs/product-dialog/product-dialog.component';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  @ViewChild('circleRef') circleRef: ElementRef;
  backlogs: Backlog[] = [];
  sprint: SprintBacklog = new SprintBacklog('','','','','','','','');
  product: ProductBacklog = new ProductBacklog('','','','','','', '', '') ;
  backlogItemsSprint : BacklogItem[] = [];
  backlogItemsProduct: BacklogItem[] = [];
  id: string
  subscription!: Subscription;
  @Output() dataLoad: EventEmitter<BacklogItem[]> = new EventEmitter<BacklogItem[]>();


  constructor( private backlogService: BacklogService, 
    private itemService: BacklogItemService, 
    private router: Router,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      console.log(this.id)
      this.getProductBacklog();
      this.getSprintBacklog();
    })
   
     
  }

  
  getProductBacklog() {
    this.backlogService.getProductBacklogByProjekatId(this.id)
      .pipe(
        switchMap(product => {
          this.product = product;
          this.getAllBacklogItemsForProduct();
          return of(product);
        })
      )
      .subscribe();
  }
  
  getSprintBacklog() {
    this.backlogService.getSprintBacklogByProjekatId(this.id)
      .pipe(
        switchMap(sprint => {
          this.sprint = sprint;
          this.getAllBacklogItemsForSprint();
          return of(sprint);
        })
      )
      .subscribe();
  }

  getAllBacklogItemsForProduct(){
      this.subscription = this.backlogService.getBacklogItemsForBacklogId(this.product.idBacklog)
      .subscribe(
        (data: BacklogItem[]) => {
          this.backlogItemsProduct = data;
          return this.backlogItemsProduct;
        },
        (error) => {
          console.error('Error fetching product backlog:', error);
        }
      );
  }
  
  getAllBacklogItemsForSprint() {
      this.subscription = this.backlogService.getBacklogItemsForBacklogId(this.sprint.idBacklog)
      .subscribe(
        (data: BacklogItem[]) => {
          this.backlogItemsSprint = data;
          return this.backlogItemsSprint;
        },
        (error) => {
          console.error('Error fetching sprint backlog:', error);
        }
      );
  }

  getEpicsForSprintBacklogId(id): void {
    this.backlogService.getEpicsForBacklogId(id).subscribe(epics => {
      this.backlogItemsSprint = epics;
      
    });
  }
  
  getFunctionalitiesForSprintBacklogId(id): void {
    this.backlogService.getFunctionalitiesForBacklogId(id).subscribe(functionalities => {
      this.backlogItemsSprint = functionalities
     
    });
  }
  
  getUserStoriesForSprintBacklogId(id): void {
    this.backlogService.getUserStoriesForBacklogId(id).subscribe(userStories => {
      this.backlogItemsSprint = userStories;
   
    });
  }
  
  getTasksForProductBacklogId(id): void {
    this.backlogService.getTasksForBacklogId(id).subscribe(tasks => {
      this.backlogItemsProduct = tasks;
      
    });
  }

  
  getEpicsForProductBacklogId(id): void {
    this.backlogService.getEpicsForBacklogId(id).subscribe(epics => {
      this.backlogItemsProduct = epics;
      
    });
  }
  
  getFunctionalitiesForProductBacklogId(id): void {
    this.backlogService.getFunctionalitiesForBacklogId(id).subscribe(functionalities => {
      this.backlogItemsProduct = functionalities
     
    });
  }
  
  getUserStoriesForProductBacklogId(id): void {
    this.backlogService.getUserStoriesForBacklogId(id).subscribe(userStories => {
      this.backlogItemsProduct = userStories;
   
    });
  }
  
  getTasksForSprintBacklogId(id): void {
    this.backlogService.getTasksForBacklogId(id).subscribe(tasks => {
      this.backlogItemsSprint = tasks;
      
    });
  }
  
  

  updateSprintItem(item: BacklogItem){
    this.itemService.updateBacklogItem(new BacklogItem
      (item.backlogItemId, 
      item.backlogItemName,
      localStorage.getItem('id'),
      this.product.idBacklog.toString()
      ))
      .subscribe(
        (updatedItem: BacklogItem) =>  {
        this.snackBar.open('Successfully moved backlog item: ' + item.backlogItemName, 'OK', {
          duration: 2500
        });
        const index = this.backlogItemsSprint.findIndex(x => x.backlogItemId === updatedItem.backlogItemId);
        if (index !== -1) {
          this.backlogItemsSprint.splice(index, 1); //brisanje iz liste product backlogitema
        }
        this.backlogItemsProduct.push(updatedItem);
        this.dataLoad.emit(this.backlogItemsProduct);
        },
        (error: any) => {
          console.error('Error updating backlog item:', error);
        }
      );
    }

    updateProductItem(item: BacklogItem): void {
      this.itemService.updateBacklogItem(new BacklogItem(
        item.backlogItemId,
        item.backlogItemName,
        localStorage.getItem('id'),
        this.sprint.idBacklog.toString()
      )).subscribe(
        (updatedItem: BacklogItem) => {
          this.snackBar.open('Successfully moved backlog item: ' + updatedItem.backlogItemName, 'OK', {
            duration: 2500
          });
          const index = this.backlogItemsProduct.findIndex(x => x.backlogItemId === updatedItem.backlogItemId);
          if (index !== -1) {
            this.backlogItemsProduct.splice(index, 1); //brisanje iz liste product backlogitema
          }
          this.backlogItemsSprint.push(updatedItem);
          this.dataLoad.emit(this.backlogItemsSprint);
        },
        (error: any) => {
          console.error('Error updating backlog item:', error);
        }
      );
    }
    
  openItemDetailsDialog(item: BacklogItem):void{
    const dialogRef = this.dialog.open(ItemDetailsComponent, {
      data: item
    });
    dialogRef.componentInstance.itemAdded.subscribe(() => {
      this.getAllBacklogItemsForProduct();
      this.getAllBacklogItemsForSprint();
    });
  }

  openCreateDialog(id): void {
    const dialogRef = this.dialog.open(CreateItemDialogComponent, {
      data: {
        backlogItemsProduct: this.backlogItemsProduct,
        id: id
      }
    });
  
    dialogRef.componentInstance.dataLoad.subscribe((updatedBacklogItemsProduct: BacklogItem[]) => {
      this.backlogItemsProduct = updatedBacklogItemsProduct;
      this.cdr.detectChanges();
    });

    dialogRef.afterClosed().subscribe(res => {
      if (res === 1) this.getProductBacklog();
      this.getSprintBacklog();
    });
  }
  
  openCriteriaDialog(flag: number,data:BacklogItem): void{
    const dialogRef = this.dialog.open(CriteriaDialogComponent, {
      data: data
    });
    dialogRef.componentInstance.flagCriteria = flag;
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
      this.getSprintBacklog();})
    
  }

  openDescriptionDialog(flag: number,data:BacklogItem): void{
    const dialogRef = this.dialog.open(DescriptionDialogComponent, {
      data: data
    });
    dialogRef.componentInstance.flagDescription = flag;
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
      this.getSprintBacklog();})
  }

  openStoryPointDialog(flag: number,data:BacklogItem): void{
    const dialogRef = this.dialog.open(StoryPointDialogComponent, {
      data: data
    });
    dialogRef.componentInstance.flagPoint = flag;
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
      this.getSprintBacklog();})
  }

  openRagDialog(flag: number,data:BacklogItem): void{
    const dialogRef = this.dialog.open(RagDialogComponent, {
      data: data
    });
    dialogRef.componentInstance.flagRag = flag;
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
      this.getSprintBacklog();})
  }

  openStatusDialog(flag: number,data:BacklogItem): void{
    const dialogRef = this.dialog.open(StatusDialogComponent, {
      data: data
    });
    dialogRef.componentInstance.flagStatus = flag;
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
      this.getSprintBacklog();})
  }

  openEditDialog(flag: number, data:BacklogItem):void {
    const dialogRef = this.dialog.open(EditItemDialogComponent, {
      data: data
    });
    dialogRef.componentInstance.flagItem = flag;
    dialogRef.componentInstance.itemAdded.subscribe(() => {
      this.getProductBacklog();
      this.getSprintBacklog();
    });
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
    this.getSprintBacklog();})
  }


  openSprintDialog(){
    const dialogRef = this.dialog.open(SprintDialogComponent, {
      data: this.sprint
    });
    dialogRef.componentInstance.itemAdded.subscribe(() => {
      this.getProductBacklog();
      this.getSprintBacklog();
    });
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
    this.getSprintBacklog();})
  }

  openProductDialog(){
    const dialogRef = this.dialog.open(ProductDialogComponent, {
      data: this.product
    });
    dialogRef.componentInstance.itemAdded.subscribe(() => {
      this.getProductBacklog();
      this.getSprintBacklog();
    });
    dialogRef.afterClosed().subscribe(res=> {if(res===1) this.getProductBacklog();
    this.getSprintBacklog();})
  }
}

  


