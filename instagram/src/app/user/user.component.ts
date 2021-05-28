import { Component, OnInit } from '@angular/core';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoggedUser } from 'src/core/entities/loggedUser';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit {
  frmGroup: FormGroup;
  logUser: LoggedUser;
  constructor(
    private UserService: UserService,
    private router: Router,
    private http: HttpClient
  ) {
    this.frmGroup = new FormGroup({
      email: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
    });
    this.logUser = new LoggedUser('', '');
  }
  ngOnInit(): void {}

  onSubmit() {
    this.logUser = this.frmGroup.value;
    this.http
      .post('https://localhost:44395/User/login', this.logUser)
      .subscribe((response) => {
        localStorage.setItem('token', 'Bearer ' + (<any>response).token);
        this.router.navigate(['/', 'dashboard']);
      });
  }
}
