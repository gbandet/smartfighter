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
  stats: any;
  seasonParam: string;

  constructor(
    private playerService: PlayerService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.route.paramMap
      .switchMap((params: ParamMap) => this.playerService.getPlayer(params.get('name')))
      .subscribe(player => this.player = player);

    Observable.combineLatest(this.route.paramMap, this.route.queryParamMap)
      .switchMap(([params, queryParams]: [ParamMap, ParamMap]) => {
        this.seasonParam = queryParams.get('season');
        return this.playerService.getPlayerStats(params.get('name'), queryParams.get('season'));
      })
      .subscribe(stats => this.stats = stats);
  }
}
