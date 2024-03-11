import { NgModule } from '@angular/core';
import { BookComponent } from './book/book.component';
import { CommonModule } from '@angular/common';

@NgModule({
  imports: [ CommonModule ],
  exports: [
    BookComponent
  ],
  declarations: [ BookComponent ]
})
export class SharedModule {}
