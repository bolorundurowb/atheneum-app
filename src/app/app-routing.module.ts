import { NgModule } from '@angular/core';
import { mapToCanActivate, PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards';

const routes: Routes = [
  {
    path: '',
    canActivate: mapToCanActivate([ AuthGuard ]),
    loadChildren: () => import('./tabs/tabs.module').then(m => m.TabsPageModule)
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {
}
