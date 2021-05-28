import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoggedUser } from 'src/core/entities/loggedUser';
import { Router } from '@angular/router';
import { tokenize } from '@angular/compiler/src/ml_parser/lexer';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  result: number;
  private instagramUrl = 'https://localhost:44395/User/login';
  constructor(private http: HttpClient, private router: Router) {
    this.result = 0;
  }
}
