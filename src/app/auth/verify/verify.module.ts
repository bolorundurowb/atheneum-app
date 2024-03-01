import { NgModule } from '@angular/core';
import { IonicModule } from '@ionic/angular';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VerifyPageRoutingModule } from './verify-routing.module';
import { VerifyPage } from './verify.page';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    VerifyPageRoutingModule
  ],
  declarations: [ VerifyPage ]
})
export class VerifyPageModule {
}
