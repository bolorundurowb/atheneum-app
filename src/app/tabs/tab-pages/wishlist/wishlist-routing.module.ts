import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WishlistPage } from './wishlist.page';

const routes: Routes = [
  {
    path: '',
    component: WishlistPage,
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class WishListPageRoutingModule {
}
