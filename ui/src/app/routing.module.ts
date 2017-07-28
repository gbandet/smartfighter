import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InstructionsComponent }   from './instructions.component';

const routes: Routes = [
    { path: '', redirectTo: '/instructions', pathMatch: 'full' },
    { path: 'instructions', component: InstructionsComponent }
];

@NgModule({
    imports: [ RouterModule.forRoot(routes) ],
    exports: [ RouterModule ]
})
export class RoutingModule {}
