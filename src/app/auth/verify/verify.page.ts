import { Component } from '@angular/core';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface VerificationPayload {
  verificationCode?: string;
}

@Component({
  selector: 'app-verify',
  templateUrl: 'verify.page.html',
  styleUrls: [ 'verify.page.scss' ]
})
export class VerifyPage {
  isVerifying = false;
  payload: VerificationPayload = {};

  constructor(private authService: AuthService, private notificationService: NotificationService,
              private router: Router) {
  }

  async verify() {
    this.isVerifying = true;

    try {
      this.payload.verificationCode = this.payload.verificationCode?.toString();
      await this.authService.verifyEmail(this.payload);
      await this.notificationService.success('Account successfully verified');

      await this.router.navigate([ 'tabs', 'home' ]);
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isVerifying = false;
    }
  }
}
