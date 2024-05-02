import { HttpInterceptorFn } from '@angular/common/http';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req);
};

/*

import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from '../services/auth.service';

@Injectable()
export class RefreshTokenInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          // Refresh token has expired, so we need to get a new one
          return this.authService.refreshToken().pipe(
            switchMap((newToken: string) => {
              // Clone the request and add the new token to the Authorization header
              const clonedRequest = request.clone({
                headers: request.headers.set('Authorization', `Bearer ${newToken}`)
              });

              // Retry the original request with the new token
              return next.handle(clonedRequest);
            })
          );
        } else {
          // Pass the error through
          return throwError(error);
        }
      })
    );
  }
}



*/
