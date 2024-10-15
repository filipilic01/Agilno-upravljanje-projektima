import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CriteriaDialogComponent } from './criteria-dialog.component';

describe('CriteriaDialogComponent', () => {
  let component: CriteriaDialogComponent;
  let fixture: ComponentFixture<CriteriaDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CriteriaDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CriteriaDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
