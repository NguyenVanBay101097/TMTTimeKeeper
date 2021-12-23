import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { TimekeeperListComponent } from './timekeeper-list/timekeeper-list.component';
import { TimekeeperAccountsComponent } from './timekeeper-accounts/timekeeper-accounts.component';
import { TimekeeperDataComponent } from './timekeeper-data/timekeeper-data.component';
import { AppService } from './app.service';
import { TimeKeeperCreateDialogComponent } from './time-keeper-create-dialog/time-keeper-create-dialog.component';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    LoginComponent,
    MainComponent,
    TimekeeperListComponent,
    TimekeeperAccountsComponent,
    TimekeeperDataComponent,
    TimeKeeperCreateDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgbModalModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { 
        path: 'main',
        component: MainComponent,
        children: [
          { path: 'timekeeper-list', component: TimekeeperListComponent},
          { path: 'timekeeper-accounts', component: TimekeeperAccountsComponent },
          { path: 'timekeeper-data', component: TimekeeperDataComponent },
          { path: '', redirectTo: 'timekeeper-list', pathMatch: 'full'},
        ]
      },
    ])
  ],
  providers: [AppService],
  bootstrap: [AppComponent],
  entryComponents: [TimeKeeperCreateDialogComponent]
})
export class AppModule { }
