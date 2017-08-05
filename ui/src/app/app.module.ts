import { NgModule } from '@angular/core';
import { HttpModule }    from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BsDropdownModule } from 'ngx-bootstrap';

import { RoutingModule } from './routing.module';

import { AppComponent } from './app.component';
import { GameService } from './game/game.service';
import { GameListComponent } from './game/game-list.component';
import { RoundStatusClassPipe } from './game/pipes';
import { UnrankedComponent } from './game/unranked.component';
import { IndexComponent } from './index.component';
import { InstructionsComponent } from './instructions.component';
import { PlayerComponent } from './player/player.component';
import { PlayerService } from './player/player.service';
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
    PlayerComponent,
    PlayoffsComponent,
    RoundStatusClassPipe,
    SeasonComponent,
    SeasonListComponent,
    UnrankedComponent,
  ],
  imports: [
    BrowserModule,
    BsDropdownModule.forRoot(),
    HttpModule,
    RoutingModule,
  ],
  providers: [
    GameService,
    PlayerService,
    SeasonService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
