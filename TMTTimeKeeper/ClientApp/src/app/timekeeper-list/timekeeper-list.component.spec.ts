import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimekeeperListComponent } from './timekeeper-list.component';

describe('TimekeeperListComponent', () => {
  let component: TimekeeperListComponent;
  let fixture: ComponentFixture<TimekeeperListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimekeeperListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimekeeperListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
