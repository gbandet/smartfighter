import { Component, OnInit } from '@angular/core';

import { Season } from './season';
import { SeasonService } from './season.service';
import { TitleService } from '../title.service';

@Component({
  selector: 'sf-season-list',
  templateUrl: './season-list.component.html',
})
export class SeasonListComponent implements OnInit {
  seasons: Season[] = [];
  loading = true;
  error: any = null;

  constructor(
    private seasonService: SeasonService,
    private titleService: TitleService,
  ) {}

  ngOnInit() {
    this.titleService.setTitle('Seasons');
    this.seasonService.getSeasons().subscribe(
      page => {
        this.seasons = page.data;
        this.loading = false;
      },
      error => {
        this.error = true;
        this.loading = false;
      });
  }
}
