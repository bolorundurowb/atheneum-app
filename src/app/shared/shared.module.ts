import { NgModule } from '@angular/core';
import { BookComponent } from './book/book.component';
import { CommonModule } from '@angular/common';
import { EmptyComponent } from './empty/empty.component';

@NgModule({
  imports: [ CommonModule ],
  exports: [
    BookComponent,
    EmptyComponent,
  ],
  declarations: [ BookComponent, EmptyComponent ]
})
export class SharedModule {
}
