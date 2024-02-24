import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WishlistPage } from './wishlist.page';

import { WishListRoutingModule } from './wishlist-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    WishListRoutingModule
  ],
  declarations: [ WishlistPage ]
})
export class WishListModule {
}
