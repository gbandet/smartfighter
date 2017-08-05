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
    this.seasonService.getSeasons({limit: 1}).subscribe(page => {
      if (page.data) {
        this.router.navigate(['/seasons', page.data[0].id])
      } else {
        this.router.navigate(['/instructions'])
      }
    });
  }
}
