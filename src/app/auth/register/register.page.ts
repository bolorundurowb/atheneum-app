import { Component } from '@angular/core';
import { AuthService, NotificationService } from '../../services';
import { Router } from '@angular/router';

interface RegisterPayload {
  emailAddress?: string;
  password?: string;
}

@Component({
  selector: 'app-register',
  templateUrl: 'register.page.html',
  styleUrls: [ 'register.page.scss' ]
})
export class RegisterPage {
  isRegistering = false;
  payload: RegisterPayload = {};

  constructor(private authService: AuthService, private notificationService: NotificationService,
              private router: Router) {
  }

  async register() {
    this.isRegistering = true;

    try {
      const response = await this.authService.register(this.payload);

      await this.notificationService.success('Successfully registered');
      this.authService.persistUser(response);

      await this.router.navigate([ 'auth', 'verify' ]);
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isRegistering = false;
    }
  }
}
