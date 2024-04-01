import { NgModule } from '@angular/core';
import { BookComponent } from './book/book.component';
import { CommonModule } from '@angular/common';
import { EmptyComponent } from './empty/empty.component';
import { IonicModule } from '@ionic/angular';

@NgModule({
  imports: [ CommonModule, IonicModule ],
  exports: [
    BookComponent,
    EmptyComponent,
  ],
  declarations: [ BookComponent, EmptyComponent ]
})
export class SharedModule {
}
