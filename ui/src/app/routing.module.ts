import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { IndexComponent }   from './index.component';
import { InstructionsComponent }   from './instructions.component';
import { UnrankedComponent } from './game/unranked.component';
import { PlayerComponent } from './player/player.component';
import { SeasonComponent } from './season/season.component';
import { SeasonListComponent } from './season/season-list.component';

const routes: Routes = [
    { path: '', component: IndexComponent, pathMatch: 'full'  },
    { path: 'instructions', component: InstructionsComponent },
    { path: 'players/:name', component: PlayerComponent },
    { path: 'seasons/:id', component: SeasonComponent },
    { path: 'seasons', component: SeasonListComponent },
    { path: 'unranked', component: UnrankedComponent },
];

@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule ]
})
export class RoutingModule {}
