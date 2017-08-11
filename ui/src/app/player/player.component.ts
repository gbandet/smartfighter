import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import {Observable} from 'rxjs/Observable';
import 'rxjs/add/observable/combineLatest';
import 'rxjs/add/operator/switchMap';

import { Player } from './player';
import { PlayerService } from './player.service';
import { TitleService } from '../title.service';

@Component({
  selector: 'sf-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css'],
})
export class PlayerComponent implements OnInit {
  player: Player = new Player();
  stats: any = {};
  opponentCharacters: any[] = [];
  seasonParam = '';
  loading = {player: true, stats: true};
  error = {player: null, stats: null};

  constructor(
    private playerService: PlayerService,
    private route: ActivatedRoute,
    private titleService: TitleService,
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
          this.titleService.setTitle(player.name);
        },
        error => {
          this.error.player = error;
          this.loading.player = false;
        });

    Observable.combineLatest(this.route.paramMap, this.route.queryParamMap)
      .switchMap(([params, queryParams]: [ParamMap, ParamMap]) => {
        this.loading.stats = true;
        this.error.stats = null;
        const search: any = {};
        this.seasonParam = queryParams.get('season');
        if (this.seasonParam) {
          search.season = this.seasonParam;
        }
        return this.playerService.getPlayerStats(params.get('name'), search);
      })
      .subscribe(
        stats => {
          this.stats = stats;
          this.opponentCharacters = this.getOpponentList(stats.characters);
          this.loading.stats = false;
        },
        error => {
          this.error.stats = error;
          this.loading.stats = false;
        });
  }

  getOpponentList(characters: any): any[] {
    const opponents: any = {};
    for (const character of characters) {
      for (const code of Object.keys(character.opponents)) {
        opponents[code] = {
          code: code,
          name: character.opponents[code].name,
        };
      }
    }
    const list = [];
    for (const code of Object.keys(opponents)) {
      list.push(opponents[code]);
    }
    return list.sort((a, b) => a.name < b.name ? -1 : (a.name === b.name ? 0 : 1));
  }
}
