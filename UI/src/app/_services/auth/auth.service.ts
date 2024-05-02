import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../../_models/auth/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  private currentuserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentuserSource.asObservable();
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'Auth/login', model).pipe(
      //UsingRxjs
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }
  setCurrentUser(user: User) {
    user.roles  = [];
    const roles = this.getDecodedToken(user.jwtToken).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentuserSource.next(user);

  }
  logout() {
    localStorage.removeItem('user');
    this.currentuserSource.next(null);
  }
  getDecodedToken(token:string){
    return JSON.parse(atob(token.split('.')[1]));
  }
  register(model: User) {
    return this.http.post<User>(this.baseUrl + 'Auth/register', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }
}
