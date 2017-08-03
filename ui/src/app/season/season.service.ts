import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Game } from '../game/game';
import { Page } from '../paging';
import { Season, SeasonRanking } from './season';

@Injectable()
export class SeasonService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/seasons';

  constructor(private http: Http) { }

  getSeasons(): Promise<Page<Season>> {
    return this.http.get(this.url)
      .toPromise()
      .then(response => new Page<Season>(response.json()))
      .catch(this.handleError);
  }

  getSeason(id: number): Promise<Season> {
    const url = `${this.url}/${id}`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json() as Season)
      .catch(this.handleError);
  }

  getSeasonRanking(id: number): Promise<SeasonRanking> {
    const url = `${this.url}/${id}/ranking`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json() as SeasonRanking)
      .catch(this.handleError);
  }

  getSeasonGames(id: number): Promise<Page<Game>> {
    const url = `${this.url}/${id}/games`;
    return this.http.get(url)
      .toPromise()
      .then(response => new Page<Game>(response.json()))
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
