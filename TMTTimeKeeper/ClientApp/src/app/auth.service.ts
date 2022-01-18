import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = 'api/Accounts';
  constructor(
    private http: HttpClient,
    @Inject('BASE_API') private baseApi: string
  ) { 
  }
  isAuthenticated() {
    const token = localStorage.getItem('access_token');
    if (!token) {
        return false;
    }

    return true;
  }

  performLogin(val: any): Observable<any> {
    let storeName = val.storeName.trim();
    let url = 'https://' + storeName + '.tdental.dev/api/account/login';
    return this.http.post<any>(url, val);
}

  login(val) {
    return this.http.post(this.baseApi + this.apiUrl + '/login', val);
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('url');
  }
}

