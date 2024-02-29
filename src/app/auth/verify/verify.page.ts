import { Component } from '@angular/core';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface LoginPayload {
  emailAddress?: string;
  password?: string;
}

@Component({
  selector: 'app-verify',
  templateUrl: 'verify.page.html',
  styleUrls: [ 'verify.page.scss' ]
})
export class VerifyPage {
  isLoggingIn = false;
  payload: LoginPayload = {};

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
