import { Component, OnInit } from '@angular/core';
import { AuthService, NotificationService, UserService } from '../../services';
import { Router } from '@angular/router';

import { App } from '@capacitor/app';

interface UpdateProfilePayload {
  firstName?: string;
  lastName?: string;
  emailAddress?: string;
}

@Component({
  selector: 'app-settings',
  templateUrl: 'settings.page.html',
  styleUrls: [ 'settings.page.scss' ]
})
export class SettingsPage implements OnInit {
  appVersion?: string;
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

  isUpdatingProfile = false;
  updatePayload: UpdateProfilePayload = {};

  constructor(private authService: AuthService, private router: Router, private userService: UserService,
              private notificationService: NotificationService) {
  }

  async ngOnInit() {
    this.appVersion = await this.getAppVersion();

    const currentUser = this.authService.getUser();
    this.setUser(currentUser);

    // load the latest user details
    const profile = await this.userService.getProfile();
    this.setUser(profile);
    this.authService.persistUser({ ...currentUser, ...profile });
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

  async updateProfile() {
    this.isUpdatingProfile = true;

    try {
      await this.userService.updateProfile(this.updatePayload);
      await this.notificationService.success('Profile successfully updated');
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isUpdatingProfile = false;
    }
  }

  setUser(user: any) {
    const { firstName, lastName, emailAddress } = user;
    this.updatePayload = { firstName, lastName, emailAddress };
  }
}
