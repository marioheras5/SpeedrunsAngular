import { Component, Inject, EventEmitter, Output, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-speedruns',
  templateUrl: './speedruns.component.html',
  styleUrls: ['./speedruns.component.css']
})
export class SpeedrunsComponent implements OnInit {

  public gamename: string = '';
  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    console.log(this.gamename);
  }
  ngOnInit() {
    this.route.queryParams.subscribe(
      params => {
        this.gamename = params['name'];
      }
    )
  }
}
