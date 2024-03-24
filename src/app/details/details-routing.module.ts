import { RouterModule, Routes } from '@angular/router';
import { DetailsPage } from './details.page';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: DetailsPage,
    children: []
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ]
})
export class DetailsPageRoutingModule {
}
