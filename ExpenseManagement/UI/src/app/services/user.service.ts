import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseApiUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseApiUrl}/GetAllUsers`);
  }

  getTotalOwes(userId: string): Observable<number> {
    return this.http.get<number>(`${this.baseApiUrl}/GetUserTotalOwes/${userId}`);
  }

  getTotalOwns(userId: string): Observable<number> {
    return this.http.get<number>(`${this.baseApiUrl}/GetUserTotalOwns/${userId}`);
  }
}


// import { Injectable } from '@angular/core';
// import { environment } from '../../environments/environment.development';
// import { HttpClient } from '@angular/common/http';
// import { Observable } from 'rxjs/internal/Observable';
// import { User } from '../models/user.model';

// @Injectable({
//   providedIn: 'root'
// })
// export class UserService {

//   baseApiUrl : string = environment.baseApiUrl;
// constructor(private http: HttpClient) { }
//   getAllUsers() : Observable<User[]>{
//     return this.http.get<User[]>(this.baseApiUrl + '/GetAllUsers');
//   }
// }
