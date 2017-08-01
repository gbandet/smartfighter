import { NgModule } from '@angular/core';
import { HttpModule }    from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BsDropdownModule } from 'ngx-bootstrap';

import { RoutingModule } from './routing.module';

import { AppComponent } from './app.component';
import { GameListComponent } from './game/game-list.component';
import { IndexComponent } from './index.component';
import { InstructionsComponent } from './instructions.component';
import { PlayoffsComponent } from './season/playoffs.component';
import { SeasonComponent } from './season/season.component';
import { SeasonListComponent } from './season/season-list.component';
import { SeasonService } from './season/season.service';

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    IndexComponent,
    InstructionsComponent,
    PlayoffsComponent,
    SeasonComponent,
    SeasonListComponent,
  ],
  imports: [
    BrowserModule,
    BsDropdownModule.forRoot(),
    HttpModule,
    RoutingModule,
  ],
  providers: [SeasonService],
  bootstrap: [AppComponent],
})
export class AppModule { }
