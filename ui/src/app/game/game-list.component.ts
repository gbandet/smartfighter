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
        return 'badge-default';
      case 2:
        return 'badge-success';
      case 3:
        return 'badge-warning';
      case 4:
        return 'badge-primary';
      case 5:
        return 'badge-default';
      case 6:
        return 'badge-danger';
      case 7:
        return 'badge-default';
      case 8:
        return 'badge-info';
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
