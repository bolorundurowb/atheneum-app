import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordPage } from './forgot-password.page';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: ForgotPasswordPage
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class ForgotPasswordPageRoutingModule {
}
