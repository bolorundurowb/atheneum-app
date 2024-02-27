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
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ]
})
export class AuthPageRoutingModule {
}
