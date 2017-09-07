import { Component, OnInit } from '@angular/core';

import { TitleService } from './title.service';

@Component({
    selector: 'sf-instructions',
    templateUrl: './instructions.component.html',
})
export class InstructionsComponent implements OnInit {
  constructor(private titleService: TitleService) {}

  ngOnInit() {
    this.titleService.setTitle('Instructions');
  }
}
