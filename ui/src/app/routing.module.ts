import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InstructionsComponent }   from './instructions.component';
import { SeasonListComponent } from './season/season-list.component';

const routes: Routes = [
    { path: '', redirectTo: '/instructions', pathMatch: 'full' },
    { path: 'instructions', component: InstructionsComponent },
    { path: 'seasons', component: SeasonListComponent },
];

@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule ]
})
export class RoutingModule {}
