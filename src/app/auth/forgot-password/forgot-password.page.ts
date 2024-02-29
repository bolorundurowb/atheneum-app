import { Component } from '@angular/core';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: 'forgot-password.page.html',
  styleUrls: [ 'forgot-password.page.scss' ]
})
export class ForgotPasswordPage {
  isLoggingIn = false;
  payload: any = {};

  constructor(private authService: AuthService, private notificationService: NotificationService,
              private router: Router) {
  }

  async login() {
    this.isLoggingIn = true;

    try {
      const response = await this.authService.login(this.payload);

      await this.notificationService.success('Successfully logged in');
      this.authService.persistUser(response);

      await this.router.navigate(['tabs', 'home']);
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isLoggingIn = false;
    }
  }
}
