import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RailComponent } from './rail.component';

const routes: Routes = [
    { 
        path: 'rail', 
        component: RailComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RailRoutingModule { }

export const routedComponents = [
    RailComponent
];
