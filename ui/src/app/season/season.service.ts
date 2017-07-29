import { Injectable }    from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Season } from './season';

@Injectable()
export class SeasonService {
  private headers = new Headers({'Content-Type': 'application/json'});
  private url = '/ui/seasons/';

  constructor(private http: Http) { }

  getSeasons(): Promise<Season[]> {
    console.log('getSeasons');
    return this.http.get(this.url)
      .toPromise()
    .then(response => response.json() as Season[])
      .catch(this.handleError);
  }

  getSeason(id: number): Promise<Season> {
    const url = `${this.url}/${id}`;
    return this.http.get(url)
      .toPromise()
      .then(response => response.json() as Season)
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }
}
