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
  public speedruns: Speedruns[] = [];
  public offset: number = 0;
  public len: number = 20;
  public gamename: string = '';
  public game: Game = { id: 0, name: '', shortName: '', img: '' };
  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }
  getGame() {
    var params = new HttpParams().set('shortName', this.gamename);
    this.http.get<Game>(this.baseUrl + 'api/game/GetGame', { params: params }).subscribe(result => {
      this.game = result;
    }, error => console.log(error));
  }
  getSpeedruns() {
    var params = new HttpParams().set('shortName', this.gamename).set('offset', this.offset.toString()).set('len', this.len.toString());
    this.http.get<Speedruns[]>(this.baseUrl + 'api/speedruns/GetSpeedruns', { params: params }).subscribe(result => {
      this.speedruns = result;
    }, error => console.log(error));
  }
  ngOnInit() {
    this.route.queryParams.subscribe(
      params => {
        this.gamename = params.name;
      }
    )
    this.getGame();
    this.getSpeedruns();
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
  username: string;
  country: string;
  time: Time;
  date: Date;
  platform: string;
  category: string;
}
