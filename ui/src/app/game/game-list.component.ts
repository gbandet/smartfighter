import { Component, Input, OnInit } from '@angular/core';

import { Game } from './game';

@Component({
  selector: 'game-list',
  templateUrl: './game-list.component.html',
})
export class GameListComponent implements OnInit {
  @Input() games: Game[];

  constructor() { }

  ngOnInit() {}
}
