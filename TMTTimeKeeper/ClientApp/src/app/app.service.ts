import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  apiUrl = 'api/TimeKeepers';
  constructor(
    private http: HttpClient,
    @Inject('BASE_API') private baseApi: string
  ) { 
  }
  connect(val: any){
    return this.http.post(this.baseApi + this.apiUrl + '/connect', val);
  }
}
