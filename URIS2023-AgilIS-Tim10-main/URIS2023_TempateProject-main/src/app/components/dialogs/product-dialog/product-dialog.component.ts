import { Component, OnInit,  Output, EventEmitter, Inject} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProductBacklog, ProductBacklogCreation } from 'app/models/product-backlog';
import { BacklogService } from 'app/services/backlog/backlog.service';
import { ViewChild,ElementRef } from '@angular/core';


@Component({
  selector: 'product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent implements OnInit {

  @Output() itemAdded = new EventEmitter();
  @ViewChild('titleInput') titleInput: ElementRef;
  @ViewChild('descInput') descInput: ElementRef;
  title: string
  desc: string

  constructor(@Inject (MAT_DIALOG_DATA) public data: ProductBacklog,
  public dialogRef: MatDialogRef<[ProductDialogComponent]>,
  private backlogService: BacklogService,
  private snackBar: MatSnackBar) { }

  ngOnInit(): void {
  this.title= this.data.naslov;
   this.desc = this.data.opis;
  }

  
  public cancel() : void{
    this.dialogRef.close();
  }

  saveBacklog( ){
    console.log(this.data)
    this.data.naslov = this.titleInput.nativeElement.value;
    this.data.opis  = this.descInput.nativeElement.value;
    this.backlogService.updateProductBacklog(this.data).subscribe(res => {
      console.log(res);

      this.snackBar.open('Succesfully updated product backlog ', 'OK', {
        duration: 2500
        
      })
      this.itemAdded.emit();
      this.dialogRef.close();
    })
  }

}
