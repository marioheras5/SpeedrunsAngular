import { Component, Inject, EventEmitter , Output} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }
  loginClick() {
    const userElement = document.getElementsByClassName('username')[0] as HTMLInputElement;
    const passElement = document.getElementsByClassName('password')[0] as HTMLInputElement;

    var params = new HttpParams().set('username', userElement.value).set('password', passElement.value);
    this.http.post<any>(this.baseUrl + 'api/login/login', params).subscribe(result => {
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
      error.textContent = "Error: No existe ese usuario";
    } else if (result == 404) {
      error.textContent = "Error: Contraseña incorrecta";
    } else {
      error.textContent = "Error: Ha ocurrido un error al iniciar sesión";
    }
  }
}
