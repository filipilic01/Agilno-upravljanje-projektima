import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RagDialogComponent } from './rag-dialog.component';

describe('RagDialogComponent', () => {
  let component: RagDialogComponent;
  let fixture: ComponentFixture<RagDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RagDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RagDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
