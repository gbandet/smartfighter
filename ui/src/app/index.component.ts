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
    this.seasonService.getSeasons().then(page => {
      if (page.data) {
        this.router.navigate(['/seasons', page.data[page.data.length - 1].id])
      } else {
        this.router.navigate(['/instructions'])
      }
    });
  }
}
