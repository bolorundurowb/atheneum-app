import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TabsPage } from './tabs.page';

const routes: Routes = [
  {
    path: 'tabs',
    component: TabsPage,
    children: [
      {
        path: 'home',
        loadChildren: () => import('./tab-pages/home/home.module').then(m => m.HomePageModule)
      },
      {
        path: 'library',
        loadChildren: () => import('./tab-pages/library/library.module').then(m => m.LibraryPageModule)
      },
      {
        path: 'wishlist',
        loadChildren: () => import('./tab-pages/wishlist/wishlist.module').then(m => m.WishListPageModule)
      },
      {
        path: 'profile',
        loadChildren: () => import('./tab-pages/profile/profile.module').then(m => m.ProfilePageModule)
      },
      {
        path: '',
        redirectTo: '/tabs/home',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '',
    redirectTo: '/tabs/home',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
})
export class TabsPageRoutingModule {}
