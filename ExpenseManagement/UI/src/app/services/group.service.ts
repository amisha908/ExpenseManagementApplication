import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Group, Member } from '../models/group.model';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  baseApiUrl:string =environment.baseApiUrl;

  constructor(private http:HttpClient) { }
  getAllGroup():Observable<Group[]>
  {
    return this.http.get<Group[]>(this.baseApiUrl + '/api/Group')

  }
  addGroup(groupObj : Group) : Observable<Group>{
    return this.http.post<Group>(this.baseApiUrl + '/api/Group/CreateGroup', groupObj)
  }
  getMember(groupId: string): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.baseApiUrl}/api/Member/member/${groupId}`);
  }
  deleteGroup(groupId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseApiUrl}/api/Group/${groupId}`);
  }
  

}
