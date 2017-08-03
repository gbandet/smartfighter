export class Pager {
  count: number;
  next: string;
  previous: string;
}

export class Page<T> {
  pager: Pager = new Pager();
  data: T[] = [];

  constructor(json: any) {
    if (json) {
      this.pager.count = json.count;
      this.pager.next = json.next;
      this.pager.previous = json.previous;
      this.data = json.results as T[];
    }
  }
}
