import { Component, OnInit } from '@angular/core';

import { Season } from './season/season';
import { SeasonService } from './season/season.service';

@Component({
  selector: 'sf-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'SmartFighter';
  seasons: Season[];

  constructor(private seasonService: SeasonService) {}

  ngOnInit() {
    this.seasonService.getSeasons({limit: 5}).subscribe(page => this.seasons = page.data);
  }
}
