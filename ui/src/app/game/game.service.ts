import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Game } from './game';

@Injectable()
export class GameService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/games/';

  constructor(private http: Http) { }

  getGames(): Promise<Game[]> {
    return this.http.get(this.url)
      .toPromise()
      .then(response => response.json() as Game[])
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
