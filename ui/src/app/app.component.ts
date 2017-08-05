import { Component } from '@angular/core';

import { Season } from './season/season';
import { SeasonService } from './season/season.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'SmartFighter';
  seasons: Season[];

  constructor(private seasonService: SeasonService) {}

  ngOnInit() {
    this.seasonService.getSeasons({limit: 5}).then(page => this.seasons = page.data);
  }
}
