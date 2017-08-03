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

  getStatusClass(status: number) {
    switch (status) {
      case 1:
        return 'label-default';
      case 2:
        return 'label-success';
      case 3:
        return 'label-warning';
      case 4:
        return 'label-primary';
      case 5:
        return 'label-default';
      case 6:
        return 'label-danger';
      case 7:
        return 'label-default';
      case 8:
        return 'label-info';
    }
    return '';
  }
}
