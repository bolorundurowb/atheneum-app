import { NgModule } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { BookPageRoutingModule } from './book-routing.module';
import { BookPage } from './book.page';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    BookPageRoutingModule
  ],
  declarations: [ BookPage ]
})
export class BookPageModule {
}
