import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  domain = '';
  loginForm: FormGroup;
  submitted = false;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private http: HttpClient,
    private authService: AuthService
  ) {
    if (authService.isAuthenticated()) {
      this.router.navigate(['/main/timekeeper-list']);
    }
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      storeName: [null, Validators.required],
      userName: [null, Validators.required],
      password: [null, Validators.required],
      rememberMe: false
    })
  }

  onLogin() {
    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }
    let formValue = this.loginForm.value;
    formValue.domainName = this.getDomain();
    this.authService.login(formValue).subscribe((result: any) => {
      if (result.succeeded) {
        if (this.rememberMeValue) {
          localStorage.setItem('access_token', result.token);
          localStorage.setItem('refresh_token', result.refreshToken);
          // let url = 'https://' + formValue.storeName.trim() + '.tdental.dev';
          // localStorage.setItem('url', url);
        }
        this.router.navigate(['/main/timekeeper-list']);
      }
    })
    // this.router.navigate(['/main/timekeeper-list']);
  }

  getDomain() {
    return `https://${this.storeNameValue || ''}${this.domain}`;
  }

  get storeNameValue() {
    return this.loginForm.get('storeName').value;
  }

  get rememberMeValue() {
    return this.loginForm.get('rememberMe').value;
  }
}

