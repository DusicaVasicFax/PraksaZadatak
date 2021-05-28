import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/core/entities/user';

import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  constructor(private router: Router, private http: HttpClient) {}

  ngOnInit(): void {}

  logout() {
    let headers = new HttpHeaders()
      .set('Authorization', localStorage.getItem('token') || '')
      .set('Content-Type', 'application/json');
    this.http
      .post('https://localhost:44395/User/logout', null, { headers })
      .subscribe((response) => {
        localStorage.removeItem('token');
        this.router.navigate(['/', 'login']);
      });
  }

  /*showProfile()
  {
    document.getElementById('profile').style.display = "block";
    let headers = new HttpHeaders()
    .set('Authorization', localStorage.getItem('token') || '')
    .set('Content-Type', 'application/json');
    return this.http.get<User>('https://localhost:44395/User/user/', { headers }).subscribe(data =>data);
  }*/
}
