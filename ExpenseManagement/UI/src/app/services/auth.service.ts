import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';

export interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44377';
  private tokenKey = 'token';
  private userNameSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  public userName$ = this.userNameSubject.asObservable();

  constructor(private http: HttpClient) {
    const storedToken = this.getToken();
    if (storedToken) {
      this.storeUserName(storedToken);
    }
  }

  login(email: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/api/Auth/login`, { email, password }).pipe(
      tap(response => {
        this.storeToken(response.token);
        this.storeUserName(response.token);
        
      }),
      catchError(error => {
        console.error('Login failed:', error);
        throw error;
      })
    );
  }

  private storeUserName(token: string): void {
    try {
      const decoded: any = jwtDecode(token);
      const userName = decoded.userName || decoded.sub; // Adjust according to your JWT structure
      this.userNameSubject.next(userName);
    } catch (error) {
      console.error('Error decoding token for user name:', error);
      this.userNameSubject.next(null);
    }
  }
  getUserRole(): string | null {
    const token = this.getToken();
    if (token) {
      try {
        const decoded: any = jwtDecode(token);
        return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      } catch (error) {
        console.error('Error decoding token for role:', error);
        return null;
      }
    }
    return null;
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.userNameSubject.next(null);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  private storeToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }
  getUserId(): string | null {
        const token = this.getToken();
        console.log(token);
        if (token) {
          try {
            const decoded: any = jwtDecode(token); // Decode the token
            console.log(decoded.userId)
            console.log(decoded.sub)
            this.userName$=decoded.sub;
            return decoded.userId; // Extract user ID from decoded token
    
          } catch (error) {
            console.error('Error decoding token:', error);
            return null; // Return null if decoding fails
          }
        }
        return null; // Return null if no token
      }

  getUserName(): string | null {
    return this.userNameSubject.value;
  }
}


// // src/app/services/auth.service.ts

// import { Injectable } from '@angular/core';
// import { HttpClient } from '@angular/common/http';
// import { Observable, of } from 'rxjs';
// import { tap, catchError } from 'rxjs/operators';
// import { jwtDecode } from 'jwt-decode';

// export interface LoginResponse {
//   token: string;
//   // Define other properties as needed
// }

// @Injectable({
//   providedIn: 'root'
// })
// export class AuthService {
//   private apiUrl = 'https://localhost:44377'; // Replace with your API URL
//   private tokenKey = 'token'; // Key used to store token in localStorage
//   private userNameKey = ''; // Key used to store user name in localStorage

//   constructor(private http: HttpClient) {}

//   login(email: string, password: string): Observable<LoginResponse> {
//     return this.http.post<LoginResponse>(`${this.apiUrl}/api/Auth/login`, { email, password }).pipe(
//       tap(response => {
//         this.storeToken(response.token); // Store token in localStorage
//         this.storeUserName(response.token); // Store user name in localStorage
//       }),
//       catchError(error => {
//         // Handle login errors here (e.g., log or display error)
//         console.error('Login failed:', error);
//         throw error; // Rethrow or handle as needed
//       })
//     );
//   }
//   private storeUserName(token: string): void {
//     try {
//       const decoded: any = jwtDecode(token); // Decode the token
//       localStorage.setItem(this.userNameKey, decoded.userName); // Store user name in localStorage
//     } catch (error) {
//       console.error('Error decoding token for user name:', error);
//     }
//   }

//   logout(): void {
//     localStorage.removeItem(this.tokenKey); // Remove token from localStorage
//     // Implement any additional cleanup tasks on logout if needed
//   }

//   isLoggedIn(): boolean {
//     return !!this.getToken(); // Check if token exists in localStorage
//   }

//   getToken(): string | null {
//     return localStorage.getItem(this.tokenKey); // Retrieve token from localStorage
//   }

//   getUserId(): string | null {
//     const token = this.getToken();
//     console.log(token);
//     if (token) {
//       try {
//         const decoded: any = jwtDecode(token); // Decode the token
//         console.log(decoded.userId)
//         console.log(decoded.sub)
//         this.userNameKey=decoded.sub;
//         return decoded.userId; // Extract user ID from decoded token

//       } catch (error) {
//         console.error('Error decoding token:', error);
//         return null; // Return null if decoding fails
//       }
//     }
//     return null; // Return null if no token
//   }

//   private storeToken(token: string): void {
//     localStorage.setItem(this.tokenKey, token); // Store token in localStorage
//   }
//   getUserName() {
//     return this.userNameKey // Retrieve user name from localStorage
//   }

// }
