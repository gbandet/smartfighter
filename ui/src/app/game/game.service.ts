import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Game } from './game';
import { Page } from '../paging';

@Injectable()
export class GameService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/games';

  constructor(private http: Http) { }

  getGames(search?: object): Promise<Page<Game>> {
    return this.http.get(this.url, {search: search})
      .toPromise()
      .then(response => new Page<Game>(response.json()))
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
