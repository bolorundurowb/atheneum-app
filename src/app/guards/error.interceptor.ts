import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { AuthService, NotificationService } from '../services';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private notificationService: NotificationService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(catchError(error => {
      const errorStatus = error.status;
      const errorMessage = error.error?.message ?? error.message;

      if (errorStatus === 401) {
        this.authService.logout();
        window.location.href = '/auth/login';
      }

      return throwError(() => errorMessage);
    }));
  }
}
