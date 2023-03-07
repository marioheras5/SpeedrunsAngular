import { Component, Inject, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public games: Game[] = [];
  public offset: number = 0;
  public len: number = 9;
  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    var params = new HttpParams().set('offset', '0').set('len', '9');
    this.http.get<Game[]>(this.baseUrl + 'api/game/GetGames', { params: params }).subscribe(result => {
      this.games = result;
    }, error => console.log(error));
  }
  getGames() {
    var params = new HttpParams().set('offset', this.offset.toString()).set('len', this.len.toString());
    this.http.get<Game[]>(this.baseUrl + 'api/game/GetGames', { params: params }).subscribe(result => {
      this.games.push(...result);
    }, error => console.log(error));
  }
  onClick(event: Event) {
    var elemento = event.target || event.srcElement || event.currentTarget as any;
    var id = elemento.attributes.id.value;
    this.router.navigate(["/speedruns"], { queryParams: { name: id} });
  }
  
}
interface Game {
  id: number;
  name: string;
  shortName: string;
  img: string;
}
