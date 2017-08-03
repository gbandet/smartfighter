import { Component, OnInit } from '@angular/core';

import { Game } from '../game/game';
import { GameService } from './game.service';

@Component({
  selector: 'unranked',
  templateUrl: './unranked.component.html',
})
export class UnrankedComponent implements OnInit {
  games: Game[] = [];

  constructor(private gameService: GameService) { }

  ngOnInit() {
    this.gameService.getGames().then(page => this.games = page.data);
  }
}
