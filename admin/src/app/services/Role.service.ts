// role.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private apiUrl = 'https://localhost:7057/api/roles';

  constructor(private http: HttpClient) { }

  getRoles(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }

  createRole(roleName: string): Observable<any> {
    return this.http.post<any>(this.apiUrl, { roleName });
  }

  getUsers(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/users`);
  }

  assignRole(userId: string, roleName: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/assignRole`, { userId, roleName });
  }

  removeRole(userId: string, roleName: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/removeRole`, { userId, roleName });
  }
}
