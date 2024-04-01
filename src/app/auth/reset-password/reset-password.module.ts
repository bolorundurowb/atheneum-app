import { NgModule } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ResetPasswordPage } from './reset-password.page';
import { ForgotPasswordPageRoutingModule } from './forgot-password-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ForgotPasswordPageRoutingModule
  ],
  declarations: [ ResetPasswordPage ]
})
export class ForgotPasswordPageModule {
}
