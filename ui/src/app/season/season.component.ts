import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { Game } from '../game/game';
import { Season, RankingPlayer } from './season';
import { SeasonService } from './season.service';
import { TitleService } from '../title.service';

@Component({
  selector: 'sf-season',
  templateUrl: './season.component.html',
})
export class SeasonComponent implements OnInit {
  season: Season = new Season();
  ranking: RankingPlayer[] = [];
  placement: RankingPlayer[] = [];
  games: Game[] = [];
  loading: any = {ranking: true, games: true};
  error: any = {ranking: null, games: null};

  constructor(
    private seasonService: SeasonService,
    private route: ActivatedRoute,
    private titleService: TitleService,
  ) {}

  ngOnInit() {
    this.route.paramMap
      .switchMap((params: ParamMap) => this.seasonService.getSeason(+params.get('id')))
      .subscribe(
        season => {
          this.season = season;
          this.titleService.setTitle(season.name);
        },
        error => this.season = new Season());
    this.route.paramMap
      .switchMap((params: ParamMap) => {
        this.loading.ranking = true;
        this.error.ranking = null;
        return this.seasonService.getSeasonRanking(+params.get('id'));
      })
      .subscribe(
        ranking => {
          this.ranking = ranking.ranking;
          this.placement = ranking.placement;
          this.loading.ranking = false;
        },
        error => {
          this.error.ranking = error;
          this.loading.ranking = false;
        });
    this.route.paramMap
      .switchMap((params: ParamMap) => {
        this.loading.games = true;
        this.error.games = null;
        return this.seasonService.getSeasonGames(+params.get('id'), {limit: 20});
      })
      .subscribe(
        page => {
          this.games = page.data;
          this.loading.games = false;
        },
        error => {
          this.error.games = error;
          this.loading.games = false;
        });
  }

  isSeasonFinished(season: Season) {
    if (!season.end_date) {
      return false;
    }
    return new Date(season.end_date) < new Date();
  }
}
