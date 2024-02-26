import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LibraryPageRoutingModule } from './library-routing.module';
import { LibraryPage } from './library.page';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    LibraryPageRoutingModule
  ],
  declarations: [ LibraryPage ]
})
export class LibraryPageModule {
}
