import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';

import { Season } from './season/season';
import { SeasonService } from './season/season.service';
import { TitleService } from './title.service';

@Component({
  selector: 'sf-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'SmartFighter';
  seasons: Season[];

  constructor(
    private router: Router,
    private seasonService: SeasonService,
    private titleService: TitleService,
  ) {}

  ngOnInit() {
    this.seasonService.getSeasons({limit: 5}).subscribe(page => this.seasons = page.data);
    this.router.events
      .filter(event => event instanceof NavigationStart)
      .subscribe(event => this.titleService.setTitle());
  }
}
