import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Player } from './player';

@Injectable()
export class PlayerService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/players';

  constructor(private http: Http) { }

  getPlayer(name: string): Promise<Player> {
    const url = `${this.url}/${name}`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json() as Player)
      .catch(this.handleError);
  }

  getPlayerStats(name: string, season: string) : Promise<any> {
    let url = `${this.url}/${name}/stats`;
    if (season) {
      url = `${url}?season=${season}`;
    }
    return this.http.get(url)
      .toPromise()
      .then(response => response.json())
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
