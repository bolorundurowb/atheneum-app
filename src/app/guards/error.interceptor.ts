import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { AuthService } from '../services';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {
  }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(catchError(error => {
      const errorStatus = error.status;
      const errorMessage = error.error?.message ?? error.message;

      if (errorStatus === 401) {
        this.authService.logout();
        window.location.href = '/auth/login';
      } else if (errorStatus >= 500) {
        const errorTitle = error.statusText;
        // TODO
        // this.notificationService.showError(errorMessage, errorTitle);
      }

      return throwError(() => errorMessage);
    }));
  }
}
