import { Component, OnInit } from '@angular/core';

import { Game } from '../game/game';
import { GameService } from './game.service';
import { TitleService } from '../title.service';

@Component({
  selector: 'sf-unranked',
  templateUrl: './unranked.component.html',
})
export class UnrankedComponent implements OnInit {
  games: Game[] = [];
  loading = true;
  error: any;

  constructor(
    private gameService: GameService,
    private titleService: TitleService,
  ) { }

  ngOnInit() {
    this.titleService.setTitle('Unranked games');
    this.loading = true;
    this.gameService.getGames({phase: 0, limit: 50}).subscribe(
      page => {
        this.games = page.data;
        this.loading = false;
      },
      error => {
        this.error = error;
        this.loading = false;
      });
  }
}
