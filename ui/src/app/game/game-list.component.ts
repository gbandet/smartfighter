import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from "@angular/platform-browser";

import { Game } from './game';

@Component({
  selector: 'game-list',
  templateUrl: './game-list.component.html',
})
export class GameListComponent implements OnInit {
  @Input() games: Game[];

  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() {
  }

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

  getStatusLabel(status: number) {
    switch (status) {
      case 1:
        return this.sanitizer.bypassSecurityTrustHtml('&nbsp;');
      case 2:
        return 'V';
      case 3:
        return 'CA';
      case 4:
        return 'EX';
      case 5:
        return 'C';
      case 6:
        return 'P';
      case 7:
        return 'T';
      case 8:
        return 'D';
    }
    return '?';
  }
}
