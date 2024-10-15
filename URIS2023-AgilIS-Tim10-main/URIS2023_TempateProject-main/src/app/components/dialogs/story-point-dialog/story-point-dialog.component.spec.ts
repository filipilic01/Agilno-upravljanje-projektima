import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoryPointDialogComponent } from './story-point-dialog.component';

describe('StoryPointDialogComponent', () => {
  let component: StoryPointDialogComponent;
  let fixture: ComponentFixture<StoryPointDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StoryPointDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StoryPointDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
