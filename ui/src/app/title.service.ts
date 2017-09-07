import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable()
export class TitleService extends Title {
  setTitle(newTitle?: string) {
    if (newTitle) {
      super.setTitle(`${newTitle} - SmartFighter`);
    } else {
      super.setTitle('SmartFighter');
    }
  }
}
