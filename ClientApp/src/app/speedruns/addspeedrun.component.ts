import { Component, Inject, EventEmitter, Output, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Time } from '@angular/common';

@Component({
  selector: 'app-addspeedrun',
  templateUrl: './addspeedrun.component.html',
  styleUrls: ['./addspeedrun.component.css']
})
export class AddSpeedrunComponent implements OnInit {
  public gamename: string = '';
  public username: string = '';
  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }
  addSpeedrun() {
    const country = document.getElementsByClassName('country')[0] as HTMLInputElement;
    const time = document.getElementsByClassName('time')[0] as HTMLInputElement;
    const platform = document.getElementById('platform') as HTMLSelectElement;
    const category = document.getElementById('category') as HTMLSelectElement;
    var formData = new FormData();
    formData.append('username', this.username);
    formData.append('shortName', this.gamename);
    formData.append('country', country.value);
    formData.append('time', time.value);
    formData.append('date', new Date().toISOString());
    formData.append('platform', platform.value);
    formData.append('category', category.value);
    this.http.post<boolean>(this.baseUrl + 'api/speedrun/AddSpeedrun', formData).subscribe(result => {
      console.log(result);
      if (result) {
        this.router.navigate(["/speedruns"], { queryParams: { name: this.gamename } });
      }
    }, error => {
      console.log(error);
    });
  }
  ngOnInit() {
    this.route.queryParams.subscribe(
      params => {
        this.gamename = params.name;
      }
    )
    this.username = localStorage.getItem('token') || '';
  }
}
