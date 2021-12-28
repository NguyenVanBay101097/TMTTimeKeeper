import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

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
    let storeName = val.storeName.trim();
    let url = 'https://' + storeName + '.tdental.dev/api/account/login';
    return this.http.post(url,val);
    // let storeName = val.storeName.trim();
    // let url = 'https://' + storeName + '.tdental.dev/api/account/login';
    // this.http.post(url,val).subscribe((result: any) => {
    //   localStorage.setItem('access_token', result.token);
    //   localStorage.setItem('refresh_token', result.refreshToken);
    //   localStorage.setItem('url', url);
    // });

    // return this.performLogin(val).pipe(
    //   mergeMap((result: any) => {
    //     if (result.succeeded) {
    //           localStorage.setItem('access_token', result.token);
    //           localStorage.setItem('refresh_token', result.refreshToken);
    //           let url = 'https://' + val.storeName.trim() + '.tdental.dev/api/account/login';
    //           localStorage.setItem('url', url);
    //     } else {
    //         return of(result);
    //     }
    // })
    // )
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('url');
  }
}

