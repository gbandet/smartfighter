import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import { Observable } from 'rxjs/Observable';

import { Player } from './player';

@Injectable()
export class PlayerService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/players';

  constructor(private http: Http) { }

  getPlayer(name: string): Observable<Player> {
    const url = `${this.url}/${name}`;
    return this.http.get(url)
      .map(response => response.json() as Player);
  }

  getPlayerStats(name: string, search?: object): Observable<any> {
    const url = `${this.url}/${name}/stats`;
    return this.http.get(url, {search: search})
      .map(response => response.json());
  }
}
