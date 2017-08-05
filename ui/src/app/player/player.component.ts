import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/switchMap';

import { Player }  from './player';
import { PlayerService } from './player.service';

@Component({
  selector: 'player',
  templateUrl: './player.component.html',
})
export class PlayerComponent implements OnInit {
  player: Player;
  stats: any = {};
  seasonParam: string = '';
  loading: any = {player: true, stats: true};
  error: any = {player: null, stats: null};

  constructor(
    private playerService: PlayerService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.route.paramMap
      .switchMap((params: ParamMap) => {
        this.loading.player = true;
        this.error.player = null;
        return this.playerService.getPlayer(params.get('name'));
      })
      .subscribe(
        player => {
          this.player = player;
          this.loading.player = false;
        },
        error => {
          this.error.player = error;
          this.loading.player = false;
        });

    Observable.combineLatest(this.route.paramMap, this.route.queryParamMap)
      .switchMap(([params, queryParams]: [ParamMap, ParamMap]) => {
        this.loading.stats = true;
        this.error.stats = null;
        let search: any = {};
        this.seasonParam = queryParams.get('season');
        if (this.seasonParam) {
          search.season = this.seasonParam;
        }
        return this.playerService.getPlayerStats(params.get('name'), search);
      })
      .subscribe(
        stats => {
          this.stats = stats;
          this.loading.stats = false;
        },
        error => {
          this.error.stats = error;
          this.loading.stats = false;
        });
  }
}
