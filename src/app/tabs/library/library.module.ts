import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LibraryPageRoutingModule } from './library-routing.module';
import { LibraryPage } from './library.page';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    SharedModule,
    LibraryPageRoutingModule
  ],
  declarations: [ LibraryPage ]
})
export class LibraryPageModule {
}
