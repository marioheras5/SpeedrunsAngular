import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public timeout = 0 as any;
  constructor() {
  }
  isLoggedIn() {
    return localStorage.getItem('token') != null;
  }
  keyPress(event: KeyboardEvent) {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(this.search, 500);
  }
  deslogearse() {
    localStorage.removeItem('token');
  }
  search() {
    const searchtext = document.getElementsByClassName('searchtext')[0] as HTMLInputElement;
    console.log(searchtext.value);
  }
}
