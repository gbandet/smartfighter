import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'sf-playoffs',
  templateUrl: './playoffs.component.html',
  styleUrls: ['./playoffs.component.css'],
})
export class PlayoffsComponent implements OnInit {
  @Input() playoffs: any;

  constructor() {}

  ngOnInit() {}

  getRoundName(round:number, count:number): string {
    if (round === count) {
      return 'Final';
    } else if (round === count - 1) {
      return 'Semifinals';
    } else if (round === count - 2) {
      return 'Quarterfinals';
    }
    return 'Round ' + round;
  }
}
