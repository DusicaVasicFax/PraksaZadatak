import { Component, OnInit } from '@angular/core';

import { FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from 'src/core/entities/user';
import { LoginService } from '../login.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  frmGroup: FormGroup;
  title = 'dropzone';
  files: File[] = [];
  user: User;
  constructor(
    private LoginService: LoginService,
    private router: Router,
    private http: HttpClient
  ) {
    this.frmGroup = new FormGroup({
      email: new FormControl(null, Validators.required),
      firstname: new FormControl(null, Validators.required),
      lastname: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      confirmPassword: new FormControl(null, Validators.required),
    });
    this.user = new User('', '', '', '', '', '', '', '');
  }

  ngOnInit(): void {}

  onSelect(event) {
    console.log(event);
    this.files.push(...event.addedFiles);

    const formData = new FormData();

    for (var i = 0; i < this.files.length; i++) {
      formData.append('file[]', this.files[i]);
    }

    this.http
      .post('http://localhost:8001/upload.php', formData)
      .subscribe((res) => {
        console.log(res);
        alert('Uploaded Successfully.');
      });
  }

  onRemove(event) {
    console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
  }
  onSubmit() {
    this.user = this.frmGroup.value;
    this.user.username = (<HTMLInputElement>(
      document.getElementById('email')
    )).value;
    this.user.phoneNumber = (<HTMLInputElement>(
      document.getElementById('phoneNumber')
    )).value;

    let status = this.LoginService.registerUser(this.user);
    if (status === 200) {
      this.router.navigate(['/', 'login']);
    } else {
      this.router.navigate(['/', 'register']);
    }
  }
}
