import { Component, OnInit } from '@angular/core';
import { AuthService, NotificationService, UserService } from '../../services';
import { Router } from '@angular/router';

import { App } from '@capacitor/app';

interface UpdateProfilePayload {
  id?: string;
  firstName?: string;
  lastName?: string;
  emailAddress?: string;
}

interface ChangePasswordPayload {
  currentPassword?: string;
  newPassword?: string;
  confirmNewPassword?: string;
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

  isChangingPassword = false;
  changePayload: ChangePasswordPayload = {};

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

  async changePassword() {
    if (Number(this.changePayload.newPassword?.length) < 8) {
      await this.notificationService.error('The password must be at least 8 characters');
      return;
    }

    if (this.changePayload.confirmNewPassword !== this.changePayload.newPassword) {
      await this.notificationService.error('The password and confirmation must match');
      return;
    }

    this.isChangingPassword = true;

    try {
      await this.userService.updatePassword(this.changePayload);
      await this.notificationService.success('Password changed successfully');
      this.changePayload = {};
    } catch (e) {
      await this.notificationService.error(e as string);
    } finally {
      this.isChangingPassword = false;
    }
  }

  async buyMeACoffee() {
    await this.router.navigateByUrl('https://www.buymeacoffee.com/bolorundurowb');
  }

  async goToSourceCode() {
    await this.router.navigateByUrl('https://github.com/bolorundurowb/atheneum-app');
  }

  async giveFeedback() {
    await this.router.navigateByUrl('https://github.com/bolorundurowb/atheneum-app/issues');
  }

  setUser(user: any) {
    const { _id, firstName, lastName, emailAddress } = user;
    this.updatePayload = { id: _id, firstName, lastName, emailAddress };
  }
}
