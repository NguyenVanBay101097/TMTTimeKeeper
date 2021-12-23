import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimekeeperDataComponent } from './timekeeper-data.component';

describe('TimekeeperDataComponent', () => {
  let component: TimekeeperDataComponent;
  let fixture: ComponentFixture<TimekeeperDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimekeeperDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimekeeperDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
