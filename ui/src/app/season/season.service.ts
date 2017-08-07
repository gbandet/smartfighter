import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import {Observable} from 'rxjs/Observable';

import { Game } from '../game/game';
import { Page } from '../paging';
import { Season, SeasonRanking } from './season';

@Injectable()
export class SeasonService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/seasons';

  constructor(private http: Http) { }

  getSeasons(search?: object): Observable<Page<Season>> {
    return this.http.get(this.url, {search: search})
      .map(response => new Page<Season>(response.json()));
  }

  getSeason(id: number): Observable<Season> {
    const url = `${this.url}/${id}`;
    return this.http.get(url)
      .map(response => response.json() as Season);
  }

  getSeasonRanking(id: number): Observable<SeasonRanking> {
    const url = `${this.url}/${id}/ranking`;
    return this.http.get(url)
      .map(response => response.json() as SeasonRanking);
  }

  getSeasonGames(id: number, search?: object): Observable<Page<Game>> {
    const url = `${this.url}/${id}/games`;
    return this.http.get(url, {search: search})
      .map(response => new Page<Game>(response.json()));
  }
}
