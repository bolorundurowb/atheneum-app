import { RouterModule, Routes } from '@angular/router';
import { DetailsPage } from './details.page';
import { NgModule } from '@angular/core';

const routes: Routes = [
  {
    path: '',
    component: DetailsPage,
    children: [
      {
        path: 'book',
        loadChildren: () => import('./book/book.module').then(m => m.BookPageModule)
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ]
})
export class DetailsPageRoutingModule {
}
