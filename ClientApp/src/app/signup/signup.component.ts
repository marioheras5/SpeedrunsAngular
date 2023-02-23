import { Component, Inject, EventEmitter, Output } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }
  signupClick() {
    const userElement = document.getElementsByClassName('username')[0] as HTMLInputElement;
    const passElement = document.getElementsByClassName('password')[0] as HTMLInputElement;

    var params = new HttpParams().set('username', userElement.value).set('password', passElement.value);
    this.http.post<any>(this.baseUrl + 'api/login/signup', params).subscribe(result => {
      console.log(result);
      if (result.statusCode == 200) {
        this.success();
      } else {
        this.mostrarError(result.statusCode);
      }
    }, error => {
      this.mostrarError(error);
    });
  }
  success() {
    const userElement = document.getElementsByClassName('username')[0] as HTMLInputElement;
    localStorage.setItem('token', userElement.value);
    this.router.navigate(['/']);
  }
  mostrarError(result: number) {
    var error = document.getElementsByClassName('error')[0] as HTMLDivElement;
    if (result == 400) {
      error.textContent = "Error: Ya existe este usuario";
    } else {
      error.textContent = "Error: Ha ocurrido un error al registrarse";
    }
  }
}
