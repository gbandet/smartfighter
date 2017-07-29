import { Component, OnInit } from '@angular/core';

import { Season } from './season';
import { SeasonService } from './season.service';

@Component({
  selector: 'season-list',
  templateUrl: './season-list.component.html',
})
export class SeasonListComponent implements OnInit {
  seasons: Season[];

  constructor(private seasonService: SeasonService) {}

  ngOnInit() {
    this.seasonService.getSeasons().then(seasons => this.seasons = seasons);
  }
}
