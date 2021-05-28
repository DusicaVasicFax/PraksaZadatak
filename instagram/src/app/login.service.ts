import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/core/entities/user';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private instagramUrl = 'https://localhost:44395/User/register';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  result: number;
  constructor(private http: HttpClient) {
    this.result = 0;
  }

  registerUser(u: User) {
    this.http
      .post<any>(this.instagramUrl, u, { observe: 'response' })
      .subscribe((response) => {
        this.result = response.status;
      });
    return this.result;
  }
}
