import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WishlistPage } from './wishlist.page';

import { WishListPageRoutingModule } from './wishlist-routing.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    WishListPageRoutingModule
  ],
  declarations: [ WishlistPage ]
})
export class WishListPageModule {
}
