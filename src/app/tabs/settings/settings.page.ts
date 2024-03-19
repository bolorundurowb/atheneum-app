import { Component } from '@angular/core';
import { AuthService } from '../../services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: 'settings.page.html',
  styleUrls: [ 'settings.page.scss' ]
})
export class SettingsPage {
  logOutButtons = [
    {
      text: 'Cancel',
      role: 'cancel',
      handler: () => {
      }
    },
    {
      text: 'OK',
      role: 'confirm',
      handler: async () => {
        this.authService.logout();
        await this.router.navigate([ 'auth', 'login' ]);
      }
    }
  ];

  constructor(private authService: AuthService, private router: Router) {
  }
}
