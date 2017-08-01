import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { SeasonService } from './season/season.service';

@Component({
  selector: 'index',
  templateUrl: './index.component.html',
})
export class IndexComponent {
  constructor(
    private seasonService: SeasonService,
    private router: Router,
  ) {}

  ngOnInit() {
    this.seasonService.getSeasons().then(seasons => {
      if (seasons) {
        console.log(seasons);
        this.router.navigate(['/seasons', seasons[seasons.length - 1].id])
      } else {
        this.router.navigate(['/instructions'])
      }
    });
  }
}
