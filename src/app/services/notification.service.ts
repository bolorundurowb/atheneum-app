import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  constructor(private toastController: ToastController) {
  }

  async success(message: string) {
    return this.displayToast(message, 'success-toast');
  }

  error(message: string): Promise<void> {
    return this.displayToast(message, 'error-toast');
  }

  private async displayToast(message: string, cssClass: string) {
    const toast = await this.toastController
      .create({
        message,
        duration: 3500,
        position: 'bottom',
        cssClass
      });

    await toast.present();
  }
}
