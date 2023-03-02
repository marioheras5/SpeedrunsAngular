import { Component, Inject, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public games: Game[] = [];
  constructor( private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.games = this.getGames(0, 9);
  }
  getGames(page: number, size: number) {
    var params = new HttpParams().set('page', page.toString()).set('size', size.toString());
    this.http.get<Game[]>(this.baseUrl + 'api/game/GetGames', { params: params }).subscribe(result => {
      return result;
    }, error => console.error(error));
    return [];
  }
}
interface Game {
  id: number;
  name: string;
  shortName: string;
  img: string;
}
