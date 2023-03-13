import { Component, Inject, EventEmitter, Output, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';

@Component({
  selector: 'app-speedruns',
  templateUrl: './speedruns.component.html',
  styleUrls: ['./speedruns.component.css']
})
export class SpeedrunsComponent implements OnInit {
  public timeout = 0 as any;
  public speedruns: Speedruns[] = [];
  public offset: number = 0;
  public len: number = 20;
  public gamename: string = '';
  public game: Game = { id: 0, name: '', shortName: '', img: '' };
  public categories: string[] = [];
  public currentCategory: string;

  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }
  getGame() {
    var params = new HttpParams().set('shortName', this.gamename);
    this.http.get<Game>(this.baseUrl + 'api/game/GetGame', { params: params }).subscribe(result => {
      this.game = result;
    }, error => console.log(error));
  }
  changeCategory(event: Event) {
    var elemento = event.target as HTMLAnchorElement || event.srcElement as HTMLAnchorElement || event.currentTarget as HTMLAnchorElement;
    this.currentCategory = elemento.innerHTML;
    this.getSpeedruns();
  }
  getSpeedruns() {
    var params = new HttpParams().set('shortName', this.gamename).set('category', this.currentCategory).set('offset', this.offset.toString()).set('len', this.len.toString());
    this.http.get<Speedruns[]>(this.baseUrl + 'api/speedrun/GetSpeedruns', { params: params }).subscribe(result => {
      this.speedruns = result;
    }, error => console.log(error));
  }
  getCategories() {
    var params = new HttpParams().set('shortName', this.gamename);
    this.http.get<string[]>(this.baseUrl + 'api/speedrun/GetCategories', { params: params }).subscribe(result => {
      this.categories = result;
      this.currentCategory = this.categories[0];
      this.getSpeedruns();
    }, error => console.log(error));
  }
  addSpeedrun(event: Event) {
    this.router.navigate(["/addspeedrun"], { queryParams: { name: this.gamename } });
  }
  keyPress(event: KeyboardEvent) {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      var searchtext = document.getElementById('searchtextuser') as HTMLInputElement;
      console.log(searchtext.value);
      var params = new HttpParams().set('shortName', this.gamename).set('category', this.currentCategory).set('offset', this.offset.toString()).set('len', this.len.toString()).set('search', searchtext.value);
      this.http.get<Speedruns[]>(this.baseUrl + 'api/speedrun/GetSpeedruns', { params: params }).subscribe(result => {
        this.speedruns = result;
      }, error => console.log(error));
    }, 500);
  }
  ngOnInit() {
    this.route.queryParams.subscribe(
      params => {
        this.gamename = params.name;
      }
    )
    this.getGame();
    this.getCategories();
  }
}
interface Game {
  id: number;
  name: string;
  shortName: string;
  img: string;
}
interface Speedruns {
  id: number;
  position: number;
  username: string;
  country: string;
  time: Time;
  date: Date;
  platform: string;
  category: string;
}
