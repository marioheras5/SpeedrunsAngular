import { Component } from '@angular/core';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor() {
    this.changeFavicon();
  }
  title = 'app';
  assetDark: string = 'assets/favicon-dark.png';
  assetLight: string = 'assets/favicon.png';

  changeFavicon() {
    var link: any = document.querySelector("link[rel*='icon']");
    if (link == null) return;
    if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
      link.href = this.assetDark;
    } else {
      link.href = this.assetLight;
    }
  }
}
