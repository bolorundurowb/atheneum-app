import { RouterModule, Routes } from '@angular/router';
import { AuthPage } from './auth.page';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: AuthPage,
    children: [
      {
        path: 'login',
        loadChildren: () => import('./login/login.module').then(m => m.LoginPageModule)
      },
      {
        path: 'forgot-password',
        loadChildren: () => import('./forgot-password/forgot-password.module').then(m => m.ForgotPasswordPageModule)
      },
      {
        path: 'reset-password',
        loadChildren: () => import('./reset-password/reset-password.module').then(m => m.ResetPasswordPageModule)
      },
      {
        path: 'verify',
        loadChildren: () => import('./verify/verify.module').then(m => m.VerifyPageModule)
      },
      {
        path: 'register',
        loadChildren: () => import('./register/register.module').then(m => m.RegisterPageModule)
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ]
})
export class AuthPageRoutingModule {
}
