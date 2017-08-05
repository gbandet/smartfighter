import { Component, OnInit } from '@angular/core';

import { Game } from '../game/game';
import { GameService } from './game.service';

@Component({
  selector: 'unranked',
  templateUrl: './unranked.component.html',
})
export class UnrankedComponent implements OnInit {
  games: Game[] = [];
  loading: boolean = true;
  error: any;

  constructor(private gameService: GameService) { }

  ngOnInit() {
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
