import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'playoffs',
  templateUrl: './playoffs.component.html',
  styleUrls: ['./playoffs.component.css'],
})
export class PlayoffsComponent implements OnInit {
  @Input('data') playoffs: any;

  constructor() {}

  ngOnInit() {}
}
