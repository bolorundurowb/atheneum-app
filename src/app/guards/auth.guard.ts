import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services';

@Injectable({ providedIn: 'root' })
export class AuthGuard {
  constructor(private authService: AuthService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authService.isLoggedIn()) {
      if (this.authService.getUser().isEmailVerified) {
        return true;
      }

      window.location.href = '/auth/verify';
      return false;
    }

    window.location.href = '/auth/login';
    return false;
  }
}
