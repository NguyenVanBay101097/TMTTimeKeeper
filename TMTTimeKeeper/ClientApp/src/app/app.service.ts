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
    return this.http.post(this.baseApi + this.apiUrl, val);
  }

  getTimeKeepers() {
    return this.http.get(this.baseApi + this.apiUrl);
  }

  getTimeKeeperById(id) {
    return this.http.get(this.baseApi + this.apiUrl + '/' + id);
  }

  update(id, val: any) {
    return this.http.put(this.baseApi + this.apiUrl + '/' + id, val);
  }

  delete(id) {
    return this.http.delete(this.baseApi + this.apiUrl + '/' + id);
  }
}
