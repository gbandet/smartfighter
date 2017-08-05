import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import { Observable } from 'rxjs/Observable';

import { Game } from './game';
import { Page } from '../paging';

@Injectable()
export class GameService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/games';

  constructor(private http: Http) { }

  getGames(search?: object): Observable<Page<Game>> {
    return this.http.get(this.url, {search: search})
      .map(response => new Page<Game>(response.json()));
  }
}
