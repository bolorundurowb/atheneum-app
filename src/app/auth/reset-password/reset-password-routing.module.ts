import { RouterModule, Routes } from '@angular/router';
import { ResetPasswordPage } from './reset-password.page';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: ResetPasswordPage
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ForgotPasswordPageRoutingModule {
}
