import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { Season, RankingPlayer } from './season';
import { SeasonService } from './season.service';

@Component({
  selector: 'season',
  templateUrl: './season.component.html',
})
export class SeasonComponent implements OnInit {
  season: Season = new Season();
  ranking: RankingPlayer[] = [];
  placement: RankingPlayer[] = [];

  constructor(
    private seasonService: SeasonService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.route.paramMap
      .switchMap((params: ParamMap) => this.seasonService.getSeason(+params.get('id')))
      .subscribe(season => this.season = season);
    this.route.paramMap
      .switchMap((params: ParamMap) => this.seasonService.getSeasonRanking(+params.get('id')))
      .subscribe(ranking => {
        this.ranking = ranking.ranking;
        this.placement = ranking.placement;
      });
  }

  isSeasonFinished(season: Season) {
    if (!season.end_date) {
      return false;
    }
    return new Date(season.end_date) < new Date();
  }
}
