import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services';
import { Router } from '@angular/router';

import { App } from '@capacitor/app';

@Component({
  selector: 'app-settings',
  templateUrl: 'settings.page.html',
  styleUrls: [ 'settings.page.scss' ]
})
export class SettingsPage implements OnInit {
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

  appVersion?: string;

  constructor(private authService: AuthService, private router: Router) {
  }

  async ngOnInit() {
    this.appVersion = await this.getAppVersion();
  }

  async getAppVersion(): Promise<string | undefined> {
    try {
      const appInfo = await App.getInfo();
      return appInfo.version;
    } catch (e) {
      console.error('Failed to get the app version', e);
      return undefined;
    }
  }
}
