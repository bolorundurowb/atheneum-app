import { RouterModule, Routes } from '@angular/router';
import { VerifyPage } from './verify.page';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: VerifyPage
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class VerifyPageRoutingModule {
}
