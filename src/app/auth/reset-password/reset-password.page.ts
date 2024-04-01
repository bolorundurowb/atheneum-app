import { Component } from '@angular/core';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface ForgotPasswordPayload {
  emailAddress?: string;
}

@Component({
  selector: 'app-forgot-password',
  templateUrl: 'forgot-password.page.html',
  styleUrls: [ 'forgot-password.page.scss' ]
})
export class ResetPasswordPage {
  isRequesting = false;
  payload: ForgotPasswordPayload = {};

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
