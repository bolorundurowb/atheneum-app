import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DetailsPageRoutingModule } from './details-routing.module';
import { DetailsPage } from './details.page';
import { IonicModule } from '@ionic/angular';

@NgModule({
  imports: [
    CommonModule,
    DetailsPageRoutingModule,
    IonicModule
  ],
  declarations: [ DetailsPage ]
})
export class DetailsPageModule {
}
