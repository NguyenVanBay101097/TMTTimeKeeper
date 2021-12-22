import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimekeeperAccountsComponent } from './timekeeper-accounts.component';

describe('TimekeeperAccountsComponent', () => {
  let component: TimekeeperAccountsComponent;
  let fixture: ComponentFixture<TimekeeperAccountsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimekeeperAccountsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimekeeperAccountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
