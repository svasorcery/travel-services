import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ErrorsComponent } from './errors.component';
import { ForbiddenComponent } from './forbidden.component';

const errorRoutes: Routes = [
    { path: 'error', component: ErrorsComponent },
    { path: 'forbidden', component: ForbiddenComponent },
];

@NgModule({
    imports: [RouterModule.forChild(errorRoutes)],
    exports: [RouterModule]
})
export class ErrorRoutingModule {

}


export const errorsComponents = [
    ErrorsComponent,
    ForbiddenComponent
];
