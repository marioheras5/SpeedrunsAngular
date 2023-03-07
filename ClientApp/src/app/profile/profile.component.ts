import { Component, Inject, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {
  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }
  addGame() {
    const nameElement = document.getElementsByClassName('name')[0] as HTMLInputElement;
    const shortNameElement = document.getElementsByClassName('shortname')[0] as HTMLInputElement;
    const imgElement = document.getElementsByClassName('img')[0] as HTMLInputElement;
    const imgFile = imgElement.files?.item(0) as File;

    var formData = new FormData();
    formData.append('name', nameElement.value);
    formData.append('shortName', shortNameElement.value);
    formData.append('img', imgFile);
    this.http.post<boolean>(this.baseUrl + 'api/game/AddGame', formData).subscribe(result => {
      console.log(result);
      if (result) {
        this.router.navigate(['/']);
      }
    }, error => {
      console.log(error);
    });
  }
  removeGame() {
    const idElement = document.getElementsByClassName('id')[0] as HTMLInputElement;

    var formData = new FormData();
    formData.append('id', idElement.value);
    this.http.post<boolean>(this.baseUrl + 'api/game/RemoveGame', formData).subscribe(result => {
      console.log(result);
      if (result) {
        this.router.navigate(['/']);
      }
    }, error => {
      console.log(error);
    });
  }
}
