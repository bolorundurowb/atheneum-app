import { Component } from '@angular/core';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface ResetPasswordPayload {
  emailAddress?: string;
  resetCode?: string;
  password?: string;
  confirmPassword?: string;
}

@Component({
  selector: 'app-reset-password',
  templateUrl: 'reset-password.page.html',
  styleUrls: [ 'reset-password.page.scss' ]
})
export class ResetPasswordPage {
  isRequesting = false;
  payload: ResetPasswordPayload = {};

  constructor(private authService: AuthService, private notificationService: NotificationService,
              private router: Router) {
  }

  async requestReset() {
    this.isRequesting = true;

    try {
      await this.authService.forgotPassword(this.payload);
      await this.notificationService.success('A reset code has been sent your email address');
      this.payload = {};

      setTimeout(async () => {
        await this.router.navigate([ 'auth', 'reset-password' ]);
      }, 1500);
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isRequesting = false;
    }
  }
}
