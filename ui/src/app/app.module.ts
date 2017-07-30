import { NgModule } from '@angular/core';
import { HttpModule }    from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { RoutingModule } from './routing.module';

import { AppComponent } from './app.component';
import { InstructionsComponent } from './instructions.component';
import { PlayoffsComponent } from './season/playoffs.component';
import { SeasonComponent } from './season/season.component';
import { SeasonListComponent } from './season/season-list.component';
import { SeasonService } from './season/season.service';

@NgModule({
  declarations: [
    AppComponent,
    InstructionsComponent,
    PlayoffsComponent,
    SeasonComponent,
    SeasonListComponent,
  ],
  imports: [
    BrowserModule,
    HttpModule,
    NgbModule.forRoot(),
    RoutingModule,
  ],
  providers: [SeasonService],
  bootstrap: [AppComponent],
})
export class AppModule { }
