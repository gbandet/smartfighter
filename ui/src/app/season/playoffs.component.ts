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
}
