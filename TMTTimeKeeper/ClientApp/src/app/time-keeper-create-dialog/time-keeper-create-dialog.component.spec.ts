import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeKeeperCreateDialogComponent } from './time-keeper-create-dialog.component';

describe('TimeKeeperCreateDialogComponent', () => {
  let component: TimeKeeperCreateDialogComponent;
  let fixture: ComponentFixture<TimeKeeperCreateDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimeKeeperCreateDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeKeeperCreateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
