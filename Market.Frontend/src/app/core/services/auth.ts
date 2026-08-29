import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, finalize, of, tap } from 'rxjs';

import { environment } from '../../../environments/environment';

export interface LoginRequest {
  email: string;
  password: string;
  fingerprint?: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  expiresAtUtc: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = `${environment.apiUrl}/api/auth`;

  private readonly accessTokenKey = 'access_token';
  private readonly refreshTokenKey = 'refresh_token';

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${this.apiUrl}/login`,
      request
    ).pipe(
      tap((response) => {
        localStorage.setItem(
          this.accessTokenKey,
          response.accessToken
        );

        localStorage.setItem(
          this.refreshTokenKey,
          response.refreshToken
        );
      }),
    );
  }

  logout(): Observable<void> {
    const refreshToken = this.getRefreshToken();

    if (!refreshToken) {
      this.clearTokens();
      return of(void 0);
    }

    return this.http
      .post<void>(
        `${this.apiUrl}/logout`,
        { refreshToken }
      )
      .pipe(
        finalize(() => this.clearTokens())
      );
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.refreshTokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getAccessToken();
  }

  isAdmin(): boolean {
    const token = this.getAccessToken();

    if (!token) {
      return false;
    }

    try {
      const payloadPart = token.split('.')[1];

      if (!payloadPart) {
        return false;
      }

      let base64 = payloadPart
        .replace(/-/g, '+')
        .replace(/_/g, '/');

      while (base64.length % 4 !== 0) {
        base64 += '=';
      }

      const payload = JSON.parse(atob(base64));

      return String(payload['is_admin']).toLowerCase() === 'true';
    } catch {
      return false;
    }
  }

  private clearTokens(): void {
    localStorage.removeItem(this.accessTokenKey);
    localStorage.removeItem(this.refreshTokenKey);
  }
}