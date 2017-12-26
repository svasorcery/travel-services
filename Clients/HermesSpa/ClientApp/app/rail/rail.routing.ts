import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RailComponent } from './rail.component';
import { RailSearchComponent } from './search.component';

const routes: Routes = [
    { 
        path: 'rail', 
        component: RailComponent,
        children: [
            {
                path: '',
                component: RailSearchComponent
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RailRoutingModule { }

export const routedComponents = [
    RailComponent,
    RailSearchComponent
];
