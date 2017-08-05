import { Component, OnInit } from '@angular/core';

import { Season } from './season';
import { SeasonService } from './season.service';

@Component({
  selector: 'season-list',
  templateUrl: './season-list.component.html',
})
export class SeasonListComponent implements OnInit {
  seasons: Season[];
  loading: boolean = true;
  error: any = null;

  constructor(private seasonService: SeasonService) {}

  ngOnInit() {
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
