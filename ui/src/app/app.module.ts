import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { RoutingModule } from './routing.module';

import { AppComponent } from './app.component';
import { InstructionsComponent } from './instructions.component';

@NgModule({
  declarations: [
    AppComponent,
    InstructionsComponent,
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    RoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
