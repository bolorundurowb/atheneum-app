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
        path: 'verify',
        loadChildren: () => import('./verify/verify.module').then(m => m.VerifyPageModule)
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ]
})
export class AuthPageRoutingModule {
}
